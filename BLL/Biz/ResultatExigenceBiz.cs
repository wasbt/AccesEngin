using BLL.Common;
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
        public ResultatExigenceBiz(EnginDbContext context, ILog log) : base(context, log)
        {

        }


        public async Task<ResultatExigenceModel> GetResultatExigenceByDemandeAccesId(int Id)
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
            var typeCheckList = await context.TypeCheckList.Where(x => x.Id == demandeAcces.TypeCheckListId).FirstOrDefaultAsync();
            if (typeCheckList == null)
            {
                return null;
            }
            #endregion

            #region get & check resultat entete 
            var resultatEntete = await context.DemandeResultatEntete.Where(x => x.DemandeAccesEnginId == demandeAcces.Id).FirstOrDefaultAsync();
            if (resultatEntete == null)
            {
                return null;
            }
            #endregion

            #region get exigence
            var exigences = typeCheckList.CheckListRubrique.SelectMany(x => x.CheckListExigence).ToList(); // all checklist exigences
            #endregion


            #region Conformité

            var resultatExigenceDetail = resultatEntete.ResultatExigence.ToList();
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

            var RubricGroupingList = typeCheckList.InfoGenerale.GroupBy(g => g.InfoGeneralRubrique.Name);
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

                    var info = demandeAcces.ResultatInfoGenerale.Where(x => x.InfoGeneraleId == rebricInfo.Id).FirstOrDefault();
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
            var rubriqueGrouping = typeCheckList.CheckListRubrique.Where(x => x.IsActif == true);
            foreach (var rubrique in rubriqueGrouping)
            {

                var group = new Group
                {
                    Key = rubrique.Name,
                    ColorRubrique = "#dadbdb",
                    ResultatValue = new List<ResultatValue>(),

                };

                var checkListExigenceGrouping = rubrique.CheckListExigence.Where(x => x.IsActif == true);


                foreach (var checkListExigence in checkListExigenceGrouping)
                {
                    var data = resultatEntete.ResultatExigence.Where(x => x.CheckListExigenceId == checkListExigence.Id).FirstOrDefault();

                    if (data == null)
                    {
                        continue;
                    }
                    var element = new ResultatValue
                    {
                        Name = checkListExigence.Name,
                        Datetime = data.Date.ToString(),
                        Observation = data.Observation,
                        Conform = data.IsConform ? $"Conforme" : $"Non conforme",
                        IsExigence = true,
                        IsInfoG = false,
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
