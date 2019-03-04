using BLL.Common;
using DAL;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

        public async Task<byte[]> GetResultatToExcelAsync(int? id)
        {
            byte[] result = null;

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
            var minColumn = 2;
            var maxColumnInfoG = 7;
            var maxColumn = 14;
            var startingRow = 2;
            var showSrNo = true;
            var ExigenceColumn = 2;
            var ResultatColumnInfoG = 5;
            var ResultatColumn = 8;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 3 : 3;

                workSheet.InsertRow(workSheet.Cells[startingRow + 1, minColumn, startingRow, maxColumnInfoG].Start.Row, 1, copyStylesFromRow: 1);


                foreach (var infoRubriqueGroup in typeCheckList.InfoGenerale.GroupBy(g => g.InfoGeneralRubrique.Name))
                {
                    startingRow++;

                    #region rubrique info generale
                    //format the current row's cells
                    for (int celli = minColumn; celli <= maxColumnInfoG; celli++)
                    {
                        workSheet.Cells[startingRow, celli].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    //always insert a row after this one!
                    workSheet.InsertRow(workSheet.Cells[startingRow + 1, 1].Start.Row, 1);

                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Merge = true;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Font.Bold = true;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Fill.BackgroundColor.SetColor(1, 66, 189, 226);
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Indent = 1;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Value = $"{infoRubriqueGroup.Key}";
                    #endregion

                    foreach (var rebricInfo in infoRubriqueGroup)
                    {
                        var info = demandeAcces.ResultatInfoGenerale.Where(x => x.InfoGeneraleId == rebricInfo.Id).FirstOrDefault();
                        if (info == null)
                        {
                            continue;
                        }
                        startingRow++;

                        #region info General 
                        workSheet.Cells[startingRow, ExigenceColumn, startingRow, ExigenceColumn + 2].Merge = true;
                        workSheet.Cells[startingRow, ExigenceColumn].Value = $"{rebricInfo.Name}";
                        workSheet.Cells[startingRow, ExigenceColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Column(ExigenceColumn).AutoFit();
                        #endregion

                        #region Resultat
                        workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG].Merge = true;
                        workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG + 1].Style.WrapText = true;
                        workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG + 1].AutoFitColumns();
                        workSheet.Cells[startingRow, ResultatColumnInfoG].Value = $"{info.ValueInfo}";
                        workSheet.Column(ResultatColumnInfoG).AutoFit();

                        #endregion
                    }
                }

                startingRow+=2;

                #region Header
                workSheet.Cells[startingRow, ExigenceColumn, startingRow, ExigenceColumn + 5].Merge = true;
                workSheet.Cells[startingRow, ExigenceColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells[startingRow, ExigenceColumn].Value = $"Exigence";
                workSheet.Cells[startingRow, ExigenceColumn + 6].Value = $"Conforme";
                workSheet.Cells[startingRow, ExigenceColumn + 8].Value = $"Observation";
                workSheet.Cells[startingRow, ExigenceColumn + 11].Value = $"Date";
                #endregion

                foreach (var rubrique in typeCheckList.CheckListRubrique.Where(x => x.IsActif == true))
                {
                    startingRow++;

                    #region rubrique 
                    //format the current row's cells
                    for (int celli = minColumn; celli <= maxColumn; celli++)
                    {
                        workSheet.Cells[startingRow, celli].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    //always insert a row after this one!
                    workSheet.InsertRow(workSheet.Cells[startingRow + 1, 1].Start.Row, 1);

                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Merge = true;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Style.Font.Bold = true;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Style.Fill.BackgroundColor.SetColor(1, 156, 204, 101);
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Style.Indent = 1;
                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Value = $"{rubrique.Name}";
                    #endregion
                    foreach (var checkListExigence in rubrique.CheckListExigence.Where(x => x.IsActif == true))
                    {
                        var data = demandeAcces.ResultatExigence.Where(x => x.CheckListExigenceId == checkListExigence.Id).FirstOrDefault();

                        if (data == null)
                        {
                            continue;
                        }
                        startingRow++;

                        #region Exigence 
                        workSheet.Cells[startingRow, ExigenceColumn, startingRow, ExigenceColumn + 5].Merge = true;
                        workSheet.Cells[startingRow, ExigenceColumn].Value = $"{checkListExigence.Name}";
                        workSheet.Cells[startingRow, ExigenceColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Column(ExigenceColumn).AutoFit();
                        #endregion


                        #region Resultat
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn+1].Merge = true;
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].Style.WrapText = true;
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].AutoFitColumns();
                        workSheet.Cells[startingRow, ResultatColumn].Value = data.IsConform ? $"Conforme" : $"Non conforme";
                        workSheet.Cells[startingRow, ResultatColumn + 2].Value = $"{data.Observation}";
                        workSheet.Cells[startingRow, ResultatColumn + 5].Value = data.Date.HasValue ? $"{data.Date.Value.ToString("dd/mm/yyyy")}" : $"{data.Date}";
                        workSheet.Column(ResultatColumn).AutoFit();
                        #endregion

                    }
                }
                result = package.GetAsByteArray();
            }

            return result;



        }

        public string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
    }
}
