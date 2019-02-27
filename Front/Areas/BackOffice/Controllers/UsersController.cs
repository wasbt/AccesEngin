using BLL.Biz;
using Front.Controllers;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front.Areas.BackOffice.Controllers
{
    public class UsersController : BaseController
    {
        // GET: BackOffice/Users
        // GET: Admin/AuthManager
        [HttpGet]
        public ActionResult UsersAndRolesList()
        {
            var biz = new ProfileBiz(context, MvcApplication.log);

            List<UsersAndRolesElement> model = new List<UsersAndRolesElement>();

            model = biz.GetUsersAndRolesViewModel(0, 0);

            return View(model);
        }


        [HttpGet]
        public ActionResult AffectationRoles()
        {
            var biz = new ProfileBiz(context, MvcApplication.log);
           
            ViewData["UsersList"] = new SelectList(context.Profile, "Id", "FullName");
            
            ViewData["AllRoles"] = biz.GetAllRoles();

            return View();
        }

        public ActionResult Desactiver(string id)
        {
            var user = context.AspNetUsers.Find(id);

            if (user != null)
            {
                var query = string.Format("delete from ASPNETUSERSROLES where UserId = '{0}'", user.Id);
                var result = context.Database.ExecuteSqlCommand(query);

                if (result > 0)
                    TempData[ConstsAccesEngin.MESSAGE_SUCCESS] = string.Format("L'utiilisateur <strong>{0}</strong> a été désactivé avec succès", user.Profile.FullName);
            }

            return RedirectToAction("Index");
        }
    }
}