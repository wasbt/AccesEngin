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
using Shared.API.IN;
using Shared.ENUMS;
using BLL.Common;
using System.IO;
using BLL.Biz;
using Shared.Models;

namespace Front.Controllers
{
    [Authorize(Roles = ConstsAccesEngin.ROLE_CHEFPROJET + "," + ConstsAccesEngin.ROLE_CONTROLEUR)]
    public class DemandeAccesEnginsController : BaseController
    {
        // GET: DemandeAccesEngins
        public async Task<ActionResult> Index(StandardModel<DemandeAccesEngin> model, SearchDemandeModel Filter)
        {

            var demandeAccesEngin = context.DemandeAccesEngin.Where(x => x.StatutDemandeId != 3).Include(d => d.AspNetUsers).Include(d => d.REF_TypeCheckList);
            var query = demandeAccesEngin.AsQueryable();

            if (IsChefProjet)
            {
                query = query.Where(x => x.CreatedBy == CurrentUserId);
            }
            if (IsConroleur)
            {
                query = query.Where(x => !x.ResultatControleEntete.Any());
            }

            if (Filter.StatutDemandeId.HasValue)
            {
                query = query.Where(x => x.StatutDemandeId == Filter.StatutDemandeId);
            }
            if (Filter.TypeCheckListId.HasValue)
            {
                query = query.Where(x => x.TypeCheckListId == Filter.TypeCheckListId);
            }
            if (Filter.Autorise.HasValue)
            {
                query = query.Where(x => x.IsAutorise == Filter.Autorise);
            }
            if (Filter.DatePlannification.HasValue)
            {
                query = query.Where(x => x.DatePlannification.Day == Filter.DatePlannification.Value.Day &&
                                         x.DatePlannification.Month == Filter.DatePlannification.Value.Month &&
                                         x.DatePlannification.Year == Filter.DatePlannification.Value.Year);
            }
            if (Filter.Controle.HasValue)
            {
                if (Filter.Controle.Value)
                {
                    query = query.Where(x => x.ResultatControleEntete.Any());
                }
                else
                {
                    query = query.Where(x => !x.ResultatControleEntete.Any());
                }
            }
            if (!string.IsNullOrWhiteSpace(Filter.Content))
            {
                query = query.Where(x => x.Id.ToString().Contains(Filter.Content) ||
                                         x.Observation.Contains(Filter.Content));
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

            var statutDemandes = context.REF_StatutDemandes.Where(x => x.Id != (int)DemandeStatus.Expirer);
            var typeCheckLists = context.REF_TypeCheckList;

            ViewBag.StatutDemandeId = new SelectList(statutDemandes, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(typeCheckLists, "Id", "Name");

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
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name");
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
                var listTypeCheckList = demandeAccesEnginPlannig.Select(x => x.REF_TypeCheckList).ToList();
                var getCountTypeEngin = listTypeCheckList.SelectMany(x => x.REF_TypeEngin);
                var selecDureeEstimativeToDay = getCountTypeEngin.Select(x => double.Parse(x.DureeEstimative));
                var sumDuree = selecDureeEstimativeToDay.Sum();


                if (sumDuree >= (long)DureeEstimativeEnums.MaxDuree)
                {
                    TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "s'il vous plaît choisir une autre date de planification!";
                    ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name");
                    ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
                    return View(demandeAccesEngin);
                }

                #endregion



                demandeAccesEngin.CreatedBy = CurrentUserId;
                demandeAccesEngin.CreatedOn = DateTime.Now;
                demandeAccesEngin.IsAutorise = false;
                context.DemandeAccesEngin.Add(demandeAccesEngin);
                await context.SaveChangesAsync();



                foreach (var item in ResultatInfoGeneral)
                {
                    var resultatInfoGeneral = new DemandeAccesEnginInfoGeneraleValue()
                    {
                        DemandeAccesEnginId = demandeAccesEngin.Id,
                        InfoGeneraleId = item.GeneralInfoId,
                        ValueInfo = item.ValueInfo,
                        CreatedOn = DateTime.Now,
                    };

                    var addeResultatIinfoGenerale = context.DemandeAccesEnginInfoGeneraleValue.Add(resultatInfoGeneral);
                }

                await context.SaveChangesAsync();

                #region case row has file
                if (file != null)
                {
                    //add file to database
                    fileId = await biz.SaveAppFile(file, ContainerName, demandeAccesEngin.Id.ToString(), SourceName);

                    //verify if file was added
                    if (fileId == 0)
                    {
                        return HttpNotFound();
                    }
                    demandeAccesEngin.AppFileId = fileId;
                }
                #endregion



                await context.SaveChangesAsync();

                #region Notif + push
                //var FullName = biz.GetFullNameByUserId(CurrentUserId);

                //string Content = string.Format("<strong>{0}</strong> vous a affecté l'action suivante: <strong>{1}</strong> prévue pour le <strong>{2}</strong>", FullName, demandeAccesEngin.REF_TypeEngin.Name, demandeAccesEngin.Observation, demandeAccesEngin.DatePlannification.ToString("yyyy-MM-dd"));
                //string DestUserId = demandeAccesEngin.ResponsableUserId;

                //biz.AddNotification(DestUserId, Content, CurrentUserId, NotifType: NotifTypes.ActionInfo, ObjectId: demandeAccesEngin.Id);

                //#region push
                //string PushTitle = "Nouvelle Action";
                //string PushContent = string.Format("{0} vous a affecté une nouvelle action prévue pour le {1}", FullName, actionInfo.DtPrevue.ToString("yyyy-MM-dd"));
                //string UserToken = context.AspNetUsers.Find(DestUserId).Profile.SingleOrDefault().MyOPSToken;
                //biz.SendGCMPushToUser(PushTitle, PushContent, UserToken);
                // #endregion

                #endregion
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
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
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name", demandeAccesEngin.Entite.SiteId);
            ViewBag.EntiteId = new SelectList(context.Entite.Where(e => e.SiteId == demandeAccesEngin.Entite.SiteId), "Id", "Name", demandeAccesEngin.EntiteId);

            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            ViewBag.TypeEnginId = new SelectList(context.REF_TypeEngin.Where(t => t.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.TypeEnginId);
            ViewBag.NatureMatiereId = new SelectList(context.REF_NatureMatiere.Where(n => n.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.NatureMatiereId);
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
                var demandeAcces = await context.DemandeAccesEngin.FindAsync(demandeAccesEngin.Id);
                demandeAcces.CreatedBy = CurrentUserId;
                demandeAcces.StatutDemandeId = null;
                demandeAcces.CreatedOn = DateTime.Now;
                var oldResultatInfoGenerale = context.DemandeAccesEnginInfoGeneraleValue.Where(x => x.DemandeAccesEnginId == demandeAccesEngin.Id);

                foreach (var olditem in oldResultatInfoGenerale)
                {
                    context.DemandeAccesEnginInfoGeneraleValue.Remove(olditem);
                }
                await context.SaveChangesAsync();


                foreach (var item in ResultatInfoGeneral)
                {
                    var resultatInfoGeneral = new DemandeAccesEnginInfoGeneraleValue()
                    {
                        DemandeAccesEnginId = demandeAccesEngin.Id,
                        InfoGeneraleId = item.GeneralInfoId,
                        ValueInfo = item.ValueInfo,
                        CreatedOn = DateTime.Now,
                    };

                    var addeResultatIinfoGenerale = context.DemandeAccesEnginInfoGeneraleValue.Add(resultatInfoGeneral);
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
                await context.SaveChangesAsync();

                #endregion
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.SiteId = new SelectList(context.Sites, "Id", "Name", demandeAccesEngin.Entite.SiteId);
            ViewBag.EntiteId = new SelectList(context.Entite.Where(e => e.SiteId == demandeAccesEngin.Entite.SiteId), "Id", "Name", demandeAccesEngin.EntiteId);

            ViewBag.TypeCheckListId = new SelectList(context.REF_TypeCheckList, "Id", "Name", demandeAccesEngin.TypeCheckListId);
            ViewBag.TypeEnginId = new SelectList(context.REF_TypeEngin.Where(t => t.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.TypeEnginId);
            ViewBag.NatureMatiereId = new SelectList(context.REF_NatureMatiere.Where(n => n.TypeCheckListId == demandeAccesEngin.TypeCheckListId), "Id", "Name", demandeAccesEngin.NatureMatiereId);
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

        [Authorize(Roles = ConstsAccesEngin.ROLE_CONTROLEUR)]
        public async Task<ActionResult> MyControl(StandardModel<DemandeAccesEngin> model, SearchDemandeModel Filter)
        {
            ViewBag.ShowButton = false;

            var query = context.DemandeAccesEngin.AsQueryable();


            if (IsConroleur)
            {
                query = query.Where(x => x.ResultatControleEntete.Any(re => re.CreatedBy == CurrentUserId && re.ResultatControleDetail.Any()));
            }
            if (Filter.StatutDemandeId.HasValue)
            {
                query = query.Where(x => x.StatutDemandeId == Filter.StatutDemandeId);
            }
            if (Filter.TypeCheckListId.HasValue)
            {
                query = query.Where(x => x.TypeCheckListId == Filter.TypeCheckListId);
            }
            if (Filter.Autorise.HasValue)
            {
                query = query.Where(x => x.IsAutorise == Filter.Autorise);
            }
            if (Filter.Controle.HasValue)
            {
                if (Filter.Controle.Value)
                {
                    query = query.Where(x => x.ResultatControleEntete.Any());
                }
                else
                {
                    query = query.Where(x => !x.ResultatControleEntete.Any());
                }
            }
            if (Filter.DatePlannification.HasValue)
            {
                query = query.Where(x => x.DatePlannification.Day == Filter.DatePlannification.Value.Day &&
                                         x.DatePlannification.Month == Filter.DatePlannification.Value.Month &&
                                         x.DatePlannification.Year == Filter.DatePlannification.Value.Year);
            }
            if (!string.IsNullOrWhiteSpace(Filter.Content))
            {
                query = query.Where(x => x.Id.ToString().Contains(Filter.Content) ||
                                         x.Observation.Contains(Filter.Content));
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
            var statutDemandes = context.REF_StatutDemandes.Where(x => x.Id != (int)DemandeStatus.Expirer);
            var typeCheckLists = context.REF_TypeCheckList;

            ViewBag.StatutDemandeId = new SelectList(statutDemandes, "Id", "Name");
            ViewBag.TypeCheckListId = new SelectList(typeCheckLists, "Id", "Name");

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
