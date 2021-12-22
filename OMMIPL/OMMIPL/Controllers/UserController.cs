using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMMIPL.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult UserDashboard()
        {
            return View();
        }
        
    }
}