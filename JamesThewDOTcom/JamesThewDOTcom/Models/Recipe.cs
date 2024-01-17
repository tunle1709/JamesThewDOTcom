using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamesThewDOTcom.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Ingredints { get; set; }
        public string Steps { get; set; }
        public string RecipeType { get; set; }
        public int EmployeeID { get; set; }
    }
}