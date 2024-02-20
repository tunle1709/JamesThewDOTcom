using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using JamesThewDOTcom.Models;
using PagedList;

namespace JamesThewDOTcom.Controllers
{
    public class RecipesPublicController : Controller
    {

        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Receipe
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var recipes = db.Recipes.ToList().ToPagedList(pageNumber, pageSize);

            foreach (var recipe in recipes)
            {
                int employeeID = (int)recipe.EmployeeID;
                Employee employee = db.Employees.Find(employeeID);

                if (employee != null && !string.IsNullOrEmpty(employee.UserName))
                {
                    recipe.UserName = employee.UserName;
                }
                else
                {
                    recipe.UserName = "Unknown";
                }
            }

            return View("~/Views/User/RecipesPublic/Index.cshtml", recipes);
        }

        public ActionResult Detail(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            
            var ingredients = db.Ingredients.Where(i => i.RecipeID == id).ToList();

            recipe.Ingredients1 = ingredients;

            return View("~/Views/User/RecipesPublic/Detail.cshtml", recipe);
        }


    }
}
