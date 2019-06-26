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
    public class EntiteController : BackOfficeController
    {
        // GET: BackOffice/Entite
        public async Task<ActionResult> Index(StandardModel<Entite> model)
        {
            var entity = context.Entite.Include(e => e.AspNetUsers).Include(e => e.Sites);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.Entite.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<Entite>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>entity.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await entity.ToListAsync());
        }

        // GET: BackOffice/Entite/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entite entity = await context.Entite.FindAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // GET: BackOffice/Entite/Create
        public ActionResult Create()
        {
            ViewBag.HSEEntiteUserId = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name");
            return View();
        }

        // POST: BackOffice/Entite/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Entite entity)
        {
            if (ModelState.IsValid)
            {
                entity.CreatedBy = CurrentUserId;
                entity.CreatedOn = DateTime.Now;
                context.Entite.Add(entity);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", entity.CreatedBy);
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name", entity.SiteId);
            return View(entity);
        }

        // GET: BackOffice/Entite/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entite entity = await context.Entite.FindAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.HSEEntiteUserId = new SelectList(context.AspNetUsers, "Id", "Email", entity.CreatedBy);
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name", entity.SiteId);
            return View(entity);
        }

        // POST: BackOffice/Entite/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Entite entity)
        {
            if (ModelState.IsValid)
            {
                entity.CreatedBy = CurrentUserId;
                entity.CreatedOn = DateTime.Now;
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", entity.CreatedBy);
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name", entity.SiteId);
            return View(entity);
        }

        // GET: BackOffice/Entite/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entite entity = await context.Entite.FindAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // POST: BackOffice/Entite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Entite entity = await context.Entite.FindAsync(id);
            context.Entite.Remove(entity);
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
