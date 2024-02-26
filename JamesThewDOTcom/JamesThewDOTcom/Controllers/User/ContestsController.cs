using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using JamesThewDOTcom.Models;
using System.IO;

namespace JamesThewDOTcom.Controllers
{
    public class ContestsController : Controller
    {

        private JamesThewDBEntities db = new JamesThewDBEntities();

        // GET: Contests
        public ActionResult Index()
        {
            List<CookingContest> cookingContests = db.CookingContests.ToList();

            return View("~/Views/User/Contests/Index.cshtml", cookingContests);
        }

        public ActionResult DetailContest(int id)
        {
            var contestDetails = db.CookingContests.Find(id);

            if (contestDetails == null)
            {
                return HttpNotFound();
            }

            return View("~/Views/User/Contests/DetailContest.cshtml", contestDetails);
        }

        [HttpPost]
        public ActionResult RegisterContest(int contestId)
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

            var registerContest = new RegiterContest
            {
                CustomerID = loggedInCustomer.CustomerID,
                CookingContestID = contestId
            };

            db.RegiterContests.Add(registerContest);
            db.SaveChanges();

            return RedirectToAction("UploadRecipe", "Contests", new { customerId = loggedInCustomer.CustomerID, registerContestId = contestId });
        }

        [HttpGet]
        public ActionResult UploadRecipe(int customerId, int registerContestId)
        {
            ViewBag.CustomerID = customerId;
            ViewBag.RegisterContestID = registerContestId;

            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

            return View("~/Views/User/Contests/UploadRecipe.cshtml");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadRecipe(
            [Bind(Include = "RecipeID,Title,RecipeType,CustomerID,RegisterContestID,Image,Ingredients,Step1,Step2,Step3,Step4,Step5,ImageOfStep1,ImageOfStep2,ImageOfStep3,ImageOfStep4,ImageOfStep5, Description")] CustomerRecipe customerRecipe,
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
                    customerRecipe.Image = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 1 (ImageOfStep1)
                if (imageOfStep1File != null && imageOfStep1File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep1File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep1File.SaveAs(filePath);
                    customerRecipe.ImageOfStep1 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 2 (ImageOfStep2)
                if (imageOfStep2File != null && imageOfStep2File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep2File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep2File.SaveAs(filePath);
                    customerRecipe.ImageOfStep2 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 3 (ImageOfStep3)
                if (imageOfStep3File != null && imageOfStep3File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep3File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep3File.SaveAs(filePath);
                    customerRecipe.ImageOfStep3 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 4 (ImageOfStep4)
                if (imageOfStep4File != null && imageOfStep4File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep4File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep4File.SaveAs(filePath);
                    customerRecipe.ImageOfStep4 = "~/Images/" + fileName;
                }

                // Xử lý ảnh cho bước 5 (ImageOfStep5)
                if (imageOfStep5File != null && imageOfStep5File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageOfStep5File.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageOfStep5File.SaveAs(filePath);
                    customerRecipe.ImageOfStep5 = "~/Images/" + fileName;
                }

                db.CustomerRecipes.Add(customerRecipe);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Post the recipe for success!";
                return RedirectToAction("UploadRecipe", new { customerId = customerRecipe.CustomerID, registerContestId = customerRecipe.RegisterContestID });
            }

            return View("~/Views/User/Contests/UploadRecipe.cshtml", customerRecipe);
        }
    }
}