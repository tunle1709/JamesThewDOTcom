using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;

namespace JamesThewDOTcom.Models
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PhoneNumber { get; set; }
    }
}