using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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

            bool registrationSuccess = RegisterNewCustomer(customer);

            if (registrationSuccess)
            {
                return RedirectToAction("Login", "UserAuth");
            }
            else
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");
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
}
