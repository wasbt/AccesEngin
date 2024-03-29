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
using Shared.API.IN;
using BLL.Helpers;

namespace BLL.Biz
{
    public class DemandeAccesBiz : CommonBiz
    {
        public DemandeAccesBiz(OcpPerformanceDataContext context, ILog log) : base(context, log)
        {
        }

        public List<ControleModel> DemandeAccesList(FilterListDemande filterList)
        {
            var controlesList = new List<ControleModel>();
            IQueryable<ControleModel> controles;
            var demandeAccesQuery = context.DemandeAccesEngin.AsQueryable();

            if (filterList.TypeCheckListId.HasValue && filterList.TypeCheckListId != 0)
            {
                demandeAccesQuery = demandeAccesQuery.Where(x => x.TypeCheckListId == filterList.TypeCheckListId);
            }
            if (filterList.StatutId.HasValue && filterList.StatutId != 0)
            {
                demandeAccesQuery = demandeAccesQuery.Where(x => x.StatutDemandeId == filterList.StatutId);
            }
            if (filterList.DatePlanification.HasValue)
            {
                demandeAccesQuery = demandeAccesQuery.Where(x => x.DatePlannification == filterList.DatePlanification);
            }
            if (!string.IsNullOrWhiteSpace(filterList.Matricule))
            {
                demandeAccesQuery = demandeAccesQuery.Where(x =>
                    x.IsAutorise &&
                    x.StatutDemandeId == (int)DemandeStatus.Accepter &&
                    x.DemandeAccesEnginInfoGeneraleValue
                    .Any(i => i.REF_InfoGenerale.Name.Equals("Matricule", StringComparison.OrdinalIgnoreCase) &&
                          i.ValueInfo.Contains(filterList.Matricule)));
            }

            if (filterList.OnlyControle)
            {
                demandeAccesQuery = demandeAccesQuery.Where(x => x.ResultatControleEntete.Any());
                 controles = demandeAccesQuery.Select(x => new ControleModel
                {

                    IsAutorise = x.IsAutorise,
                    TypeCheckListName = x.REF_TypeCheckList.Name,
                    TypeEnginName = x.REF_TypeEngin.Name,
                    DatePlannification = x.DatePlannification,
                    Statut = x.REF_StatutDemandes.Name,
                    StatutColor = x.REF_StatutDemandes.Color,
                    Id = x.Id,
                    AutoriseName = x.IsAutorise ? "Autorisé" : "Non autorisé",
                     OnlyControle = true,
                });
            }
            else
            {
                demandeAccesQuery = demandeAccesQuery.Where(x => !x.ResultatControleEntete.Any());
                 controles = demandeAccesQuery.Select(x => new ControleModel
                {

                    IsAutorise = x.IsAutorise,
                    TypeCheckListName = x.REF_TypeCheckList.Name,
                    TypeEnginName = x.REF_TypeEngin.Name,
                    DatePlannification = x.DatePlannification,
                    Statut = x.REF_StatutDemandes.Name,
                    StatutColor = x.REF_StatutDemandes.Color,
                    Id = x.Id,
                    AutoriseName = x.IsAutorise ? "Autorisé" : "Non autorisé",
                     OnlyControle = false,
                });
            }

            var demandeList = controles.ToList();

            //var demandeAcces = Demendes.Skip(filterList.PageIndex * filterList.PageSize).Take(filterList.PageSize).ToList();
            return demandeList;
        }

      

        public async Task<TypeCheckListDTO> GetCheckListAsync(long id)
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

        public async Task<bool> PostResultatExigencesAsync(PostResultatExigenceModel postResultat)
        {
            byte[] fileData = null;
            long SourceId = 0;
            long fileId = 0;
            try
            {
                #region Update Demande
                var demande = await context.DemandeAccesEngin.FindAsync(postResultat.ResultatCheckList.DemandeAccesEnginId);
                demande.IsAutorise = postResultat.ResultatCheckList.IsAutorise;
                #endregion

                #region Save entete resultat Exigence 
                var controlHasDetails = demande.ResultatControleEntete.Count > 0;
                if (controlHasDetails)
                {
                    return false;
                }
                var resultatEntete = new ResultatControleEntete();
                resultatEntete.DemandeAccesEnginId = postResultat.ResultatCheckList.DemandeAccesEnginId;
                resultatEntete.CreatedBy = postResultat.ResultatCheckList.CreatedBy;
                resultatEntete.CreatedOn = DateTime.Now;
                #endregion

                context.ResultatControleEntete.Add(resultatEntete);
                await context.SaveChangesAsync();

                SourceId = resultatEntete.Id;

                #region Save resultat Exigence
                foreach (var resultatEx in postResultat.ResultatCheckList.ResultatsList)
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

                if (postResultat.ByteFile != null)
                {
                    var biz = new CommonBiz(context, log);

                    var stream = new MemoryStream(postResultat.ByteFile);

                    var memoryPostedFile = new MemoryPostedFile(stream, "image/jpeg", postResultat.NameFile);

                    var objFile = memoryPostedFile;

                    fileId = await biz.SaveAppFile(objFile, ConstsAccesEngin.ContainerName, SourceId.ToString(), ConstsAccesEngin.ContainerName);

                    resultatEntete.AppFileId = fileId;
                }
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

        public async Task<List<TypeCheckListDTO>> GetTypeCheckListAsync()
        {
            var typeCheckList = await context.REF_TypeCheckList.AsQueryable().ToListAsync();

            #region Check Controle id & find it

            var typeCheckListDTO = typeCheckList.Select(x => x.TypeCheckListToDTO()).ToList();

            //  var tt = typeCheckListDTO.Rubriques.GroupBy(r => r.Name).Select(x => new Grouping<string, CheckListRubriqueDTO>(x.Key, x)).ToList();
            #endregion
            // typeCheckListDTO.RubriquesGrouping = tt;

            return typeCheckListDTO;
        }

    }

}
