using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamesThewDOTcom.Models
{
    public class CookingContest
    {
        public int CookingContestID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}