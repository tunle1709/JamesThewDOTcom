using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace JamesThewDOTcom.Controllers
{
    public class FAQController : Controller
    {

        string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

        // GET: FAQ
        public ActionResult Index()
        {
            return View();
        }
    }
}