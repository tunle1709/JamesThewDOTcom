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
    public class EmployeesController : Controller
    {
        private string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;
        private string tableName = "Employees";

        private Employees GetEmployeeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName} WHERE EmployeeID = {id}";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        Employees employee = new Employees
                        {
                            EmployeeID = (int)dataTable.Rows[0]["EmployeeID"],
                            UserName = dataTable.Rows[0]["UserName"].ToString(),
                            Password = dataTable.Rows[0]["Password"].ToString(),
                            Last_Name = dataTable.Rows[0]["Last_Name"].ToString(),
                            First_Name = dataTable.Rows[0]["First_Name"].ToString(),
                            BirthDate = (DateTime)dataTable.Rows[0]["BirthDate"],
                            Address = dataTable.Rows[0]["Address"].ToString(),
                            City = dataTable.Rows[0]["City"].ToString(),
                            Region = dataTable.Rows[0]["Region"].ToString(),
                            PhoneNumber = dataTable.Rows[0]["PhoneNumber"].ToString()
                        };
                        return employee;
                    }
                }
            }
            return null;
        }

        // GET: Employees
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

                    List<Employees> employeeList = dataSet.Tables[tableName].AsEnumerable()
                        .Select(row => new Employees
                        {
                            EmployeeID = row.Field<int>("EmployeeID"),
                            UserName = row.Field<string>("UserName"),
                            Password = row.Field<string>("Password"),
                            Last_Name = row.Field<string>("Last_Name"),
                            First_Name = row.Field<string>("First_Name"),
                            BirthDate = row.Field<DateTime>("BirthDate"),
                            Address = row.Field<string>("Address"),
                            City = row.Field<string>("City"),
                            Region = row.Field<string>("Region"),
                            PhoneNumber = row.Field<string>("PhoneNumber")
                        })
                        .ToList();

                    return View(employeeList);
                }
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            Employees employee = GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employees employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"INSERT INTO {tableName} (UserName, Password, Last_Name, First_Name, BirthDate, Address, City, Region, PhoneNumber) " +
                                 $"VALUES (@UserName, @Password, @Last_Name, @First_Name, @BirthDate, @Address, @City, @Region, @PhoneNumber)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", employee.UserName);
                        cmd.Parameters.AddWithValue("@Password", employee.Password);
                        cmd.Parameters.AddWithValue("@Last_Name", employee.Last_Name);
                        cmd.Parameters.AddWithValue("@First_Name", employee.First_Name);
                        cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                        cmd.Parameters.AddWithValue("@Address", employee.Address);
                        cmd.Parameters.AddWithValue("@City", employee.City);
                        cmd.Parameters.AddWithValue("@Region", employee.Region);
                        cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            Employees employee = GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employees employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"UPDATE {tableName} SET UserName = @UserName, Password = @Password, " +
                                 $"Last_Name = @Last_Name, First_Name = @First_Name, " +
                                 $"BirthDate = @BirthDate, Address = @Address, City = @City, Region = @Region, " +
                                 $"PhoneNumber = @PhoneNumber WHERE EmployeeID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", employee.UserName);
                        cmd.Parameters.AddWithValue("@Password", employee.Password);
                        cmd.Parameters.AddWithValue("@Last_Name", employee.Last_Name);
                        cmd.Parameters.AddWithValue("@First_Name", employee.First_Name);
                        cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                        cmd.Parameters.AddWithValue("@Address", employee.Address);
                        cmd.Parameters.AddWithValue("@City", employee.City);
                        cmd.Parameters.AddWithValue("@Region", employee.Region);
                        cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            Employees employee = GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employees employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"DELETE FROM {tableName} WHERE EmployeeID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
