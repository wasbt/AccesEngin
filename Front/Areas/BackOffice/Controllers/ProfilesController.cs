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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Front.Areas.BackOffice.Controllers
{
    public class ProfilesController : BackOfficeController
    {
        // GET: BackOffice/Profiles
        public async Task<ActionResult> Index(StandardModel<Profile> model)
        {
            var profile = context.Profile.Include(p => p.AspNetUsers);
            int pageSize = 10;

            int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);

            var query = context.Profile.AsQueryable();

            if (!String.IsNullOrEmpty(model.content))
            {
                query = (IQueryable<Profile>)query.ProcessWhere(model.columnName, model.content);
            }

            query = query.OrderBy(x => x.Id);

            model.resultList = query.ToPagedList(pageNumber, pageSize);

            ViewBag.Log = query.ToString();

            return View(model);

            // V2: return View(await Task.Run(() => query.ToPagedList(pageNumber, pageSize)));
            // V1: return View(await Task.Run(()=>profile.OrderBy(x=>x.Id).ToPagedList(pageNumber,pageSize)));
            // V0: return View(await profile.ToListAsync());
        }

        // GET: BackOffice/Profiles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profiles = await context.Profile.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // GET: BackOffice/Profiles/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(context.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: BackOffice/Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Profile profile, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
                var user = new ApplicationUser { UserName = profile.Email, Email = profile.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                var ProfileUser = new Profile
                {
                    Id = user.Id,
                    FullName = profile.FullName,
                    Email = profile.Email,
                    Phone = profile.Phone,
                    Details = profile.Details,
                };
                context.Profile.Add(ProfileUser);
                await context.SaveChangesAsync();
                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Élément ajouté avec succès!";
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(context.AspNetUsers, "Id", "Email", profile.Id);
            return View(profile);
        }

        // GET: BackOffice/Profiles/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profiles = await context.Profile.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(context.AspNetUsers, "Id", "Email", profiles.Id);
            return View(profiles);
        }

        // POST: BackOffice/Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Profile profile, RegisterViewModel model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("Confirm password");
            if (ModelState.IsValid)
            {
                //Edite Profile
                context.Entry(profile).State = EntityState.Modified;
                await context.SaveChangesAsync();

                //Edite AspNetUsers
                if (!string.IsNullOrWhiteSpace(profile.Email))
                {
                    var user = UserManager.FindById(profile.Id);
                    if (!string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.ConfirmPassword))
                    {
                        var newPasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                        user.PasswordHash = newPasswordHash;
                    }
                    user.Email = profile.Email;
                    user.UserName = profile.Email;
                    Task.WaitAny(UserManager.UpdateAsync(user));
                }

                TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = "Mise à jour efféctuée avec succès!";
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(context.AspNetUsers, "Id", "Email", profile.Id);
            return View(profile);
        }

        // GET: BackOffice/Profiles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profiles = await context.Profile.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // POST: BackOffice/Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Profile profiles = await context.Profile.FindAsync(id);
            context.Profile.Remove(profiles);
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
