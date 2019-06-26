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
    public class CheckListRubriquesController : BackOfficeController
    {
        // GET: BackOffice/CheckListRubriques
        public async Task<ActionResult> Index(StandardModel<REF_CheckListRubrique> model)
        {
            var checkListRubrique = context.REF_CheckListRubrique.Include(c => c.AspNetUsers).Include(c => c.REF_TypeCheckList);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.REF_CheckListRubrique.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<REF_CheckListRubrique>)query.ProcessWhere(model.columnName, model.content);
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
            REF_CheckListRubrique checkListRubrique = await context.REF_CheckListRubrique.FindAsync(id);
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
            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: BackOffice/CheckListRubriques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(REF_CheckListRubrique checkListRubrique)
        {
            if (ModelState.IsValid)
            {
                checkListRubrique.CreatedBy = CurrentUserId;
                checkListRubrique.CreatedOn = DateTime.Now;
                context.REF_CheckListRubrique.Add(checkListRubrique);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListRubrique.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", checkListRubrique.TypeCheckListId);
            return View(checkListRubrique);
        }

        // GET: BackOffice/CheckListRubriques/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CheckListRubrique checkListRubrique = await context.REF_CheckListRubrique.FindAsync(id);
            if (checkListRubrique == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListRubrique.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", checkListRubrique.TypeCheckListId);
            return View(checkListRubrique);
        }

        // POST: BackOffice/CheckListRubriques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(REF_CheckListRubrique checkListRubrique)
        {
            if (ModelState.IsValid)
            {
                checkListRubrique.CreatedBy = CurrentUserId;
                checkListRubrique.CreatedOn = DateTime.Now;
                context.Entry(checkListRubrique).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", checkListRubrique.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", checkListRubrique.TypeCheckListId);
            return View(checkListRubrique);
        }

        // GET: BackOffice/CheckListRubriques/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CheckListRubrique checkListRubrique = await context.REF_CheckListRubrique.FindAsync(id);
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
            REF_CheckListRubrique checkListRubrique = await context.REF_CheckListRubrique.FindAsync(id);
            context.REF_CheckListRubrique.Remove(checkListRubrique);
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
