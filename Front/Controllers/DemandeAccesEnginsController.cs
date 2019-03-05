using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using X.PagedList;
using Front.Models;
using Front.Controllers;
using Shared;
using Front.AGUtils;
using Shared.API.IN;
using Shared.ENUMS;

namespace Front.Controllers
{
    public class DemandeAccesEnginsController : BaseController
    {
        // GET: DemandeAccesEngins
        public async Task<ActionResult> Index(StandardModel<DemandeAccesEngin> model)
        {
            ViewBag.ShowButton = false;

            var demandeAccesEngin = context.DemandeAccesEngin.Include(d => d.AspNetUsers).Include(d => d.TypeCheckList);

            #region Calculer durée approximative du contrôle par type d’engin

            var dateNow = DateTime.Now.Date;
            var demandeAccesEnginToDay = demandeAccesEngin.Where(x => DbFunctions.TruncateTime(x.CreatedOn) == dateNow).ToList();
            var listTypeCheckList = demandeAccesEnginToDay.Select(x => x.TypeCheckList).ToList();
            var getCountTypeEngin = listTypeCheckList.SelectMany(x => x.TypeEngin);
            var selecDureeEstimativeToDay = getCountTypeEngin.Select(x => double.Parse(x.DureeEstimative));
            var sumDuree = selecDureeEstimativeToDay.Sum();


            if (sumDuree >= (long)DureeEstimativeEnums.MaxDuree)
            {
                ViewBag.ShowButton = true;
            }

            #endregion




            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.DemandeAccesEngin.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<DemandeAccesEngin>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderByDescending(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>demandeAccesEngin.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await demandeAccesEngin.ToListAsync());
        }

        // GET: DemandeAccesEngins/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            if (demandeAccesEngin == null)
            {
                return HttpNotFound();
            }
            return View(demandeAccesEngin);
        }

        // GET: DemandeAccesEngins/Create
        public ActionResult Create()
        {
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: DemandeAccesEngins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DemandeAccesEngin demandeAccesEngin, ICollection<ResultatInfoGeneraleModel> ResultatInfoGeneral)
        {
            if (ModelState.IsValid)
            {
                demandeAccesEngin.CreatedBy = CurrentUserId;
                demandeAccesEngin.CreatedOn = DateTime.Now;
                context.DemandeAccesEngin.Add(demandeAccesEngin);
                await context.SaveChangesAsync();

                foreach (var item in ResultatInfoGeneral)
                {
                    var resultatInfoGeneral = new ResultatInfoGenerale()
                    {
                        DemandeAccesEnginId = demandeAccesEngin.Id,
                        InfoGeneraleId = item.GeneralInfoId,
                        ValueInfo = item.ValueInfo,
                        CreatedOn = DateTime.Now,
                    };

                    var addeResultatIinfoGenerale = context.ResultatInfoGenerale.Add(resultatInfoGeneral);
                }

                await context.SaveChangesAsync();

				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            return View(demandeAccesEngin);
        }

        // GET: DemandeAccesEngins/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            if (demandeAccesEngin == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", demandeAccesEngin.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            return View(demandeAccesEngin);
        }

        // POST: DemandeAccesEngins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DemandeAccesEngin demandeAccesEngin)
        {
            if (ModelState.IsValid)
            {
                demandeAccesEngin.CreatedBy = CurrentUserId;
                demandeAccesEngin.CreatedOn = DateTime.Now;
                context.Entry(demandeAccesEngin).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", demandeAccesEngin.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            return View(demandeAccesEngin);
        }

        // GET: DemandeAccesEngins/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            if (demandeAccesEngin == null)
            {
                return HttpNotFound();
            }
            return View(demandeAccesEngin);
        }

        // POST: DemandeAccesEngins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            context.DemandeAccesEngin.Remove(demandeAccesEngin);
            await context.SaveChangesAsync();
            TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Suppression efféctuée avec succès!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
