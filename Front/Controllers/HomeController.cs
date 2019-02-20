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

            #region MyRegion

            var ResultatViewModel = new ResultatsVM
            {
                TypeCheckList = checkList,
                controle = controle,
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

            #region MyRegion

            var controleCheckListVM = new ControleCheckListVM
            {
                TypeCheckList = checkList,
                controle = controle,
            };

            #endregion

            return View(controleCheckListVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewControleResultatCheckList(SaveNewResultatExigence ResultatExigence)
        {
            return null;
        }
        #endregion
    }
}