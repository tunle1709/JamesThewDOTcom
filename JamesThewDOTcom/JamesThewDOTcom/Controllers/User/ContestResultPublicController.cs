using JamesThewDOTcom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamesThewDOTcom.Controllers.User
{
    public class ContestResultPublicController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: ContestResults
        public ActionResult Index()
        {
            return View("~/Views/User/ContestResultPublic/Index.cshtml");
        }       
    }
}
