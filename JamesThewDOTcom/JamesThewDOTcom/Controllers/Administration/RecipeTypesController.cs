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
    public class RecipeTypesController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: RecipeTypes
        public ActionResult Index()
        {
            return View(db.RecipeTypes.ToList());
        }

        // GET: RecipeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeType recipeType = db.RecipeTypes.Find(id);
            if (recipeType == null)
            {
                return HttpNotFound();
            }
            return View(recipeType);
        }

        // GET: RecipeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeTypeID,RecipeTyName")] RecipeType recipeType)
        {
            if (ModelState.IsValid)
            {
                db.RecipeTypes.Add(recipeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipeType);
        }

        // GET: RecipeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeType recipeType = db.RecipeTypes.Find(id);
            if (recipeType == null)
            {
                return HttpNotFound();
            }
            return View(recipeType);
        }

        // POST: RecipeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeTypeID,RecipeTyName")] RecipeType recipeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipeType);
        }

        // GET: RecipeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeType recipeType = db.RecipeTypes.Find(id);
            if (recipeType == null)
            {
                return HttpNotFound();
            }
            return View(recipeType);
        }

        // POST: RecipeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecipeType recipeType = db.RecipeTypes.Find(id);
            db.RecipeTypes.Remove(recipeType);
            db.SaveChanges();
            return RedirectToAction("Index");
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
