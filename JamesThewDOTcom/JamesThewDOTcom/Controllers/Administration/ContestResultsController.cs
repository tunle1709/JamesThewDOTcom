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
    public class ContestResultsController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: ContestResults
        public ActionResult Index()
        {
            var contestResults = db.ContestResults.Include(c => c.Customer).Include(c => c.RegiterContest);
            return View(contestResults.ToList());
        }

        // GET: ContestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestResult contestResult = db.ContestResults.Find(id);
            if (contestResult == null)
            {
                return HttpNotFound();
            }
            return View(contestResult);
        }

        // GET: ContestResults/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName");
            ViewBag.RegisterContestID = new SelectList(db.RegiterContests, "RegisterContestID", "RegisterContestID");
            return View();
        }

        // POST: ContestResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Score,RegisterContestID")] ContestResult contestResult)
        {
            if (ModelState.IsValid)
            {
                db.ContestResults.Add(contestResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName", contestResult.CustomerID);
            ViewBag.RegisterContestID = new SelectList(db.RegiterContests, "RegisterContestID", "RegisterContestID", contestResult.RegisterContestID);
            return View(contestResult);
        }

        // GET: ContestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestResult contestResult = db.ContestResults.Find(id);
            if (contestResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName", contestResult.CustomerID);
            ViewBag.RegisterContestID = new SelectList(db.RegiterContests, "RegisterContestID", "RegisterContestID", contestResult.RegisterContestID);
            return View(contestResult);
        }

        // POST: ContestResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Score,RegisterContestID")] ContestResult contestResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contestResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserName", contestResult.CustomerID);
            ViewBag.RegisterContestID = new SelectList(db.RegiterContests, "RegisterContestID", "RegisterContestID", contestResult.RegisterContestID);
            return View(contestResult);
        }

        // GET: ContestResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestResult contestResult = db.ContestResults.Find(id);
            if (contestResult == null)
            {
                return HttpNotFound();
            }
            return View(contestResult);
        }

        // POST: ContestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContestResult contestResult = db.ContestResults.Find(id);
            db.ContestResults.Remove(contestResult);
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
