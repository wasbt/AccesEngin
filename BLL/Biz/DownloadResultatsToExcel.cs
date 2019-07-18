using BLL.Common;
using DAL;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class DownloadResultatsToExcel : CommonBiz
    {
        public DownloadResultatsToExcel(OcpPerformanceDataContext context, ILog log) : base(context, log)
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




            var heading = "si";
            var minColumn = 2;
            var maxColumnInfoG = 7;
            var maxColumn = 14;
            var startingRowHeadr = 2;
            var startingRow = 5;
            var ExigenceColumn = 2;
            var ResultatColumnInfoG = 5;
            var ResultatColumn = 8;
            var blue = ColorTranslator.FromHtml("#42bde2");
            var green = ColorTranslator.FromHtml("#9ccc65");
            var orange = ColorTranslator.FromHtml("#F9C851");
            var succes = ColorTranslator.FromHtml("#10C469");
            var alert = ColorTranslator.FromHtml("#EA4335");

            #region Generatre Excel

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 3 : 3;

                workSheet.InsertRow(workSheet.Cells[startingRow + 1, minColumn, startingRow, maxColumnInfoG].Start.Row, 1, copyStylesFromRow: 1);


                if (demandeAcces.IsAutorise)
                {
                    DrawnRubrique(minColumn, minColumn, startingRowHeadr, workSheet, succes);
                    workSheet.Cells[startingRowHeadr, minColumn].Value = $"Autorisé";
                }
                else
                {
                    DrawnRubrique(minColumn, minColumn, startingRowHeadr, workSheet, alert);
                    workSheet.Cells[startingRowHeadr, minColumn].Value = $"Non Autorisé";
                }

                DrawnRubrique(minColumn, maxColumnInfoG, startingRow, workSheet, blue);

                workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Value = $"Type d'engin";
                startingRow++;

                workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG].Merge = true;
                workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG + 1].Style.WrapText = true;
                workSheet.Cells[startingRow, ResultatColumnInfoG, startingRow, ResultatColumnInfoG + 1].AutoFitColumns();
                workSheet.Cells[startingRow, minColumn].Value = $"{demandeAcces.REF_TypeEngin.Name}";
                startingRow++;
                foreach (var infoRubriqueGroup in typeCheckList.REF_InfoGenerale.GroupBy(g => g.REF_InfoGeneralRubrique.Name))
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
                    DrawnRubrique(minColumn, maxColumnInfoG, startingRow, workSheet, blue);

                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Value = $"{infoRubriqueGroup.Key}";
                    #endregion

                    foreach (var rebricInfo in infoRubriqueGroup)
                    {
                        var info = demandeAcces.DemandeAccesEnginInfoGeneraleValue.Where(x => x.InfoGeneraleId == rebricInfo.Id).FirstOrDefault();
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

                startingRow += 2;

                #region Header
                workSheet.Cells[startingRow, ExigenceColumn, startingRow, ExigenceColumn + 5].Merge = true;
                workSheet.Cells[startingRow, ExigenceColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells[startingRow, ExigenceColumn].Value = $"Exigence";
                workSheet.Cells[startingRow, ExigenceColumn + 6].Value = $"Conforme";
                workSheet.Cells[startingRow, ExigenceColumn + 8].Value = $"Observation";
                workSheet.Cells[startingRow, ExigenceColumn + 11].Value = $"Date";
                #endregion

                foreach (var rubrique in typeCheckList.REF_CheckListRubrique.Where(x => x.IsActif == true))
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

                    DrawnRubrique(minColumn, maxColumn, startingRow, workSheet, green);

                    workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Value = $"{rubrique.Name}";
                    #endregion
                    foreach (var checkListExigence in rubrique.REF_CheckListExigence.Where(x => x.IsActif == true))
                    {
                        var data = resultatEntete.ResultatControleDetail.Where(x => x.CheckListExigenceId == checkListExigence.Id).FirstOrDefault();

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
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].Merge = true;
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].Style.WrapText = true;
                        workSheet.Cells[startingRow, ResultatColumn, startingRow, ResultatColumn + 1].AutoFitColumns();
                        workSheet.Cells[startingRow, ResultatColumn].Value = data.IsConform ? $"Conforme" : $"Non conforme";
                        workSheet.Cells[startingRow, ResultatColumn + 2].Value = $"{data.Observation}";
                        workSheet.Cells[startingRow, ResultatColumn + 5].Value = data.DateExpiration.HasValue ? $"{data.DateExpiration.Value.ToString("dd/mm/yyyy")}" : $"{data.DateExpiration}";
                        workSheet.Column(ResultatColumn).AutoFit();
                        #endregion

                    }
                }
                startingRow++;
                DrawnRubrique(minColumn, maxColumn, startingRow, workSheet, orange);
                workSheet.Cells[startingRow, minColumn, startingRow, maxColumn].Value = $"TAUX DE CONFORMITE";

                startingRow++;
                workSheet.Cells[startingRow, ExigenceColumn].Value = $"Conforme";
                workSheet.Cells[startingRow, ResultatColumn].Value = $"{exigencesApplicable} %";

                startingRow++;
                workSheet.Cells[startingRow, ExigenceColumn].Value = $"Non conforme";
                workSheet.Cells[startingRow, ResultatColumn].Value = $"{exigencesNonApplicable} %";




                result = package.GetAsByteArray();
            }

            #endregion

            return result;



        }

        private static void DrawnRubrique(int minColumn, int maxColumnInfoG, int startingRow, ExcelWorksheet workSheet, Color? color = null)
        {


            workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Merge = true;
            workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Font.Bold = true;
            workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Fill.PatternType = ExcelFillStyle.Solid;
            if (color.HasValue)
            {
                workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Fill.BackgroundColor.SetColor(color.Value);
            }
            workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Cells[startingRow, minColumn, startingRow, maxColumnInfoG].Style.Indent = 1;
        }

        public string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        public async Task<byte[]> GetStatistqueToExcelAsync(DateTime? dateStart, DateTime? dateEnd)
        {
            byte[] result = null;

            #region get All demandes
            var demandes = context.DemandeAccesEngin.AsQueryable();
            demandes = demandes.Where(x => x.ResultatControleEntete.Any());
            if (dateStart.HasValue && dateEnd.HasValue)
            {
                demandes = demandes.Where(x => x.CreatedOn >= dateStart && x.CreatedOn < dateEnd);
            }
            #endregion

            var heading = "si";
            var minColumn = 1;
            var maxColumnInfoG = 7;
            var startingRow = 1;
            var index = 1;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 1;
                workSheet.InsertRow(workSheet.Cells[startingRow + 1, minColumn, startingRow, maxColumnInfoG].Start.Row, 1, copyStylesFromRow: 1);

                foreach (var demande in demandes)
                {
                    workSheet.Cells[startingRow, minColumn + 1].Value = demande.Entite.Name;
                    workSheet.Cells[startingRow, minColumn + 2].Value = demande.AspNetUsers.Profile.FullName;
                    workSheet.Cells[startingRow, minColumn + 3].Value = demande.Id;
                    workSheet.Cells[startingRow, minColumn + 4].Value = demande.REF_TypeEngin.Name;
                    workSheet.Cells[startingRow, minColumn + 5].Value = demande.Observation;
                    workSheet.Cells[startingRow, minColumn + 6].Value = demande.IsAutorise ? "Autorise" : "Non autorise";
                    workSheet.Cells[startingRow, minColumn + 7].Value = demande?.REF_NatureMatiere?.Name;

                    var entite = demande.Entite.Name;
                    var chefprojet = demande.AspNetUsers.Profile.FullName;
                    var nCommande = demande.Id;
                    var typeEngin = demande.REF_TypeEngin.Name;
                    var resultatControleDetail = demande.ResultatControleEntete.SelectMany(x => x.ResultatControleDetail).ToList();
                    var groupByRub = resultatControleDetail.GroupBy(x => x.REF_CheckListExigence.REF_CheckListRubrique.Name);
                    foreach (var items in groupByRub)
                    {
                        index = 8;
                        workSheet.Cells[startingRow, minColumn + index].Value = items.Key;
                        foreach (var item in items)
                        {
                            startingRow++;
                            workSheet.Cells[startingRow, minColumn + index].Value = item.REF_CheckListExigence.Name;
                            workSheet.Cells[startingRow, minColumn + index + 2].Value = item.IsConform ? "Conforme" : "Non conforme";
                            workSheet.Cells[startingRow, minColumn + index + 3].Value = item.DateExpiration?.ToString("dd/MM/yyyy");
                            var label = item.REF_CheckListExigence.Name;
                            var isConform = item.IsConform;
                        }
                        index++;
                        startingRow++;
                    }
                    var isAutorise = demande.IsAutorise;
                    var observation = demande.Observation;
                    startingRow++;
                }
                result = package.GetAsByteArray();

            }
            return result;

        }
    }
}
