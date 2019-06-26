﻿using BLL.Common;
using DAL;
using log4net;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Shared;
using Shared.Models;
using System.Web;
using System.Diagnostics;
using Shared.ENUMS;
using Shared.API.OUT;
using System.IO;

namespace BLL.Biz
{
    public class DemandeAccesBiz : CommonBiz
    {
        public DemandeAccesBiz(OcpPerformanceDataContext context, ILog log) : base(context, log)
        {
        }

        public List<DemandeAccesDto> DemandeAccesList(int pageIndex, int pageSize)
        {
            var demandeAccesList = context.DemandeAccesEngin.ToList();
            var Demendes = demandeAccesList.Where(x => !x.ResultatControleEntete.Any()).Select(x => x.DemandeAccesToDTO()).ToList();
            var demandeAcces = Demendes.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return demandeAcces;
        }

        public async Task<DemandeDetail> GetDetailsDemandeByIdAsync(int Id)
        {
            var demandeAcces = await context.DemandeAccesEngin.FindAsync(Id);

            if (demandeAcces == null)
                return null;


            var demandeDetail = new DemandeDetail()
            {
                Id = demandeAcces.Id,
                TypeCheckListId = demandeAcces.TypeCheckListId,
                TypeEnginName = demandeAcces.REF_TypeEngin.Name,
                TypeCheckListName = demandeAcces.REF_TypeCheckList.Name,
                NatureMatiereName = demandeAcces?.REF_NatureMatiere?.Name,
                EntityName = demandeAcces.Entite.Name,
                DatePlannification = demandeAcces.DatePlannification,
                IsAutorise = demandeAcces.IsAutorise,
                Observation = demandeAcces?.Observation,
                CreatedBy = demandeAcces.CreatedBy,
                CreatedOn = demandeAcces.CreatedOn,
                CreatedEmail = demandeAcces.AspNetUsers.Email,
                AutoriseName = demandeAcces.IsAutorise ? "Autorisé" : "Non autorisé",
                StatutId = demandeAcces.StatutDemandeId,
                Statut = demandeAcces?.REF_StatutDemandes?.Name,
                StatutColor = demandeAcces?.REF_StatutDemandes?.Color,
            };

            return demandeDetail;
        }

        public async Task<TypeCheckListDTO> GetCheckListAsync(int id)
        {
            var controle = await context.DemandeAccesEngin.FindAsync(id);

            #region Check Controle id & find it

            var typeCheckList = await context.REF_TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();

            var typeCheckListDTO = typeCheckList.TypeCheckListToDTO();

            //  var tt = typeCheckListDTO.Rubriques.GroupBy(r => r.Name).Select(x => new Grouping<string, CheckListRubriqueDTO>(x.Key, x)).ToList();
            #endregion
            // typeCheckListDTO.RubriquesGrouping = tt;

            return typeCheckListDTO;
        }

        public async Task<bool> PostResultatExigencesAsync(ResultatCheckList resultat, HttpPostedFileBase uploadedFile, string ContainerName, string SourceName)
        {
            byte[] fileData = null;
            long SourceId = 0;
            long fileId = 0;
            try
            {
                #region Update Demande
                var demande = await context.DemandeAccesEngin.FindAsync(resultat.DemandeAccesEnginId);
                demande.IsAutorise = resultat.IsAutorise;
                #endregion

                #region Save entete resultat Exigence 
                var resultatEntete = new ResultatControleEntete();
                resultatEntete.DemandeAccesEnginId = resultat.DemandeAccesEnginId;
                resultatEntete.CreatedBy = resultat.CreatedBy;
                resultatEntete.CreatedOn = DateTime.Now;
                #endregion

                context.ResultatControleEntete.Add(resultatEntete);
                await context.SaveChangesAsync();

                SourceId = resultatEntete.Id;

                #region Save resultat Exigence
                foreach (var resultatEx in resultat.ResultatsList)
                {
                    var controlResultatExigence = new ResultatControleDetail()
                    {
                        ResultatControleEnteteId = resultatEntete.Id,
                        CheckListExigenceId = resultatEx.CheckListExigenceId,
                        IsConform = resultatEx.IsConform,
                        DateExpiration = resultatEx.Date,
                        Observation = resultatEx.Observation,
                    };

                    context.ResultatControleDetail.Add(controlResultatExigence);
                }
                #endregion


                #region Ajout de la pièce jointe dans le contexte
                var biz = new CommonBiz(context, log);

                fileId = await biz.SaveAppFile(uploadedFile, ContainerName, SourceId.ToString(), SourceName);

                resultatEntete.AppFileId = fileId;
                await context.SaveChangesAsync();

                #endregion
                return true;

            }
            catch (Exception)
            {
                return false;
            }



        }

        public async Task<bool> ValiderDemande(ValiderDemande validerDemande, string currentUserId)
        {

            var demande = await context.DemandeAccesEngin.FindAsync(validerDemande.DemandeAccesEnginId);

            demande.StatutDemandeId = validerDemande.StatutDemandeId;

            if (!string.IsNullOrEmpty(validerDemande.Motif) && validerDemande.StatutDemandeId == 2)
            {
                context.ReponseDemande.Add(new ReponseDemande()
                {
                    DemandeAccesEnginId = validerDemande.DemandeAccesEnginId,
                    Motif = validerDemande.Motif,
                    CreatedBy = currentUserId,
                    CreatedOn = DateTime.Now,
                });
            }
            var verifier = await context.SaveChangesAsync();
            if (verifier > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public async Task<long> removeOldFile(DemandeAccesEngin demandeAccesEngin, HttpPostedFileBase fileBase, string ContainerName, string SourceName)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //   var existFile =  context.RegulatoryText.Where(r => r.RegulatoryTextId == regulatoryText.RegulatoryTextId);
                    if (demandeAccesEngin.AppFileId.HasValue)
                    {
                        var oldFile = await context.AppFile.FindAsync(demandeAccesEngin.AppFileId);
                        context.AppFile.Remove(oldFile);
                        await context.SaveChangesAsync();
                    }
                    //add file to database & Azure
                    var fileId = await SaveOCPFile(fileBase, ContainerName, demandeAccesEngin.Id, SourceName);
                    if (fileId != 0)
                    {
                        transaction.Commit();
                        return fileId;
                    }
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Debug.WriteLine(ex.Message);
                }
            }
            return 0;
        }

        public async Task<List<DemandeAccesDto>> DemandeAccesByMatricule(string Matricule)
        {

            #region Check demand acces id & find it
            if (string.IsNullOrWhiteSpace(Matricule))
            {
                return null;
            }
            var demandeAcces = context.DemandeAccesEngin
                .Where(x =>
                    x.IsAutorise &&
                    x.StatutDemandeId == (int)DemandeStatus.Accepter &&
                    x.DemandeAccesEnginInfoGeneraleValue
                    .Any(i => i.REF_InfoGenerale.Name.Equals("Matricule", StringComparison.OrdinalIgnoreCase) &&
                          i.ValueInfo.Contains(Matricule)));
            if (demandeAcces == null)
            {
                return null;
            }
            #endregion
            var DemendesList = await demandeAcces.ToListAsync();
            var DemendesDto = DemendesList.Select(x => x.DemandeAccesToDTO()).ToList();
            return DemendesDto;
        }

        public async Task<List<DemandeAccesEngin>> DemandeAccesByEntityMatricule(SearchDemandeModel model)
        {
            var demandeAcces = context.DemandeAccesEngin.Where(x =>
                                                                   x.IsAutorise &&
                                                                   x.ResultatControleEntete.Any())
                                                                   .AsQueryable();

            #region Check demand acces id & find it
            if (!string.IsNullOrWhiteSpace(model.Matricule))
            {
                //demandeAcces = demandeAcces.Where(x => x.ResultatInfoGenerale.Any(i => i.ValueInfo.Contains(model.Matricule)));
                demandeAcces = demandeAcces
                .Where(x =>
                       x.DemandeAccesEnginInfoGeneraleValue
                .Any(i =>
                     i.REF_InfoGenerale.Name.Equals("Matricule", StringComparison.OrdinalIgnoreCase) &&
                     i.ValueInfo.Equals(model.Matricule, StringComparison.OrdinalIgnoreCase)));
            }
            if (model.EntityId.HasValue)
            {
                demandeAcces = demandeAcces.Where(x => x.EntiteId == model.EntityId);
            }
            #endregion

            var DemendesList = await demandeAcces.ToListAsync();

            return DemendesList;
        }


        public async Task<bool> SortirEngin(ValiderDemande validerDemande, string currentUserId)
        {

            var demande = await context.DemandeAccesEngin.FindAsync(validerDemande.DemandeAccesEnginId);

            demande.StatutDemandeId = (int)DemandeStatus.Sortir;
            demande.DateSortie = validerDemande.DateSortie;
            demande.CreatedBy = currentUserId;

            var verifier = await context.SaveChangesAsync();
            if (verifier > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

    }

}
