using DATAAL;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Front.Controllers
{
    [System.Web.Http.Authorize]
    public class BaseApiController : ApiController
    {
        public TestEnginEntities context = new TestEnginEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ContainerName - SourceName

        public const string ContainerName = "";
        public const string SourceName = "";

        #endregion
        public string CurrentUserId
        {
            get { return User.Identity.GetUserId(); }
            set { CurrentUserId = value; }
        }

      
        public Profile CurrentUserProfile
        {
            get { return context.Profile.Where(u => u.Id == CurrentUserId).SingleOrDefault(); }

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
        public bool IsControleur
        {
            get
            {
                return User.IsInRole(ConstsAccesEngin.ROLE_CONTROLEUR);
            }
        }
        public bool IsChefProjet
        {
            get
            {
                return User.IsInRole(ConstsAccesEngin.ROLE_CHEFPROJET);
            }
        }

    }
}