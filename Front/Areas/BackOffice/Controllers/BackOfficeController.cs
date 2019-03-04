using Front.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared;
namespace Front.Areas.BackOffice.Controllers
{
    [Authorize(Roles = ConstsAccesEngin.ROLE_BACKOFFICE)]
    public class BackOfficeController : BaseController
    {
      
    }
}