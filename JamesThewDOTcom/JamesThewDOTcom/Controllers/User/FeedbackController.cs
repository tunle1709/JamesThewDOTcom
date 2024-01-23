using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers
{
    public class FeedbackController : Controller
    {

        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Feedback
        public ActionResult Index()
        {
            return View("~/Views/User/Feedback/Index.cshtml");
        }
    }
}