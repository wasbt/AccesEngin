using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DATAAL;
using X.PagedList;
using Front.Models;
using Front.Controllers;
using Shared;
using Front.AGUtils;

namespace Front.Areas.BackOffice.Controllers
{
    public class TypeCheckListsController : BackOfficeController
    {
        // GET: BackOffice/TypeCheckLists
        public async Task<ActionResult> Index(StandardModel<REF_TypeCheckList> model)
        {
            var typeCheckList = context.REF_TypeCheckList.Include(t => t.AspNetUsers);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.REF_TypeCheckList.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<REF_TypeCheckList>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

			model.resultList = query.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>typeCheckList.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await typeCheckList.ToListAsync());
        }

        // GET: BackOffice/TypeCheckLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_TypeCheckList typeCheckList = await context.REF_TypeCheckList.FindAsync(id);
            if (typeCheckList == null)
            {
                return HttpNotFound();
            }
            return View(typeCheckList);
        }

        // GET: BackOffice/TypeCheckLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/TypeCheckLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(REF_TypeCheckList typeCheckList)
        {
                typeCheckList.CreatedOn = DateTime.Now;
                typeCheckList.CreatedBy = CurrentUserId;
            if (ModelState.IsValid)
            {
                context.REF_TypeCheckList.Add(typeCheckList);
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeCheckList.CreatedBy);
            return View(typeCheckList);
        }

        // GET: BackOffice/TypeCheckLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_TypeCheckList typeCheckList = await context.REF_TypeCheckList.FindAsync(id);
            if (typeCheckList == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeCheckList.CreatedBy);
            return View(typeCheckList);
        }

        // POST: BackOffice/TypeCheckLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(REF_TypeCheckList typeCheckList)
        {
            if (ModelState.IsValid)
            {
                typeCheckList.CreatedOn = DateTime.Now;
                typeCheckList.CreatedBy = CurrentUserId;
                var tt = context.Entry(typeCheckList).State = EntityState.Modified;
                await context.SaveChangesAsync();
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeCheckList.CreatedBy);
            return View(typeCheckList);
        }

        // GET: BackOffice/TypeCheckLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_TypeCheckList typeCheckList = await context.REF_TypeCheckList.FindAsync(id);
            if (typeCheckList == null)
            {
                return HttpNotFound();
            }
            return View(typeCheckList);
        }

        // POST: BackOffice/TypeCheckLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            REF_TypeCheckList typeCheckList = await context.REF_TypeCheckList.FindAsync(id);
            context.REF_TypeCheckList.Remove(typeCheckList);
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
