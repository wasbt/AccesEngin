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
    public class SitesController : BackOfficeController
    {
        // GET: BackOffice/Sites
        public async Task<ActionResult> Index(StandardModel<Site> model)
        {
            var site = context.Site.Include(s => s.AspNetUsers);
            int pageSize = 10;

            int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

            var query = context.Site.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<Site>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

            model.resultList = query.ToPagedList(pageNumber, pageSize);

            ViewBag.Log = query.ToString();

            return View(model);

            // V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>site.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await site.ToListAsync());
        }

        // GET: BackOffice/Sites/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = await context.Site.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // GET: BackOffice/Sites/Create
        public ActionResult Create()
        {
            ViewBag.HSESiteId = new SelectList(context.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: BackOffice/Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Site site)
        {
            if (ModelState.IsValid)
            {
                site.CreatedBy = CurrentUserId;
                site.CreatedOn = DateTime.Now;
                context.Site.Add(site);
                await context.SaveChangesAsync();
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.HSESiteId = new SelectList(context.AspNetUsers, "Id", "Email", site.HSESiteId);
            return View(site);
        }

        // GET: BackOffice/Sites/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = await context.Site.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", site.CreatedBy);
            return View(site);
        }

        // POST: BackOffice/Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Site site)
        {
            if (ModelState.IsValid)
            {
                site.CreatedBy = CurrentUserId;
                site.CreatedOn = DateTime.Now;
                context.Entry(site).State = EntityState.Modified;
                await context.SaveChangesAsync();
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", site.CreatedBy);
            return View(site);
        }

        // GET: BackOffice/Sites/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = await context.Site.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: BackOffice/Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Site site = await context.Site.FindAsync(id);
            context.Site.Remove(site);
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
