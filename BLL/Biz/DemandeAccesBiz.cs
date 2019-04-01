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

namespace BLL.Biz
{
    public class DemandeAccesBiz : CommonBiz
    {
        public DemandeAccesBiz(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        public List<DemandeAccesDto> DemandeAccesList(int pageIndex, int pageSize)
        {
            var demandeAccesList = context.DemandeAccesEngin.ToList();
            var Demendes = demandeAccesList.Where(x => !x.DemandeResultatEntete.Any()).Select(x => x.DemandeAccesToDTO()).ToList();
            var demandeAcces = Demendes.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return demandeAcces;
        }

        public async Task<TypeCheckListDTO> GetCheckListAsync(int id)
        {
            var controle = await context.DemandeAccesEngin.FindAsync(id);

            #region Check Controle id & find it

            var typeCheckList = await context.TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();

            var typeCheckListDTO = typeCheckList.TypeCheckListToDTO();

            //  var tt = typeCheckListDTO.Rubriques.GroupBy(r => r.Name).Select(x => new Grouping<string, CheckListRubriqueDTO>(x.Key, x)).ToList();
            #endregion
            // typeCheckListDTO.RubriquesGrouping = tt;

            return typeCheckListDTO;
        }

        public async Task<bool> PostResultatExigencesAsync(ResultatCheckList resultat)
        {
            try
            {
                #region Save entete resultat Exigence 
                var resultatEntete = new DemandeResultatEntete()
                {
                    DemandeAccesEnginId = resultat.DemandeAccesEnginId,
                    CreatedBy = resultat.CreatedBy,
                    CreatedOn = resultat.CreatedOn
                };
                #endregion

                context.DemandeResultatEntete.Add(resultatEntete);
                var cc = await context.SaveChangesAsync();

                #region Save resultat Exigence
                foreach (var resultatEx in resultat.ResultatsList)
                {
                    var controlResultatExigence = new ResultatExigence()
                    {
                        DemandeResultatEnteteId = resultatEntete.Id,
                        CheckListExigenceId = resultatEx.CheckListExigenceId,
                        IsConform = resultatEx.IsConform,
                        Date = resultatEx.Date,
                        Observation = resultatEx.Observation,
                    };

                    context.ResultatExigence.Add(controlResultatExigence);


                }
                var tt = await context.SaveChangesAsync();
                #endregion
                return true;

            }
            catch (Exception)
            {
                return false;
            }



        }

        public async Task<bool> ReporterAction(ReporterDemande reporterDemande , string currentUserId)
        {
            context.Report.Add(new Report()
            {
                DemandeAccesEnginId = reporterDemande.DemandeAccesEnginId,
                MotifReport = reporterDemande.Motif,
                CreatedBy = currentUserId,
                CreatedOn = DateTime.Now,
            });
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
