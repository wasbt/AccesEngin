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
    public class CheckListRubriquesController : BaseController
    {
        // GET: BackOffice/CheckListRubriques
        public async Task<ActionResult> Index(StandardModel<CheckListRubrique> model)
        {
            var checkListRubrique = context.CheckListRubrique.Include(c => c.AspNetUsers).Include(c => c.TypeCheckList);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.CheckListRubrique.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<CheckListRubrique>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>checkListRubrique.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await checkListRubrique.ToListAsync());
        }

        // GET: BackOffice/CheckListRubriques/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckListRubrique checkListRubrique = await context.CheckListRubrique.FindAsync(id);
            if (checkListRubrique == null)
            {
                return HttpNotFound();
            }
            return View(checkListRubrique);
        }

        // GET: BackOffice/CheckListRubriques/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: BackOffice/CheckListRubriques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CheckListRubrique checkListRubrique)
        {
            if (ModelState.IsValid)
            {
                checkListRubrique.CreatedBy = CurrentUserId;
                checkListRubrique.CreatedOn = DateTime.Now;
                context.CheckListRubrique.Add(checkListRubrique);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListRubrique.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", checkListRubrique.TypeCheckListId);
            return View(checkListRubrique);
        }

        // GET: BackOffice/CheckListRubriques/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckListRubrique checkListRubrique = await context.CheckListRubrique.FindAsync(id);
            if (checkListRubrique == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListRubrique.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", checkListRubrique.TypeCheckListId);
            return View(checkListRubrique);
        }

        // POST: BackOffice/CheckListRubriques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TypeCheckListId,Name,ShowOrder,IsActif,CreatedOn,CreatedBy")] CheckListRubrique checkListRubrique)
        {
            if (ModelState.IsValid)
            {
                context.Entry(checkListRubrique).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListRubrique.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", checkListRubrique.TypeCheckListId);
            return View(checkListRubrique);
        }

        // GET: BackOffice/CheckListRubriques/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckListRubrique checkListRubrique = await context.CheckListRubrique.FindAsync(id);
            if (checkListRubrique == null)
            {
                return HttpNotFound();
            }
            return View(checkListRubrique);
        }

        // POST: BackOffice/CheckListRubriques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            CheckListRubrique checkListRubrique = await context.CheckListRubrique.FindAsync(id);
            context.CheckListRubrique.Remove(checkListRubrique);
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
