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
    public class CookingContestsController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: CookingContests
        public ActionResult Index()
        {
            return View(db.CookingContests.ToList());
        }

        // GET: CookingContests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingContest cookingContest = db.CookingContests.Find(id);
            if (cookingContest == null)
            {
                return HttpNotFound();
            }
            return View(cookingContest);
        }

        // GET: CookingContests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CookingContests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CookingContestID,Title,Description,StartDate,EndDate")] CookingContest cookingContest)
        {
            if (ModelState.IsValid)
            {
                db.CookingContests.Add(cookingContest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cookingContest);
        }

        // GET: CookingContests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingContest cookingContest = db.CookingContests.Find(id);
            if (cookingContest == null)
            {
                return HttpNotFound();
            }
            return View(cookingContest);
        }

        // POST: CookingContests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CookingContestID,Title,Description,StartDate,EndDate")] CookingContest cookingContest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cookingContest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cookingContest);
        }

        // GET: CookingContests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingContest cookingContest = db.CookingContests.Find(id);
            if (cookingContest == null)
            {
                return HttpNotFound();
            }
            return View(cookingContest);
        }

        // POST: CookingContests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CookingContest cookingContest = db.CookingContests.Find(id);
            db.CookingContests.Remove(cookingContest);
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
