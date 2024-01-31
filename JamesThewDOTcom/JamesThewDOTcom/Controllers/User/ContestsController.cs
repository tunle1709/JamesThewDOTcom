using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers
{
    public class ContestsController : Controller
    {

        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Contests
        public ActionResult Index()
        {
            return View("~/Views/User/Contests/Index.cshtml");
        }

        public ActionResult DetailContest()
        {
            return View("~/Views/User/Contests/DetailContest.cshtml");
        }
    }
}