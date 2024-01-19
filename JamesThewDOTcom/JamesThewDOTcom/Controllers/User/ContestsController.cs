using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace JamesThewDOTcom.Controllers
{
    public class ContestsController : Controller
    {

        string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

        // GET: Contests
        public ActionResult Index()
        {
            return View("~/Views/User/Contests/Index.cshtml");
        }
    }
}