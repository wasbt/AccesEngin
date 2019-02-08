using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using X.PagedList;
using Front.Models;
using Front.Controllers;
using Shared.Models;

namespace Front.Areas.BackOffice.Controllers
{
    public class TypeCheckListsController : BaseController
    {
        // GET: BackOffice/TypeCheckLists
        public ActionResult Index(StandardModel<TypeCheckList> model)
        {
            var typeCheckList = context.TypeCheckList.Include(t => t.AspNetUsers);
            return View(typeCheckList.ToList());
        }

        // GET: BackOffice/TypeCheckLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeCheckList typeCheckList = context.TypeCheckList.Find(id);
            if (typeCheckList == null)
            {
                return HttpNotFound();
            }
            return View(typeCheckList);
        }

        // GET: BackOffice/TypeCheckLists/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: BackOffice/TypeCheckLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedOn,CreatedBy")] TypeCheckList typeCheckList)
        {
            if (ModelState.IsValid)
            {
                context.TypeCheckList.Add(typeCheckList);
                context.SaveChanges();
				TempData[ConstsOcpVlr.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeCheckList.CreatedBy);
            return View(typeCheckList);
        }

        // GET: BackOffice/TypeCheckLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeCheckList typeCheckList = context.TypeCheckList.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedOn,CreatedBy")] TypeCheckList typeCheckList)
        {
            if (ModelState.IsValid)
            {
                context.Entry(typeCheckList).State = EntityState.Modified;
                context.SaveChanges();
				TempData[ConstsOcpVlr.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", typeCheckList.CreatedBy);
            return View(typeCheckList);
        }

        // GET: BackOffice/TypeCheckLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeCheckList typeCheckList = context.TypeCheckList.Find(id);
            if (typeCheckList == null)
            {
                return HttpNotFound();
            }
            return View(typeCheckList);
        }

        // POST: BackOffice/TypeCheckLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeCheckList typeCheckList = context.TypeCheckList.Find(id);
            context.TypeCheckList.Remove(typeCheckList);
            context.SaveChanges();
            TempData[ConstsOcpVlr.MESSAGE_SUCCESS] = "Suppression efféctuée avec succès!";
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
