using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers.Administration
{
    public class IngredientsController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Ingredients
        public ActionResult Index(int? recipeId)
        {
            if (recipeId == null)
            {
                var allIngredients = db.Ingredients.ToList();
                return View(allIngredients);
            }

            var ingredients = db.Ingredients.Where(i => i.RecipeID == recipeId).ToList();
            ViewBag.RecipeID = recipeId;
            ViewBag.RecipeTitle = db.Recipes.Where(r => r.RecipeID == recipeId).Select(r => r.Title).FirstOrDefault();
            return View(ingredients);
        }



        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: Ingredients/Create
        public ActionResult Create(int? recipeId)
        {
            if (recipeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipe = db.Recipes.Find(recipeId);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            ViewBag.RecipeID = new SelectList(db.Recipes, "RecipeID", "Title", recipeId);
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientID,RecipeID,IngredientName")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
                return RedirectToAction("Index", new { recipeId = ingredient.RecipeID });
            }

            ViewBag.RecipeID = ingredient.RecipeID;
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipeID = new SelectList(db.Recipes, "RecipeID", "Title", ingredient.RecipeID);
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredientID,RecipeID,IngredientName")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { recipeId = ingredient.RecipeID });
            }
            ViewBag.RecipeID = new SelectList(db.Recipes, "RecipeID", "Title", ingredient.RecipeID);
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            int? recipeId = ingredient.RecipeID;
            db.Ingredients.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Index", new { recipeId = recipeId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
