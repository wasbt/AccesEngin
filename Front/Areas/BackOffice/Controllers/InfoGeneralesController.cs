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
using Front.Areas.BackOffice.Models;

namespace Front.Areas.BackOffice.Controllers
{
    public class InfoGeneralesController : BackOfficeController
    {
        // GET: BackOffice/InfoGenerales
        public async Task<ActionResult> Index(StandardModel<InfoGeneraleVM> model)
        {
            var infoGenerale = context.InfoGenerale.Include(i => i.AspNetUsers).Include(i => i.InfoGeneralRubrique);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.InfoGenerale.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<InfoGenerale>)query.ProcessWhere(model.columnName, model.content);
            }

            var infoGeneraleQuery = query.Select(x => new InfoGeneraleVM
            {
                Id = x.Id,
                Name = x.Name,
                InfoGeneralRubriqueName = x.InfoGeneralRubrique.Name,
                CreatedBy = x.AspNetUsers.Email,
                CreatedOn = x.CreatedOn,
                TypeCheckListNames = x.TypeCheckList.Select(t => t.Name)
            });
            infoGeneraleQuery = infoGeneraleQuery.OrderBy(x => x.Id);

			model.resultList = infoGeneraleQuery.ToPagedList(pageNumber, pageSize);
            
			ViewBag.Log = query.ToString();

            return View(model);

			// V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>infoGenerale.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await infoGenerale.ToListAsync());
        }

        // GET: BackOffice/InfoGenerales/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoGenerale infoGenerale = await context.InfoGenerale.FindAsync(id);
            if (infoGenerale == null)
            {
                return HttpNotFound();
            }
            return View(infoGenerale);
        }

        // GET: BackOffice/InfoGenerales/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email");
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.InfoGeneralRubrique, "Id", "Name");
            ViewBag.TypeCheckListIds = new SelectList(context.TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: BackOffice/InfoGenerales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InfoGenerale infoGenerale,long[] TypeCheckListIds)
        {
            if (ModelState.IsValid)
            {
                infoGenerale.CreatedBy = CurrentUserId;
                infoGenerale.CreatedOn = DateTime.Now;
                context.InfoGenerale.Add(infoGenerale);
                foreach (var item in TypeCheckListIds)
                {
                    var TypeCheckList = await context.TypeCheckList.FindAsync(item); 
                    infoGenerale.TypeCheckList.Add(TypeCheckList);
                }
                await context.SaveChangesAsync();
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGenerale.CreatedBy);
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.InfoGeneralRubrique, "Id", "Name", infoGenerale.InfoGeneralRubriqueId);
            ViewBag.TypeCheckListIds = new SelectList(context.TypeCheckList, "Id", "Name");

            return View(infoGenerale);
        }

        // GET: BackOffice/InfoGenerales/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var ListTypeCheckList = new SelectList(context.TypeCheckList, "Id", "Name");
            InfoGenerale infoGenerale = await context.InfoGenerale.FindAsync(id);
            if (infoGenerale == null)
            {
                return HttpNotFound();
            }

            var typeChckListSelected = infoGenerale.TypeCheckList;
            var selectedlist = typeChckListSelected.Select(t => t.Id).ToList();
            ViewData["TypeCheckListIds"] = new MultiSelectList(

               items: context.TypeCheckList.OrderBy(o => o.Name),

               dataValueField: "Id",

               dataTextField: "Name",

               selectedValues: selectedlist );
            //ViewBag. = new MultiSelectList(ListTypeCheckList, "Id", "Name", selectedlist);

            
         
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGenerale.CreatedBy);
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.InfoGeneralRubrique, "Id", "Name", infoGenerale.InfoGeneralRubriqueId);
            return View(infoGenerale);
        }

        // POST: BackOffice/InfoGenerales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InfoGenerale infoGenerale,long[] TypeCheckListIds)
        {
            if (ModelState.IsValid)
            {
                infoGenerale.CreatedBy = CurrentUserId;
                infoGenerale.CreatedOn = DateTime.Now;
                context.Entry(infoGenerale).State = EntityState.Modified;
                //Removed && Added All TypeCheckList for this infoGenerale
                var TypeCheckLists = context.InfoGenerale.Include(w => w.TypeCheckList).Where(x => x.Id == infoGenerale.Id).SingleOrDefault().TypeCheckList;
                TypeCheckLists.Clear();

                foreach (var item in TypeCheckListIds)
                {
                    var TypeCheckList = await context.TypeCheckList.FindAsync(item);
                    infoGenerale.TypeCheckList.Add(TypeCheckList);
                }
                await context.SaveChangesAsync();
               
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGenerale.CreatedBy);
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.InfoGeneralRubrique, "Id", "Name", infoGenerale.InfoGeneralRubriqueId);
            return View(infoGenerale);
        }

        // GET: BackOffice/InfoGenerales/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoGenerale infoGenerale = await context.InfoGenerale.FindAsync(id);
            if (infoGenerale == null)
            {
                return HttpNotFound();
            }
            return View(infoGenerale);
        }

        // POST: BackOffice/InfoGenerales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            InfoGenerale infoGenerale = await context.InfoGenerale.FindAsync(id);
            infoGenerale.TypeCheckList.Clear();
            context.InfoGenerale.Remove(infoGenerale);
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
