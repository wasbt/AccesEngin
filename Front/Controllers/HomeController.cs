using Front.ViewModels;
using Shared.API.IN;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using BLL.Biz;
using Shared;

namespace Front.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        #region Resultats
        public async Task<ActionResult> Resultats(int id)
        {
            #region Check Controle id & find it
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var controle = await context.DemandeAccesEngin.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }

            #endregion

            #region get & check checklist
            var checkList = await context.TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();
            if (checkList == null)
            {
                return HttpNotFound();
            }
            #endregion

            #region get & Type Engin
            var typeEngin = await context.TypeEngin.Where(x => x.Id == controle.TypeEnginId).FirstOrDefaultAsync();

            if (typeEngin == null)
            {
                return HttpNotFound();
            }
            #endregion

            #region Conformité
            var resultatExigenceDetail = controle.ResultatExigence.ToList();
            var exigences = controle.ResultatExigence.ToList();
            var exigenceNonApplicable = resultatExigenceDetail.Where(x => !x.IsConform).ToList();
            var exigencesNonApplicableCount = exigenceNonApplicable.LongCount();
            var exigenceApplicable = resultatExigenceDetail.Where(x => x.IsConform).ToList();
            var exigencesApplicableCount = exigenceApplicable.LongCount();

            var Total = (exigencesApplicableCount + exigencesNonApplicableCount);

            var exigencesApplicable = (exigencesApplicableCount * 100) / Total;
            var exigencesNonApplicable = (exigencesNonApplicableCount * 100) / Total;

            #endregion

            #region Resultats Model
            //To Model
            var ResultatViewModel = new ResultatsVM
            {
                TypeCheckList = checkList,
                controle = controle,
                TypeEngin = typeEngin,
                exigencesApplicable = exigencesApplicable,
                exigencesNonApplicable = exigencesNonApplicable
            };

            #endregion

            return View(ResultatViewModel);
        }

        public async Task<ActionResult> NewControleResultatCheckList(int id)
        {

            #region Check Controle id & find it
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var controle = await context.DemandeAccesEngin.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }

            #endregion

            #region get & check checklist
            var checkList = await context.TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();

            if (checkList == null)
            {
                return HttpNotFound();
            }
            #endregion 


            #region Model New Controle

            var controleCheckListVM = new ResultatsVM
            {
                TypeCheckList = checkList,
                controle = controle,
                TypeEngin = controle.TypeEngin
            };

            #endregion

            return View(controleCheckListVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewControleResultatCheckList(SaveNewResultatExigence ResultatExigence)
        {
            foreach (var resultatEx in ResultatExigence.ResultatExigenceList)
            {
                var controlResultatExigence = new ResultatExigence()
                {
                    DemandeAccesEnginId = ResultatExigence.DemandeAccesEnginId,
                    CheckListExigenceId = resultatEx.CheckListExigenceId,
                    IsConform = resultatEx.IsConform,
                    Date = string.IsNullOrEmpty(resultatEx.Date) ? (DateTime?)null : Convert.ToDateTime(resultatEx.Date),
                    Observation = resultatEx.Observation,
                    CreatedOn = DateTime.Now
                };

                var addedMissionResultatExigence = context.ResultatExigence.Add(controlResultatExigence);
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "DemandeAccesEngins");
        }
        #endregion

        [ActionName("DownloadExcel")]
        public async Task<ActionResult> DownloadExcelAsync(int? id)
        {
            var cc = new DownloadResultatsToExcel(context, log);
            var toto = await cc.GetResultatToExcelAsync(id); ;
           
            if (toto != null)
            {
                byte[] filecontent = toto;
                return File(filecontent,cc.ExcelContentType, $"Textes.xlsx");
            }
            else
            {
                TempData[ConstsAccesEngin.MESSAGE_ERROR] = "Erreur telechargement fichier Excel.";
                return RedirectToAction("Index");
            }

        }
    }
}