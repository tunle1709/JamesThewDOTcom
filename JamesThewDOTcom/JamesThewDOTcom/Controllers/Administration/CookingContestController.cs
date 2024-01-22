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
    public class CookingContestController : Controller
    {
        private string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;
        private string tableName = "CookingContest";

        private CookingContest GetCookingContestById(int id)
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName} WHERE CookingContestID = {id}";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        CookingContest cookingContest = new CookingContest
                        {
                            CookingContestID = (int)dataTable.Rows[0]["CookingContestID"],
                            Title = dataTable.Rows[0]["Title"].ToString(),
                            Description = dataTable.Rows[0]["Description"].ToString(),
                            StartDate = (DateTime)dataTable.Rows[0]["StartDate"],
                            EndDate = (DateTime)dataTable.Rows[0]["EndDate"],                     
                        };
                        return cookingContest ;
                    }
                }
            }
            return null;
        }

        // GET: CookingContest
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

                    List<CookingContest> CookingContestList = dataSet.Tables[tableName].AsEnumerable()
                        .Select(row => new CookingContest
                        {
                            CookingContestID = row.Field<int>("CookingContestID"),
                            Title = row.Field<string>("Title"),
                            Description = row.Field<string>("Description"),
                            StartDate = row.Field<DateTime>("StartDate"),
                            EndDate = row.Field<DateTime>("EndDate"),
                        })
                        .ToList();

                    return View("~/Views/Administration/CookingContest/Index.cshtml", CookingContestList);
                }
            }
        }
        // GET: CookingContest/Create
        public ActionResult Create()
        {
            return View("~/Views/Administration/CookingContest/Create.cshtml");
        }

        // POST: CookingContest/Create
        [HttpPost]
        public ActionResult Create(CookingContest cookingContest)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"INSERT INTO {tableName} (Title, Description, StartDate, EndDate) " +
                                 $"VALUES (@Title, @Description, @StartDate, @EndDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", cookingContest.Title);
                        cmd.Parameters.AddWithValue("@Description", cookingContest.Description);
                        cmd.Parameters.AddWithValue("@StartDate", cookingContest.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", cookingContest.EndDate);

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index" , "CookingContest" );
            }
            catch
            {
                return View("~/Views/Administration/CookingContest/Create.cshtml");
            }
        }


        // GET: CookingContest/Details/5
        public ActionResult Details(int id)
        {
            CookingContest cookingContest = GetCookingContestById(id);
            if (cookingContest == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/CookingContest/Details.cshtml", cookingContest);
        }

        // GET: CookingContest/Edit/5
        public ActionResult Edit(int id)
        {
            CookingContest cookingContest = GetCookingContestById(id);
            if (cookingContest == null) if (cookingContest == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/CookingContest/Edit.cshtml", cookingContest);
        }

        // POST: CookingContest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CookingContest cookingContest)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"UPDATE {tableName} SET Title = @Title, Description = @Description, " +
                                 $"StartDate = @StartDate, EndDate = @EndDate WHERE CookingContestID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", cookingContest.Title);
                        cmd.Parameters.AddWithValue("@Description", cookingContest.Description);
                        cmd.Parameters.AddWithValue("@StartDate", cookingContest.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", cookingContest.EndDate);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("~/Views/Administration/CookingContest/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Administration/CookingContest/Edit.cshtml");
            }
        }

        // GET: CookingContest/Delete/5
        public ActionResult Delete(int id)
        {
            CookingContest cookingContest = GetCookingContestById(id);
            if (cookingContest == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/CookingContest/Delete.cshtml", cookingContest);
        }

        // POST: CookingContest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CookingContest cookingContest)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"DELETE FROM {tableName} WHERE CookingContestID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("~/Views/Administration/CookingContest/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Administration/CookingContest/Delete.cshtml");
            }
        }
    }
}
