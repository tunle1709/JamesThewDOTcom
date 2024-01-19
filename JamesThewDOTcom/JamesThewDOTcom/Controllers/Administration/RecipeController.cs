using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using JamesThewDOTcom.Models;

namespace JamesThewDOTcom.Controllers.Administration
{
    public class RecipeController : Controller
    {

        private string strconn = ConfigurationManager.ConnectionStrings["JamesThewDB"].ConnectionString;

        private string tableName = "Recipe";
        private Recipe GetRecipeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(strconn))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName} WHERE RecipeID = {id}";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        Recipe recipe = new Recipe
                        {
                            RecipeID = (int)dataTable.Rows[0]["RecipeID"],
                            Title = dataTable.Rows[0]["Title"].ToString(),
                            Ingredints = dataTable.Rows[0]["Ingredints"].ToString(),
                            Steps = dataTable.Rows[0]["Steps"].ToString(),
                            RecipeType = dataTable.Rows[0]["RecipeType"].ToString()
                        };
                        return recipe;
                    }
                }
            }
            return null;
        }

        // GET: Recipe
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

                    List<Recipe> recipeList = dataSet.Tables[tableName].AsEnumerable()
                        .Select(row => new Recipe
                        {
                            RecipeID = row.Field<int>("RecipeID"),
                            Title = row.Field<string>("Title"),
                            Ingredints = row.Field<string>("Ingredints"),
                            Steps = row.Field<string>("Steps"),
                            RecipeType = row.Field<string>("RecipeType")
                        })
                        .ToList();

                    return View("~/Views/Administration/Recipe/Index.cshtml", recipeList);
                }
            }
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int id)
        {
            Recipe recipe = GetRecipeById(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/Recipe/Details.cshtml", recipe);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            return View("~/Views/Administration/Recipe/Create.cshtml");
        }


        // POST: Recipe/Create
        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"INSERT INTO {tableName} (Title, Ingredints, Steps, RecipeType) " +
                                 $"VALUES (@Title, @Ingredints, @Steps, @RecipeType)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", recipe.Title);
                        cmd.Parameters.AddWithValue("@Ingredints", recipe.Ingredints);
                        cmd.Parameters.AddWithValue("@Steps", recipe.Steps);
                        cmd.Parameters.AddWithValue("@RecipeType", recipe.RecipeType);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("~/Views/Administration/Recipe/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Administration/Recipe/Create.cshtml");
            }
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            Recipe recipe = GetRecipeById(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/Recipe/Edit.cshtml", recipe);
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Recipe recipe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"UPDATE {tableName} SET Title = @Title, Ingredints = @Ingredints, " +
                                 $"Steps = @Steps, RecipeType = @RecipeType WHERE RecipeID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", recipe.Title);
                        cmd.Parameters.AddWithValue("@Ingredints", recipe.Ingredints);
                        cmd.Parameters.AddWithValue("@Steps", recipe.Steps);
                        cmd.Parameters.AddWithValue("@RecipeType", recipe.RecipeType);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("~/Views/Administration/Recipe/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Administration/Recipe/Edit.cshtml");
            }
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            Recipe recipe = GetRecipeById(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Administration/Recipe/Delete.cshtml", recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Recipe recipe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strconn))
                {
                    connection.Open();
                    string sql = $"DELETE FROM {tableName} WHERE RecipeID = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("~/Views/Administration/Recipe/Index.cshtml");
            }
            catch
            {
                return View("~/Views/Administration/Recipe/Delete.cshtml");
            }
        }
    }
}
