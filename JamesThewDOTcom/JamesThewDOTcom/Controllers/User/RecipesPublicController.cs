using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers
{
    public class RecipesPublicController : Controller
    {

        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Receipe
        public ActionResult Index()
        {
            List<Recipe> recipes = db.Recipes.ToList();
            
            return View("~/Views/User/RecipesPublic/Index.cshtml", recipes);
        }

        public ActionResult Detail(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/User/RecipesPublic/Detail.cshtml", recipe);
        }

    }
}
