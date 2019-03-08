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
    public class NatureMatieresController : BaseController
    {
        // GET: BackOffice/NatureMatieres
        public async Task<ActionResult> Index(StandardModel<NatureMatiere> model)
        {
            var natureMatiere = context.NatureMatiere.Include(n => n.AspNetUsers).Include(n => n.TypeCheckList);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.NatureMatiere.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<NatureMatiere>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>natureMatiere.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await natureMatiere.ToListAsync());
        }

        // GET: BackOffice/NatureMatieres/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NatureMatiere natureMatiere = await context.NatureMatiere.FindAsync(id);
            if (natureMatiere == null)
            {
                return HttpNotFound();
            }
            return View(natureMatiere);
        }

        // GET: BackOffice/NatureMatieres/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: BackOffice/NatureMatieres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NatureMatiere natureMatiere)
        {
            if (ModelState.IsValid)
            {
                natureMatiere.CreatedBy = CurrentUserId;
                natureMatiere.CreatedOn = DateTime.Now;
                context.NatureMatiere.Add(natureMatiere);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", natureMatiere.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", natureMatiere.TypeCheckListId);
            return View(natureMatiere);
        }

        // GET: BackOffice/NatureMatieres/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NatureMatiere natureMatiere = await context.NatureMatiere.FindAsync(id);
            if (natureMatiere == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", natureMatiere.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", natureMatiere.TypeCheckListId);
            return View(natureMatiere);
        }

        // POST: BackOffice/NatureMatieres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NatureMatiere natureMatiere)
        {
            if (ModelState.IsValid)
            {
                natureMatiere.CreatedBy = CurrentUserId;
                natureMatiere.CreatedOn = DateTime.Now;
                context.Entry(natureMatiere).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", natureMatiere.CreatedBy);
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", natureMatiere.TypeCheckListId);
            return View(natureMatiere);
        }

        // GET: BackOffice/NatureMatieres/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NatureMatiere natureMatiere = await context.NatureMatiere.FindAsync(id);
            if (natureMatiere == null)
            {
                return HttpNotFound();
            }
            return View(natureMatiere);
        }

        // POST: BackOffice/NatureMatieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            NatureMatiere natureMatiere = await context.NatureMatiere.FindAsync(id);
            context.NatureMatiere.Remove(natureMatiere);
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
