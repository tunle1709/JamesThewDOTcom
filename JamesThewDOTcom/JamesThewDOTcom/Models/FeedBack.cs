using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace JamesThewDOTcom.Models
{
    public class FeedBack
    {
        public int FeedBackID { get; set; }
        public string Comment { get; set; }
        public int CustomerID { get; set; }
        public int RecipeID { get; set; }
    }
}