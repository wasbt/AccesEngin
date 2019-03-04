using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front.Areas.BackOffice.Controllers
{
    public class SettingsController : BackOfficeController
    {
        // GET: BackOffice/Settings
        public ActionResult Index()
        {
            return View();
        }
    }
}