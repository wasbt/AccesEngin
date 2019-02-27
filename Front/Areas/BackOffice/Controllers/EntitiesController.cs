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
    public class EntitiesController : BaseController
    {
        // GET: BackOffice/Entities
        public async Task<ActionResult> Index(StandardModel<Entity> model)
        {
            var entity = context.Entity.Include(e => e.AspNetUsers).Include(e => e.Site);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.Entity.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<Entity>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>entity.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await entity.ToListAsync());
        }

        // GET: BackOffice/Entities/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = await context.Entity.FindAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // GET: BackOffice/Entities/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name");
            return View();
        }

        // POST: BackOffice/Entities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Entity entity)
        {
            if (ModelState.IsValid)
            {
                entity.CreatedBy = CurrentUserId;
                entity.CreatedOn = DateTime.Now;
                context.Entity.Add(entity);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", entity.CreatedBy);
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name", entity.SiteId);
            return View(entity);
        }

        // GET: BackOffice/Entities/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = await context.Entity.FindAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", entity.CreatedBy);
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name", entity.SiteId);
            return View(entity);
        }

        // POST: BackOffice/Entities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Entity entity)
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
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name", entity.SiteId);
            return View(entity);
        }

        // GET: BackOffice/Entities/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = await context.Entity.FindAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // POST: BackOffice/Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Entity entity = await context.Entity.FindAsync(id);
            context.Entity.Remove(entity);
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
