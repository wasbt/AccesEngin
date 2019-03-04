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

namespace Front.Areas.BackOffice.Controllers
{
    public class CheckListExigencesController : BackOfficeController
    {
        // GET: BackOffice/CheckListExigences
        public async Task<ActionResult> Index(StandardModel<CheckListExigence> model)
        {
            var checkListExigence = context.CheckListExigence.Include(c => c.AspNetUsers).Include(c => c.CheckListRubrique);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.CheckListExigence.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<CheckListExigence>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>checkListExigence.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await checkListExigence.ToListAsync());
        }

        // GET: BackOffice/CheckListExigences/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckListExigence checkListExigence = await context.CheckListExigence.FindAsync(id);
            if (checkListExigence == null)
            {
                return HttpNotFound();
            }
            return View(checkListExigence);
        }

        // GET: BackOffice/CheckListExigences/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.CheckListRubriqueId = new SelectList(context.CheckListRubrique, "Id", "Name");
            return View();
        }

        // POST: BackOffice/CheckListExigences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CheckListExigence checkListExigence)
        {
            if (ModelState.IsValid)
            {
                checkListExigence.CreatedBy = CurrentUserId;
                checkListExigence.CreatedOn = DateTime.Now;
                context.CheckListExigence.Add(checkListExigence);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListExigence.CreatedBy);
            ViewBag.CheckListRubriqueId = new SelectList(context.CheckListRubrique, "Id", "Name", checkListExigence.CheckListRubriqueId);
            return View(checkListExigence);
        }

        // GET: BackOffice/CheckListExigences/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckListExigence checkListExigence = await context.CheckListExigence.FindAsync(id);
            if (checkListExigence == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListExigence.CreatedBy);
            ViewBag.CheckListRubriqueId = new SelectList(context.CheckListRubrique, "Id", "Name", checkListExigence.CheckListRubriqueId);
            return View(checkListExigence);
        }

        // POST: BackOffice/CheckListExigences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CheckListExigence checkListExigence)
        {
            if (ModelState.IsValid)
            {
                checkListExigence.CreatedBy = CurrentUserId;
                checkListExigence.CreatedOn = DateTime.Now;
                context.Entry(checkListExigence).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListExigence.CreatedBy);
            ViewBag.CheckListRubriqueId = new SelectList(context.CheckListRubrique, "Id", "Name", checkListExigence.CheckListRubriqueId);
            return View(checkListExigence);
        }

        // GET: BackOffice/CheckListExigences/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckListExigence checkListExigence = await context.CheckListExigence.FindAsync(id);
            if (checkListExigence == null)
            {
                return HttpNotFound();
            }
            return View(checkListExigence);
        }

        // POST: BackOffice/CheckListExigences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            CheckListExigence checkListExigence = await context.CheckListExigence.FindAsync(id);
            context.CheckListExigence.Remove(checkListExigence);
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
