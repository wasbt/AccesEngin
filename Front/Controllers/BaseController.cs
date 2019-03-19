using DAL;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public EnginDbContext context = new EnginDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ContainerName - SourceName

        public const string ContainerName = "demandeaccesengincontainer";
        public const string SourceName = "DemandeAccesEngin";

        #endregion
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public string CurrentUserId
        {
            get { return User.Identity.GetUserId(); }
            set { CurrentUserId = value; }
        }

        public string CurrentFullName
        {
            get { return Session[ConstsAccesEngin.SESSION_FullName] == null ? string.Empty : (string)Session[ConstsAccesEngin.SESSION_FullName]; }
            set { CurrentFullName = value; }
        }

        public Profile CurrentUserProfile
        {
            get { return context.Profile.Where(u => u.Id == CurrentUserId).SingleOrDefault(); }

        }



        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (filterContext.ExceptionHandled)
            {
                return;
            }

            // LOG4NET
            MvcApplication.log.Error(filterContext.Exception.Message + "User: " + User?.Identity?.Name, filterContext.Exception);
            TempData[ConstsAccesEngin.MESSAGE_ERROR] = string.Format("Une erreur s'est produite, veuillez réessayer.");

            filterContext.Result = new RedirectResult("~/Home/Index/?error=true&msg=" + filterContext.Exception.Message);

            filterContext.ExceptionHandled = true;
        }

        protected string GetModelStateErrors(ModelStateDictionary modelState)
        {
            return string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        }


        public bool IsAdmin
        {
            get
            {
                return User.IsInRole(ConstsAccesEngin.ROLE_BACKOFFICE);
            }
        }

        public bool IsChefProjet
        {
            get
            {
                return User.IsInRole(ConstsAccesEngin.ROLE_CHEFPROJET);
            }
        }

        public bool IsConroleur
        {
            get
            {
                return User.IsInRole(ConstsAccesEngin.ROLE_CONTROLEUR);
            }
        }



    }
}