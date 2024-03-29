﻿using BLL.Common;
using DAL;
using log4net;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class ResultatExigenceBiz : CommonBiz
    {
        public ResultatExigenceBiz(OcpPerformanceDataContext  context, ILog log) : base(context, log)
        {

        }


        public async Task<ResultatExigenceModel> GetResultatExigenceByDemandeAccesId(long Id)
        {
            var result = new ResultatExigenceModel();
            #region Check demand acces id & find it
            if (Id == 0 || Id == null)
            {
                return null;
            }

            var demandeAcces = await context.DemandeAccesEngin.FindAsync(Id);
            if (demandeAcces == null)
            {
                return null;
            }
            #endregion

            #region get & check checklist
            var typeCheckList = await context.REF_TypeCheckList.Where(x => x.Id == demandeAcces.TypeCheckListId).FirstOrDefaultAsync();
            if (typeCheckList == null)
            {
                return null;
            }
            #endregion

            #region get & check resultat entete 
            var resultatEntete = await context.ResultatControleEntete.Where(x => x.DemandeAccesEnginId == demandeAcces.Id).FirstOrDefaultAsync();
            if (resultatEntete == null)
            {
                return null;
            }
            #endregion

            #region get exigence
            var exigences = typeCheckList.REF_CheckListRubrique.SelectMany(x => x.REF_CheckListExigence).ToList(); // all checklist exigences
            #endregion


            #region Conformité

            var resultatExigenceDetail = resultatEntete.ResultatControleDetail.ToList();
            var exigenceNonApplicable = resultatExigenceDetail.Where(x => !x.IsConform).ToList();
            var exigencesNonApplicableCount = exigenceNonApplicable.LongCount();
            var exigenceApplicable = resultatExigenceDetail.Where(x => x.IsConform).ToList();
            var exigencesApplicableCount = exigenceApplicable.LongCount();

            var Total = (exigencesApplicableCount + exigencesNonApplicableCount);

            var exigencesApplicable = (exigencesApplicableCount * 100) / Total;
            var exigencesNonApplicable = (exigencesNonApplicableCount * 100) / Total;
            #endregion

            #region Generatre Resultat

            result.DemandeAccesDto = demandeAcces.DemandeAccesToDTO();

            var RubricGroupingList = typeCheckList.REF_InfoGenerale.GroupBy(g => g.REF_InfoGeneralRubrique.Name);
            result.ResultatValueGrouping = new List<Group>();
            #region rubrique info generale
            foreach (var infoRubriqueGroup in RubricGroupingList)
            {

                var group = new Group
                {
                    Key = infoRubriqueGroup.Key,
                    ColorRubrique = "#cff0da",
                    ResultatValue = new List<ResultatValue>(),

                };

                foreach (var rebricInfo in infoRubriqueGroup)
                {

                    var info = demandeAcces.DemandeAccesEnginInfoGeneraleValue.Where(x => x.InfoGeneraleId == rebricInfo.Id).FirstOrDefault();
                    if (info == null)
                    {
                        continue;
                    }

                    var element = new ResultatValue
                    {
                        Name = rebricInfo.Name,
                        Value = info.ValueInfo,
                        IsExigence = false,
                        IsInfoG = true,
                    };
                    group.ResultatValue.Add(element);
                }
                result.ResultatValueGrouping.Add(group);
            }


            #endregion


            #region rubrique 
            var rubriqueGrouping = typeCheckList.REF_CheckListRubrique.Where(x => x.IsActif == true);
            foreach (var rubrique in rubriqueGrouping)
            {

                var group = new Group
                {
                    Key = rubrique.Name,
                    ColorRubrique = "#dadbdb",
                    ResultatValue = new List<ResultatValue>(),

                };

                var checkListExigenceGrouping = rubrique.REF_CheckListExigence.Where(x => x.IsActif == true);


                foreach (var checkListExigence in checkListExigenceGrouping)
                {
                    var data = resultatEntete.ResultatControleDetail.Where(x => x.CheckListExigenceId == checkListExigence.Id).FirstOrDefault();

                    if (data == null)
                    {
                        continue;
                    }
                    var element = new ResultatValue
                    {
                        Name = checkListExigence.Name,
                        Datetime = data.DateExpiration.ToString(),
                        Observation = data.Observation,
                        Conform = data.IsConform ? $"Conforme" : $"Non conforme",
                        IsExigence = true,
                        IsInfoG = false,
                        Color = data.IsConform ? $"#44D185" : $"#FF6370",
                    };

                    group.ResultatValue.Add(element);
                }
                result.ResultatValueGrouping.Add(group);
            }
            #endregion

            #endregion

            return result;



        }
    }
}
