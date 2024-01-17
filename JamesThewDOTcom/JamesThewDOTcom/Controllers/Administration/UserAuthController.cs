using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace JamesThewDOTcom.Controllers
{
    public class UserAuthController : Controller
    {

        string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
