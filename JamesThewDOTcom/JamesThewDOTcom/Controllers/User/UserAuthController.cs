using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JamesThewDOTcom.Models;

public class UserAuthController : Controller
{
    private JamesThewDBEntities db = new JamesThewDBEntities();

    [HttpGet]
    public ActionResult Register()
    {
        return View("~/Views/User/UserAuth/Register.cshtml");
    }

    [HttpPost]
    public ActionResult Register(Customer customer)
    {
        if (ModelState.IsValid)
        {
            if (customer.Password != customer.RePassword)
            {
                ModelState.AddModelError("RePassword", "The password and confirmation password do not match.");
                return View("~/Views/User/UserAuth/Register.cshtml", customer);
            }

            if (IsUsernameUnique(customer.UserName))
            {
                if (customer.Password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password must be at least 6 characters long.");
                    return View("~/Views/User/UserAuth/Register.cshtml", customer);
                }

                bool registrationSuccess = RegisterNewCustomer(customer);

                if (registrationSuccess)
                {
                    TempData["PaymentType"] = customer.PaymentTypeID;
                    return RedirectToAction("PaymentConfirmation", "UserAuth");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View("~/Views/User/UserAuth/Register.cshtml", customer);
            }
        }

        return View("~/Views/User/UserAuth/Register.cshtml", customer);
    }



    private bool RegisterNewCustomer(Customer customer)
    {
        try
        {
            db.Customers.Add(customer);
            db.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool IsUsernameUnique(string username)
    {
        return !db.Customers.Any(c => c.UserName == username);
    }

    public ActionResult PaymentConfirmation()
    {
        int paymentType = (int)TempData["PaymentType"];
        decimal registrationFee = paymentType == 1 ? 10 : 100;

        DateTime endDate = paymentType == 1 ? DateTime.Now.AddDays(31) : DateTime.Now.AddDays(365);

        ViewBag.RegistrationFee = registrationFee;
        ViewBag.EndDate = endDate;

        return View("~/Views/User/UserAuth/PaymentConfirmation.cshtml");
    }



    [HttpGet]
    public ActionResult Login()
    {
        return View("~/Views/User/UserAuth/Login.cshtml");
    }

    [HttpPost]
    public ActionResult Login(Customer customer)
    {

        if (ModelState.IsValid)
        {
            var authenticatedCustomer = AuthenticateCustomer(customer.UserName, customer.Password);

            if (authenticatedCustomer != null)
            {
                FormsAuthentication.SetAuthCookie(authenticatedCustomer.UserName, false);

                Session["UserName"] = authenticatedCustomer.UserName;
                Session["Password"] = authenticatedCustomer.Password;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
            }
        }

        return View("~/Views/User/UserAuth/Login.cshtml", customer);
    }

    private Customer AuthenticateCustomer(string userName, string password)
    {
        return db.Customers.FirstOrDefault(c => c.UserName == userName && c.Password == password);
    }


    [HttpGet]
    public ActionResult ResetPassword()
    {
        return View("~/Views/User/UserAuth/ResetPassword.cshtml");
    }

    [HttpPost]
    public ActionResult ResetPassword(string currentPassword, string newPassword, string confirmNewPassword)
    {
        if (ModelState.IsValid)
        {
            string userName = (string)Session["UserName"];

            var authenticatedCustomer = AuthenticateCustomer(userName, currentPassword);
            if (authenticatedCustomer != null)
            {
                if (newPassword != confirmNewPassword)
                {
                    ModelState.AddModelError("", "The new password and confirmation password do not match.");
                    return View("~/Views/User/UserAuth/ResetPassword.cshtml");
                }

                if (newPassword.Length < 6)
                {
                    ModelState.AddModelError("", "The new password must be at least 6 characters long.");
                    return View("~/Views/User/UserAuth/ResetPassword.cshtml");
                }

                if (currentPassword == newPassword)
                {
                    ModelState.AddModelError("", "The new password must be different from the old password.");
                    return View("~/Views/User/UserAuth/ResetPassword.cshtml");
                }


                var customer = db.Customers.FirstOrDefault(c => c.UserName == userName);
                if (customer != null)
                {
                    customer.Password = newPassword;
                    db.SaveChanges();


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid current password");
            }
        }

        return View("~/Views/User/UserAuth/ResetPassword.cshtml");
    }



    [HttpGet]
    public ActionResult Logout()
    {
        FormsAuthentication.SignOut();

        Session.Clear();

        return RedirectToAction("Index", "Home");
    }



    [HttpGet]
    public ActionResult EditProfile()
    {
        string userName = (string)Session["UserName"];

        Customer customer = db.Customers.FirstOrDefault(c => c.UserName == userName);

        if (customer != null)
        {
            return View("~/Views/User/UserAuth/EditProfile.cshtml", customer);
        }
        else
        {
            return HttpNotFound();
        }
    }

    [HttpPost]
    public ActionResult EditProfile(Customer updatedCustomer)
    {
        if (ModelState.IsValid)
        {
            string userName = (string)Session["UserName"];

            Customer existingCustomer = db.Customers.FirstOrDefault(c => c.UserName == userName);

            if (existingCustomer != null)
            {
                existingCustomer.First_Name = updatedCustomer.First_Name;
                existingCustomer.Last_Name = updatedCustomer.Last_Name;
                existingCustomer.Address = updatedCustomer.Address;
                existingCustomer.City = updatedCustomer.City;
                existingCustomer.Phone = updatedCustomer.Phone;
                existingCustomer.BirthDate = updatedCustomer.BirthDate;

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return HttpNotFound(); 
            }
        }

        return View("~/Views/User/UserAuth/EditProfile.cshtml", updatedCustomer);
    }
}