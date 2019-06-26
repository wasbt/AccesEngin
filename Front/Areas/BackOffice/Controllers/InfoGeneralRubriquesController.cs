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
using PagedList;
using Front.Models;
using Front.Controllers;
using Shared;
using Front.AGUtils;

namespace Front.Areas.BackOffice.Controllers
{
    public class InfoGeneralRubriquesController : BackOfficeController
    {
        // GET: BackOffice/InfoGeneralRubriques
        public async Task<ActionResult> Index(StandardModel<REF_InfoGeneralRubrique> model)
        {
            var infoGeneralRubrique = context.REF_InfoGeneralRubrique.Include(i => i.AspNetUsers);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.REF_InfoGeneralRubrique.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<REF_InfoGeneralRubrique>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>infoGeneralRubrique.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await infoGeneralRubrique.ToListAsync());
        }

        // GET: BackOffice/InfoGeneralRubriques/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_InfoGeneralRubrique infoGeneralRubrique = await context.REF_InfoGeneralRubrique.FindAsync(id);
            if (infoGeneralRubrique == null)
            {
                return HttpNotFound();
            }
            return View(infoGeneralRubrique);
        }

        // GET: BackOffice/InfoGeneralRubriques/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: BackOffice/InfoGeneralRubriques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( REF_InfoGeneralRubrique infoGeneralRubrique)
        {
            if (ModelState.IsValid)
            {
                infoGeneralRubrique.CreatedBy = CurrentUserId;
                infoGeneralRubrique.CreatedOn = DateTime.Now;
                context.REF_InfoGeneralRubrique.Add(infoGeneralRubrique);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGeneralRubrique.CreatedBy);
            return View(infoGeneralRubrique);
        }

        // GET: BackOffice/InfoGeneralRubriques/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_InfoGeneralRubrique infoGeneralRubrique = await context.REF_InfoGeneralRubrique.FindAsync(id);
            if (infoGeneralRubrique == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGeneralRubrique.CreatedBy);
            return View(infoGeneralRubrique);
        }

        // POST: BackOffice/InfoGeneralRubriques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(REF_InfoGeneralRubrique infoGeneralRubrique)
        {
            if (ModelState.IsValid)
            {
                infoGeneralRubrique.CreatedBy = CurrentUserId;
                infoGeneralRubrique.CreatedOn = DateTime.Now;
                context.Entry(infoGeneralRubrique).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGeneralRubrique.CreatedBy);
            return View(infoGeneralRubrique);
        }

        // GET: BackOffice/InfoGeneralRubriques/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_InfoGeneralRubrique infoGeneralRubrique = await context.REF_InfoGeneralRubrique.FindAsync(id);
            if (infoGeneralRubrique == null)
            {
                return HttpNotFound();
            }
            return View(infoGeneralRubrique);
        }

        // POST: BackOffice/InfoGeneralRubriques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            REF_InfoGeneralRubrique infoGeneralRubrique = await context.REF_InfoGeneralRubrique.FindAsync(id);
            context.REF_InfoGeneralRubrique.Remove(infoGeneralRubrique);
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
