using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers.Administration
{
    public class CustomersController : Controller
    {
        private string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;
        private string tableName = "Customers";

        private Customers GetCustomerById(int id)
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName} WHERE CustomerID = {id}";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        Customers customer = new Customers
                        {
                            CustomerID = (int)dataTable.Rows[0]["CustomerID"],
                            UserName = dataTable.Rows[0]["UserName"].ToString(),
                            Password = dataTable.Rows[0]["Password"].ToString(),
                            Last_Name = dataTable.Rows[0]["Last_Name"].ToString(),
                            First_Name = dataTable.Rows[0]["First_Name"].ToString(),
                            BirthDate = (DateTime)dataTable.Rows[0]["BirthDate"],
                            Address = dataTable.Rows[0]["Address"].ToString(),
                            City = dataTable.Rows[0]["City"].ToString(),
                            Phone = dataTable.Rows[0]["Phone"].ToString()
                        };
                        return customer;
                    }
                }
            }
            return null;
        }

        // GET: Customers
        public ActionResult Index()
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName}";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, tableName);

                    List<Customers> customerList = dataSet.Tables[tableName].AsEnumerable()
                        .Select(row => new Customers
                        {
                            CustomerID = row.Field<int>("CustomerID"),
                            UserName = row.Field<string>("UserName"),
                            Password = row.Field<string>("Password"),
                            Last_Name = row.Field<string>("Last_Name"),
                            First_Name = row.Field<string>("First_Name"),
                            BirthDate = row.Field<DateTime>("BirthDate"),
                            Address = row.Field<string>("Address"),
                            City = row.Field<string>("City"),
                            Phone = row.Field<string>("Phone")
                        })
                        .ToList();

                    return View("~/Views/Administration/Customers/Index.cshtml", customerList);
                }
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            Customers customer = GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/Customers/Details.cshtml", customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            Customers customer = GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/Customers/Edit.cshtml", customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customers customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"UPDATE {tableName} SET UserName = @UserName, Password = @Password, " +
                                 $"Last_Name = @Last_Name, First_Name = @First_Name, " +
                                 $"BirthDate = @BirthDate, Address = @Address, City = @City, " +
                                 $"Phone = @Phone WHERE CustomerID = {id}";
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
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index", "Customers");
            }
            catch
            {
                return View("~/Views/Administration/Customers/Edit.cshtml");
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            Customers customer = GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/Customers/Delete.cshtml", customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customers customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"DELETE FROM {tableName} WHERE CustomerID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index", "Customers");
            }
            catch
            {
                return View("~/Views/Administration/Customers/Delete.cshtml");
            }
        }
    }
}
