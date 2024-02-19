using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JamesThewDOTcom.Models;
using PagedList;

namespace JamesThewDOTcom.Controllers.Administration
{
    public class RecipesController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Recipes
        public ActionResult Index(string searchString, int? page)
        {
            var recipes = db.Recipes.Include(r => r.Employee);

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r =>
                    r.Title.Contains(searchString) ||
                    r.RecipeType.Contains(searchString));
            }

            // Phân trang
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.searchString = searchString;

            return View(recipes.OrderBy(r => r.Title).ToPagedList(pageNumber, pageSize));
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "UserName");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "RecipeID,Title,RecipeType,EmployeeID,Image,Ingredients,Step1,Step2,Step3,Step4,Step5,ImageOfStep1,ImageOfStep2,ImageOfStep3,ImageOfStep4,ImageOfStep5, Description")] Recipe recipe, 
            HttpPostedFileBase imageFile, 
            HttpPostedFileBase imageOfStep1File, 
            HttpPostedFileBase imageOfStep2File, 
            HttpPostedFileBase imageOfStep3File, 
            HttpPostedFileBase imageOfStep4File, 
            HttpPostedFileBase imageOfStep5File
            )
        {
            if (ModelState.IsValid)
            {
                // Xử lý ảnh chính (Image)
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageFile.SaveAs(filePath);
                    recipe.Image = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 1 (ImageOfStep1)
                if (imageOfStep1File != null && imageOfStep1File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep1File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep1File.SaveAs(filePath);
                    recipe.ImageOfStep1 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 2 (ImageOfStep2)
                if (imageOfStep2File != null && imageOfStep2File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep2File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep2File.SaveAs(filePath);
                    recipe.ImageOfStep2 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 3 (ImageOfStep3)
                if (imageOfStep3File != null && imageOfStep3File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep3File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep3File.SaveAs(filePath);
                    recipe.ImageOfStep3 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 4 (ImageOfStep4)
                if (imageOfStep4File != null && imageOfStep4File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep4File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep4File.SaveAs(filePath);
                    recipe.ImageOfStep4 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 5 (ImageOfStep5)
                if (imageOfStep5File != null && imageOfStep5File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep5File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep5File.SaveAs(filePath);
                    recipe.ImageOfStep5 = "~/Images/" + fileName;
                }

                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "UserName", recipe.EmployeeID);
            return View(recipe);
        }


        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "UserName", recipe.EmployeeID);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeID,Title,RecipeType,EmployeeID,Image,Ingredients,Step1,Step2,Step3,Step4,Step5,ImageOfStep1,ImageOfStep2,ImageOfStep3,ImageOfStep4,ImageOfStep5, Description")] Recipe recipe, HttpPostedFileBase imageFile, HttpPostedFileBase imageOfStep1File, HttpPostedFileBase imageOfStep2File, HttpPostedFileBase imageOfStep3File, HttpPostedFileBase imageOfStep4File, HttpPostedFileBase imageOfStep5File)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null || imageOfStep1File != null || imageOfStep2File != null || imageOfStep3File != null || imageOfStep4File != null || imageOfStep5File != null)
                {
                    List<string> imagePaths = new List<string>();

                    foreach (var file in new HttpPostedFileBase[] { imageFile, imageOfStep1File, imageOfStep2File, imageOfStep3File, imageOfStep4File, imageOfStep5File })
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                            file.SaveAs(filePath);
                            imagePaths.Add("~/Images/" + fileName);
                        }
                        else
                        {
                            if (file == imageFile)
                            {
                                imagePaths.Add(recipe.Image);
                            }
                            else if (file == imageOfStep1File)
                            {
                                imagePaths.Add(recipe.ImageOfStep1);
                            }
                            else if (file == imageOfStep2File)
                            {
                                imagePaths.Add(recipe.ImageOfStep2);
                            }
                            else if (file == imageOfStep3File)
                            {
                                imagePaths.Add(recipe.ImageOfStep3);
                            }
                            else if (file == imageOfStep4File)
                            {
                                imagePaths.Add(recipe.ImageOfStep4);
                            }
                            else if (file == imageOfStep5File)
                            {
                                imagePaths.Add(recipe.ImageOfStep5);
                            }
                        }
                    }

                    recipe.Image = imagePaths[0];
                    recipe.ImageOfStep1 = imagePaths[1];
                    recipe.ImageOfStep2 = imagePaths[2];
                    recipe.ImageOfStep3 = imagePaths[3];
                    recipe.ImageOfStep4 = imagePaths[4];
                    recipe.ImageOfStep5 = imagePaths[5];
                }

                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "UserName", recipe.EmployeeID);
            return View(recipe);
        }


        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
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
