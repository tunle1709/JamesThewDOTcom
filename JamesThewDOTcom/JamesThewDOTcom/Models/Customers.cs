using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;

namespace JamesThewDOTcom.Models
{
    public class Customers
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int CustomersTypeID { get; set; }
        public int PaymentTypeID { get; set; }
    }
}