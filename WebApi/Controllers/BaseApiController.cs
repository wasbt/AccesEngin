using DAL;
using Microsoft.AspNet.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public OcpPerformanceDataContext context = new OcpPerformanceDataContext();
        private ApplicationUserManager _userManager;

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

    }
}