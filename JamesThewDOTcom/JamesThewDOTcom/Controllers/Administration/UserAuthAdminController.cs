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
    public class UserAuthAdminController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();

        [HttpGet]
        [Route("Login")]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var authenticatedEmployee = AuthenticateEmployee(employee.UserName, employee.Password);

                if (authenticatedEmployee != null)
                {
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }

            return View(employee);
        }

        private Employee AuthenticateEmployee(string userName, string password)
        {
            var authenticatedEmployee = db.Employees
                .Where(e => e.UserName == userName && e.Password == password)
                .FirstOrDefault();

            return authenticatedEmployee;
        }
    }
}
