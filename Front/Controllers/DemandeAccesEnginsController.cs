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
using Shared.API.IN;
using Shared.ENUMS;
using BLL.Common;
using System.IO;
using BLL.Biz;

namespace Front.Controllers
{
    [Authorize(Roles = ConstsAccesEngin.ROLE_CHEFPROJET + "," + ConstsAccesEngin.ROLE_CONTROLEUR)]
    public class DemandeAccesEnginsController : BaseController
    {
        // GET: DemandeAccesEngins
        public async Task<ActionResult> Index(StandardModel<DemandeAccesEngin> model)
        {

            var demandeAccesEngin = context.DemandeAccesEngin.Include(d => d.AspNetUsers).Include(d => d.TypeCheckList);
            var query = context.DemandeAccesEngin.AsQueryable();

            if (IsChefProjet)
            {
                query = query.Where(x => x.CreatedBy == CurrentUserId);
            }
            if (IsConroleur)
            {
                query = query.Where(x => !x.DemandeResultatEntete.Any());
            }
            int pageSize = 10;

            int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);


            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<DemandeAccesEngin>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderByDescending(x => x.Id);

            model.resultList = query.ToPagedList(pageNumber, pageSize);

            ViewBag.Log = query.ToString();

            return View(model);

            // V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>demandeAccesEngin.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await demandeAccesEngin.ToListAsync());
        }

        // GET: DemandeAccesEngins/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            if (demandeAccesEngin == null)
            {
                return HttpNotFound();
            }
            return View(demandeAccesEngin);
        }

        [Authorize(Roles = ConstsAccesEngin.ROLE_CHEFPROJET)]
        // GET: DemandeAccesEngins/Create
        public ActionResult Create()
        {
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name");
            return View();
        }

        // POST: DemandeAccesEngins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DemandeAccesEngin demandeAccesEngin, ICollection<ResultatInfoGeneraleModel> ResultatInfoGeneral, HttpPostedFileBase file)
        {
            long fileId = 0;
            var biz = new CommonBiz(context, log);
            if (ModelState.IsValid)
            {



                #region Calculer durée approximative du contrôle par type d’engin

                var demandeAccesEnginQuery = context.DemandeAccesEngin;
                var dateNow = DateTime.Now.Date;
                var tomorrow = dateNow.AddDays(1);
                var demandeAccesEnginPlannig = demandeAccesEnginQuery.Where(x => DbFunctions.TruncateTime(x.DatePlannification) == demandeAccesEngin.DatePlannification).ToList();
                var listTypeCheckList = demandeAccesEnginPlannig.Select(x => x.TypeCheckList).ToList();
                var getCountTypeEngin = listTypeCheckList.SelectMany(x => x.TypeEngin);
                var selecDureeEstimativeToDay = getCountTypeEngin.Select(x => double.Parse(x.DureeEstimative));
                var sumDuree = selecDureeEstimativeToDay.Sum();


                if (sumDuree >= (long)DureeEstimativeEnums.MaxDuree)
                {
                    TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "s'il vous plaît choisir une autre date de planification!";
                    ViewBag.SiteId = new SelectList(context.Site, "Id", "Name");
                    ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
                    return View(demandeAccesEngin);
                }

                #endregion



                demandeAccesEngin.CreatedBy = CurrentUserId;
                demandeAccesEngin.CreatedOn = DateTime.Now;
                demandeAccesEngin.Autorise = false;
                context.DemandeAccesEngin.Add(demandeAccesEngin);
                await context.SaveChangesAsync();



                foreach (var item in ResultatInfoGeneral)
                {
                    var resultatInfoGeneral = new ResultatInfoGenerale()
                    {
                        DemandeAccesEnginId = demandeAccesEngin.Id,
                        InfoGeneraleId = item.GeneralInfoId,
                        ValueInfo = item.ValueInfo,
                        CreatedOn = DateTime.Now,
                    };

                    var addeResultatIinfoGenerale = context.ResultatInfoGenerale.Add(resultatInfoGeneral);
                }

                await context.SaveChangesAsync();

                #region case row has file
                if (file != null)
                {
                    //add file to database
                    fileId = await biz.SaveOCPFile(file, ContainerName, demandeAccesEngin.Id, SourceName);

                    //verify if file was added
                    if (fileId == 0)
                    {
                        return HttpNotFound();
                    }
                    demandeAccesEngin.AppFileId = fileId;
                }
                #endregion

                await context.SaveChangesAsync();
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            return View(demandeAccesEngin);
        }

        // GET: DemandeAccesEngins/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            if (demandeAccesEngin == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name", demandeAccesEngin.Entity.SiteId);
            ViewBag.EntityId = new SelectList(context.Entity.Where(e => e.SiteId == demandeAccesEngin.Entity.SiteId), "Id", "Name", demandeAccesEngin.EntityId);

            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            ViewBag.TypeEnginId = new SelectList(context.TypeEngin.Where(t => t.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.TypeEnginId);
            ViewBag.NatureMatiereId = new SelectList(context.NatureMatiere.Where(n => n.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.NatureMatiereId);
            return View(demandeAccesEngin);
        }

        // POST: DemandeAccesEngins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DemandeAccesEngin demandeAccesEngin, ICollection<ResultatInfoGeneraleModel> ResultatInfoGeneral, HttpPostedFileBase file)
        {
            var biz = new DemandeAccesBiz(context, log);
            if (ModelState.IsValid)
            {
                demandeAccesEngin.CreatedBy = CurrentUserId;
                demandeAccesEngin.StatutDemandeId = null;
                demandeAccesEngin.CreatedOn = DateTime.Now;
                context.Entry(demandeAccesEngin).State = EntityState.Modified;
               var oldResultatInfoGenerale = context.ResultatInfoGenerale.Where(x => x.DemandeAccesEnginId == demandeAccesEngin.Id);

                foreach (var olditem in oldResultatInfoGenerale)
                {
                    context.ResultatInfoGenerale.Remove(olditem);
                }

                foreach (var item in ResultatInfoGeneral)
                {
                    var resultatInfoGeneral = new ResultatInfoGenerale()
                    {
                        DemandeAccesEnginId = demandeAccesEngin.Id,
                        InfoGeneraleId = item.GeneralInfoId,
                        ValueInfo = item.ValueInfo,
                        CreatedOn = DateTime.Now,
                    };

                    var addeResultatIinfoGenerale = context.ResultatInfoGenerale.Add(resultatInfoGeneral);
                }
                await context.SaveChangesAsync();

                #region case row has file
                if (file != null)
                {
                    long fileId = await biz.removeOldFile(demandeAccesEngin, file, ContainerName, demandeAccesEngin.Id.ToString());

                    //verify if file was added
                    if (fileId == 0)
                    {
                        return HttpNotFound();
                    }

                    //add fileId to data
                    demandeAccesEngin.AppFileId = fileId;
                }
                #endregion
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.SiteId = new SelectList(context.Site, "Id", "Name", demandeAccesEngin.Entity.SiteId);
            ViewBag.EntityId = new SelectList(context.Entity.Where(e => e.SiteId == demandeAccesEngin.Entity.SiteId), "Id", "Name", demandeAccesEngin.EntityId);

            ViewBag.TypeCheckListId = new SelectList(context.TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            ViewBag.TypeEnginId = new SelectList(context.TypeEngin.Where(t => t.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.TypeEnginId);
            ViewBag.NatureMatiereId = new SelectList(context.NatureMatiere.Where(n => n.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.NatureMatiereId);
            return View(demandeAccesEngin);
        }

        // GET: DemandeAccesEngins/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            if (demandeAccesEngin == null)
            {
                return HttpNotFound();
            }
            return View(demandeAccesEngin);
        }

        // POST: DemandeAccesEngins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(id);
            context.DemandeAccesEngin.Remove(demandeAccesEngin);
            await context.SaveChangesAsync();
            TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Suppression efféctuée avec succès!";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> MyDemande(StandardModel<DemandeAccesEngin> model)
        {
            ViewBag.ShowButton = false;

            var query = context.DemandeAccesEngin.AsQueryable();

            if (IsChefProjet)
            {
                query = query.Where(x => x.CreatedBy == CurrentUserId);
            }
            if (IsConroleur)
            {
                query = query.Where(x => x.DemandeResultatEntete.Any(re => re.CreatedBy == CurrentUserId && re.ResultatExigence.Any()));
            }
            int pageSize = 10;

            int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);


            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<DemandeAccesEngin>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderByDescending(x => x.Id);

            model.resultList = query.ToPagedList(pageNumber, pageSize);

            ViewBag.Log = query.ToString();

            return View(model);

            // V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>demandeAccesEngin.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await demandeAccesEngin.ToListAsync());
        }
        public async Task<ActionResult> GetFileAzure(long? id)
        {
            var biz = new CommonBiz(context, log);

            var file = await context.AppFile.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            // var filePath = file.SystemFileName;
            //  var tt = biz.GetBlobBytes(file.SystemFileName,ContainerName);
            byte[] fileBytes = await biz.GetBlobBytes(file.SystemFileName, ContainerName);
            return File(fileBytes, MimeMapping.GetMimeMapping(file.OriginalFileName), Path.GetFileName(file.OriginalFileName));
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
