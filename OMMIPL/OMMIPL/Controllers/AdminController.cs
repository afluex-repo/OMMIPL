using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMMIPL.Controllers
{
    public class AdminController : BaseController
    {
        // GET: User
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}