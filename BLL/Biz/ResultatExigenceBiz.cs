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

            //if (demandeAcces.Autorise)
            //{

            //}
            //else
            //{

            //}

            var RubricGroupingList = typeCheckList.InfoGenerale.GroupBy(g => g.InfoGeneralRubrique.Name);
            result.ResultatValueGroupingInfoG = new List<GroupInfoGeneral>();
            result.ResultatValueGroupingExigence = new List<GroupExigence>();
            #region rubrique info generale
            foreach (var infoRubriqueGroup in RubricGroupingList)
            {

                var group = new GroupInfoGeneral
                {
                    Key = infoRubriqueGroup.Key,
                    RsesultatInfoGrenerale = new List<ResultatValueInfoGrenerale>(),

                };

                foreach (var rebricInfo in infoRubriqueGroup)
                {

                    var info = demandeAcces.ResultatInfoGenerale.Where(x => x.InfoGeneraleId == rebricInfo.Id).FirstOrDefault();
                    if (info == null)
                    {
                        continue;
                    }

                    var element = new ResultatValueInfoGrenerale
                    {
                        Name = rebricInfo.Name,
                        Value = info.ValueInfo
                    };
                    group.RsesultatInfoGrenerale.Add(element);
                }
                result.ResultatValueGroupingInfoG.Add(group);
            }


            #endregion



            #region rubrique 
            var rubriqueGrouping = typeCheckList.CheckListRubrique.Where(x => x.IsActif == true);
            foreach (var rubrique in rubriqueGrouping)
            {

                var group = new GroupExigence
                {
                    Key = rubrique.Name,
                    ResultatExigence = new List<ResultatValueExigence>(),

                };

                var checkListExigenceGrouping = rubrique.CheckListExigence.Where(x => x.IsActif == true);


                foreach (var checkListExigence in checkListExigenceGrouping)
                {
                    var data = resultatEntete.ResultatExigence.Where(x => x.CheckListExigenceId == checkListExigence.Id).FirstOrDefault();

                    if (data == null)
                    {
                        continue;
                    }
                    var element = new ResultatValueExigence
                    {
                        Name = checkListExigence.Name,
                        datetime = data.Date.ToString(),
                        Observation = data.Observation,
                        conform = data.IsConform ? $"Conforme" : $"Non conforme",
                    };

                    group.ResultatExigence.Add(element);
                }
                result.ResultatValueGroupingExigence.Add(group);
            }
            #endregion

            #endregion

            return result;



        }
    }
}
