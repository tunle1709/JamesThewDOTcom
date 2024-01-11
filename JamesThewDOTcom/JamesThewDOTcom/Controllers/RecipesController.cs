using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamesThewDOTcom.Controllers
{
    public class RecipesController : Controller
    {
        // GET: Receipe
        public ActionResult Index()
        {
            return View();
        }
    }
}
