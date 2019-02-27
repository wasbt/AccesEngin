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
    public class TypeEnginsController : BaseController
    {
        // GET: BackOffice/TypeEngins
        public async Task<ActionResult> Index(StandardModel<TypeEngin> model)
        {
            var typeEngin = context.TypeEngin.Include(t => t.AspNetUsers).Include(t => t.TypeCheckList);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.TypeEngin.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<TypeEngin>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>typeEngin.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await typeEngin.ToListAsync());
        }

        // GET: BackOffice/TypeEngins/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEngin typeEngin = await context.TypeEngin.FindAsync(id);
            if (typeEngin == null)
            {
                return HttpNotFound();
            }
            return View(typeEngin);
        }

        // GET: BackOffice/TypeEngins/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: BackOffice/TypeEngins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TypeEngin typeEngin)
        {
            if (ModelState.IsValid)
            {
                typeEngin.CreatedBy = CurrentUserId;
                typeEngin.CreatedOn = DateTime.Now; 
                context.TypeEngin.Add(typeEngin);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeEngin.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", typeEngin.TypeCheckListId);
            return View(typeEngin);
        }

        // GET: BackOffice/TypeEngins/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEngin typeEngin = await context.TypeEngin.FindAsync(id);
            if (typeEngin == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeEngin.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", typeEngin.TypeCheckListId);
            return View(typeEngin);
        }

        // POST: BackOffice/TypeEngins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TypeEngin typeEngin)
        {
            if (ModelState.IsValid)
            {
                typeEngin.CreatedBy = CurrentUserId;
                typeEngin.CreatedOn = DateTime.Now;
                context.Entry(typeEngin).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeEngin.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", typeEngin.TypeCheckListId);
            return View(typeEngin);
        }

        // GET: BackOffice/TypeEngins/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEngin typeEngin = await context.TypeEngin.FindAsync(id);
            if (typeEngin == null)
            {
                return HttpNotFound();
            }
            return View(typeEngin);
        }

        // POST: BackOffice/TypeEngins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            TypeEngin typeEngin = await context.TypeEngin.FindAsync(id);
            context.TypeEngin.Remove(typeEngin);
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
