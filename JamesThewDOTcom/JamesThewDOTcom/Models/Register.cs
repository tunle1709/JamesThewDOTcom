using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;

namespace JamesThewDOTcom.Models
{
    public class Register
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomersTypeID { get; set; }
        public int PaymentTypeID { get; set; }
    }
}