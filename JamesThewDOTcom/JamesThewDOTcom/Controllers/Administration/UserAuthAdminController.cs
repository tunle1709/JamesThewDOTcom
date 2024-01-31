using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers.Administration
{
    public class UserAuthAdminController : Controller
    {
        private JamesThewDBEntities db = new JamesThewDBEntities();
        private Employee AuthenticateEmployee(string userName, string password)
        {
            var authenticatedEmployee = db.Employees
                .Where(e => e.UserName == userName && e.Password == password)
                .FirstOrDefault();

            return authenticatedEmployee;
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(Employee employee, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var authenticatedEmployee = AuthenticateEmployee(employee.UserName, employee.Password);

                if (authenticatedEmployee != null)
                {
                    SetAuthenticationSession(authenticatedEmployee);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Employees");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }

            return View(employee);
        }

        private void SetAuthenticationSession(Employee authenticatedEmployee)
        {
            Session["UserName"] = authenticatedEmployee.UserName;
            Session["Password"] = authenticatedEmployee.Password;
        }
    }
}
