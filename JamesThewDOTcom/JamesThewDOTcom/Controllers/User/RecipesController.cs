using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace JamesThewDOTcom.Controllers
{
    public class RecipesController : Controller
    {

        string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

        // GET: Receipe
        public ActionResult Index()
        {
            return View("~/Views/User/Recipes/Index.cshtml");
        }
    }
}
