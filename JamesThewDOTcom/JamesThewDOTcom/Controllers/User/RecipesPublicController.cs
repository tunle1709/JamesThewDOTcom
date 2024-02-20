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
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var recipes = db.Recipes.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Title.Contains(searchString)).ToList();
            }

            var pagedRecipes = recipes.ToPagedList(pageNumber, pageSize);

            foreach (var recipe in pagedRecipes)
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

            ViewBag.SearchString = searchString;

            return View("~/Views/User/RecipesPublic/Index.cshtml", pagedRecipes);
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


        [HttpPost]
        public ActionResult AddFeedback(int recipeId, string feedbackComment)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "UserAuth");
            }

            string loggedInUserName = Session["UserName"].ToString();

            var loggedInCustomer = db.Customers.FirstOrDefault(c => c.UserName == loggedInUserName);

            if (loggedInCustomer == null)
            {
                return RedirectToAction("Error", "Home");
            }

            FeedBack feedback = new FeedBack
            {
                RecipeID = recipeId,
                Comment = feedbackComment,
                CustomerID = loggedInCustomer.CustomerID
            };

            db.FeedBacks.Add(feedback);
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = recipeId });
        }

    }
}
