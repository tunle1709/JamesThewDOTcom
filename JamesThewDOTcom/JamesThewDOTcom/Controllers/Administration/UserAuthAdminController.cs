using JamesThewDOTcom.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamesThewDOTcom.Controllers.Administration
{
    //[RouteArea("Admin")]
    //[RoutePrefix("Auth")]
    public class UserAuthAdminController : Controller
    {

        private string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

        [HttpGet]
        [Route("Login")]
        public ActionResult LoginAdmin()
        {
            return View("~/Views/Administration/UserAuthAdmin/LoginAdmin.cshtml");
        }


        [HttpPost]
        public ActionResult LoginAdmin(Employees employee)
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

            return View("~/Views/Administration/UserAuthAdmin/LoginAdmin.cshtml", employee);
        }

        private Employees AuthenticateEmployee(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();

                string sql = "SELECT * FROM Employees WHERE UserName = @UserName AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Employees employee = new Employees
                            {
                                EmployeeID = (int)reader["EmployeeID"],
                                UserName = reader["UserName"].ToString(),
                                Password = reader["Password"].ToString(),
                                Last_Name = reader["Last_Name"].ToString(),
                                First_Name = reader["First_Name"].ToString(),
                                BirthDate = (DateTime)reader["BirthDate"],
                                Address = reader["Address"].ToString(),
                                City = reader["City"].ToString(),
                                Region = reader["Region"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            };

                            return employee;
                        }
                    }
                }
            }

            return null;
        }

    }
}
