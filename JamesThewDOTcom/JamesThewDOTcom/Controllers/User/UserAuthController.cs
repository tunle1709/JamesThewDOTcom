using JamesThewDOTcom.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System;

public class UserAuthController : Controller
{
    private string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

    [HttpGet]
    public ActionResult Register()
    {
        return View("~/Views/User/UserAuth/Register.cshtml");
    }


    [HttpPost]
    public ActionResult Register(Customers customer)
    {
        if (ModelState.IsValid)
        {
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

    private bool RegisterNewCustomer(Customers customer)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();

                string sql = @"INSERT INTO Customers (UserName, Password, Last_Name, First_Name, BirthDate, Address, City, Phone, CustomersTypeID, PaymentTypeID)
                           VALUES (@UserName, @Password, @Last_Name, @First_Name, @BirthDate, @Address, @City, @Phone, @CustomersTypeID, @PaymentTypeID)";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", customer.UserName);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);
                    cmd.Parameters.AddWithValue("@Last_Name", customer.Last_Name);
                    cmd.Parameters.AddWithValue("@First_Name", customer.First_Name);
                    cmd.Parameters.AddWithValue("@BirthDate", customer.BirthDate);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@CustomersTypeID", customer.CustomersTypeID);
                    cmd.Parameters.AddWithValue("@PaymentTypeID", customer.PaymentTypeID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
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
    public ActionResult Login(Customers customer)
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

    private Customers AuthenticateCustomer(string userName, string password)
    {
        using (SqlConnection connection = new SqlConnection(strconn))
        {
            connection.Open();

            string sql = "SELECT * FROM Customers WHERE UserName = @UserName AND Password = @Password";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Customers customer = new Customers
                        {
                            CustomerID = (int)reader["CustomerID"],
                            UserName = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Last_Name = reader["Last_Name"].ToString(),
                            First_Name = reader["First_Name"].ToString(),
                            BirthDate = (DateTime)reader["BirthDate"],
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Phone = reader["Phone"].ToString()
                        };

                        return customer;
                    }
                }
            }
        }

        return null;
    }
}
