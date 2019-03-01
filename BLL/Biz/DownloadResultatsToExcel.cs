using BLL.Common;
using DAL;
using log4net;
using OfficeOpenXml;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class DownloadResultatsToExcel : CommonBiz
    {
        public DownloadResultatsToExcel(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        public async Task<List<ExportExcelModel>> GetResultatToExcelAsync(int? id)
        {
            #region Check demand acces id & find it
            if (id == null)
            {
                return null;
            }

            var demandeAcces = await context.DemandeAccesEngin.FindAsync(id);
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
            #region get exigence
            var exigences = typeCheckList.CheckListRubrique.SelectMany(x => x.CheckListExigence).ToList(); // all checklist exigences
            #endregion
            var heading = "si";
            var showSrNo = true;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 3 : 3;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                foreach (var rubrique in typeCheckList.CheckListRubrique.Where(x => x.IsActif == true))
                {
                    foreach (var checkListExigence in rubrique.CheckListExigence.Where(x => x.IsActif == true))
                    {
                        var data = demandeAcces.ResultatExigence.Where(x => x.CheckListExigenceId == checkListExigence.Id).FirstOrDefault();

                        if (data == null)
                        {
                            continue;
                        }
                        var model = new ExportExcelModel()
                        {
                            Entity = demandeAcces.Entity.Name,
                            ChefProjet = demandeAcces.AspNetUsers.Profile.FullName,
                            NCommande = demandeAcces.Id.ToString(),
                            TypeEngin = demandeAcces.TypeEngin.Name,

                        }


                }
                }
            }




        }
    }
}
