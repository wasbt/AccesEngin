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
using Front.Areas.BackOffice.Models;

namespace Front.Areas.BackOffice.Controllers
{
    public class InfoGeneralesController : BackOfficeController
    {
        // GET: BackOffice/InfoGenerales
        public async Task<ActionResult> Index(StandardModel<InfoGeneraleVM> model)
        {
            var infoGenerale = context.REF_InfoGenerale.Include(i => i.AspNetUsers).Include(i => i.REF_InfoGeneralRubrique);
            int pageSize = 10;
            
			int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

			var query = context.REF_InfoGenerale.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<REF_InfoGenerale>)query.ProcessWhere(model.columnName, model.content);
            }

            var infoGeneraleQuery = query.Select(x => new InfoGeneraleVM
            {
                Id = x.Id,
                Name = x.Name,
                InfoGeneralRubriqueName = x.REF_InfoGeneralRubrique.Name,
                CreatedBy = x.AspNetUsers.Email,
                CreatedOn = x.CreatedOn,
                TypeCheckListNames = x.REF_TypeCheckList.Select(t => t.Name)
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
            REF_InfoGenerale infoGenerale = await context.REF_InfoGenerale.FindAsync(id);
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
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.REF_InfoGeneralRubrique, "Id", "Name");
            ViewBag.TypeCheckListIds = new SelectList(context.REF_TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: BackOffice/InfoGenerales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(REF_InfoGenerale infoGenerale,long[] TypeCheckListIds)
        {
            if (ModelState.IsValid)
            {
                infoGenerale.CreatedBy = CurrentUserId;
                infoGenerale.CreatedOn = DateTime.Now;
                context.REF_InfoGenerale.Add(infoGenerale);
                foreach (var item in TypeCheckListIds)
                {
                    var TypeCheckList = await context.REF_TypeCheckList.FindAsync(item); 
                    infoGenerale.REF_TypeCheckList.Add(TypeCheckList);
                }
                await context.SaveChangesAsync();
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGenerale.CreatedBy);
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.REF_InfoGeneralRubrique, "Id", "Name", infoGenerale.InfoGeneralRubriqueId);
            ViewBag.TypeCheckListIds = new SelectList(context.REF_TypeCheckList, "Id", "Name");

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
            REF_InfoGenerale infoGenerale = await context.REF_InfoGenerale.FindAsync(id);
            if (infoGenerale == null)
            {
                return HttpNotFound();
            }

            var typeChckListSelected = infoGenerale.REF_TypeCheckList;
            var selectedlist = typeChckListSelected.Select(t => t.Id).ToList();
            ViewData["TypeCheckListIds"] = new MultiSelectList(

               items: context.REF_TypeCheckList.OrderBy(o => o.Name),

               dataValueField: "Id",

               dataTextField: "Name",

               selectedValues: selectedlist );
            //ViewBag. = new MultiSelectList(ListTypeCheckList, "Id", "Name", selectedlist);

            
         
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGenerale.CreatedBy);
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.REF_InfoGeneralRubrique, "Id", "Name", infoGenerale.InfoGeneralRubriqueId);
            return View(infoGenerale);
        }

        // POST: BackOffice/InfoGenerales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(REF_InfoGenerale infoGenerale,long[] TypeCheckListIds)
        {
            if (ModelState.IsValid)
            {
                infoGenerale.CreatedBy = CurrentUserId;
                infoGenerale.CreatedOn = DateTime.Now;
                context.Entry(infoGenerale).State = EntityState.Modified;
                //Removed && Added All TypeCheckList for this infoGenerale
                var TypeCheckLists = context.REF_InfoGenerale.Include(w => w.REF_TypeCheckList).Where(x => x.Id == infoGenerale.Id).SingleOrDefault().REF_TypeCheckList;
                TypeCheckLists.Clear();

                foreach (var item in TypeCheckListIds)
                {
                    var TypeCheckList = await context.REF_TypeCheckList.FindAsync(item);
                    infoGenerale.REF_TypeCheckList.Add(TypeCheckList);
                }
                await context.SaveChangesAsync();
               
				TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(context.AspNetUsers, "Id", "Email", infoGenerale.CreatedBy);
            ViewBag.InfoGeneralRubriqueId = new SelectList(context.REF_InfoGeneralRubrique, "Id", "Name", infoGenerale.InfoGeneralRubriqueId);
            return View(infoGenerale);
        }

        // GET: BackOffice/InfoGenerales/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_InfoGenerale infoGenerale = await context.REF_InfoGenerale.FindAsync(id);
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
            REF_InfoGenerale infoGenerale = await context.REF_InfoGenerale.FindAsync(id);
            infoGenerale.REF_TypeCheckList.Clear();
            context.REF_InfoGenerale.Remove(infoGenerale);
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
