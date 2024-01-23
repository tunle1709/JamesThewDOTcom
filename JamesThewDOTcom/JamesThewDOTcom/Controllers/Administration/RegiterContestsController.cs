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
    public class RegiterContestsController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: RegiterContests
        public ActionResult Index()
        {
            var regiterContests = db.RegiterContests.Include(r => r.CookingContest).Include(r => r.CookingContest1).Include(r => r.Customer);
            return View(regiterContests.ToList());
        }

        // GET: RegiterContests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegiterContest regiterContest = db.RegiterContests.Find(id);
            if (regiterContest == null)
            {
                return HttpNotFound();
            }
            return View(regiterContest);
        }

        // GET: RegiterContests/Create
        public ActionResult Create()
        {
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title");
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName");
            return View();
        }

        // POST: RegiterContests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegisterContestID,CustomerID,CookingContestID")] RegiterContest regiterContest)
        {
            if (ModelState.IsValid)
            {
                db.RegiterContests.Add(regiterContest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title", regiterContest.CookingContestID);
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title", regiterContest.CookingContestID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName", regiterContest.CustomerID);
            return View(regiterContest);
        }

        // GET: RegiterContests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegiterContest regiterContest = db.RegiterContests.Find(id);
            if (regiterContest == null)
            {
                return HttpNotFound();
            }
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title", regiterContest.CookingContestID);
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title", regiterContest.CookingContestID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName", regiterContest.CustomerID);
            return View(regiterContest);
        }

        // POST: RegiterContests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegisterContestID,CustomerID,CookingContestID")] RegiterContest regiterContest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regiterContest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title", regiterContest.CookingContestID);
            ViewBag.CookingContestID = new SelectList(db.CookingContests, "CookingContestID", "Title", regiterContest.CookingContestID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName", regiterContest.CustomerID);
            return View(regiterContest);
        }

        // GET: RegiterContests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegiterContest regiterContest = db.RegiterContests.Find(id);
            if (regiterContest == null)
            {
                return HttpNotFound();
            }
            return View(regiterContest);
        }

        // POST: RegiterContests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegiterContest regiterContest = db.RegiterContests.Find(id);
            db.RegiterContests.Remove(regiterContest);
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
