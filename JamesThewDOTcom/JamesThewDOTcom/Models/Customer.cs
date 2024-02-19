//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JamesThewDOTcom.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.FeedBacks = new HashSet<FeedBack>();
            this.RegiterContests = new HashSet<RegiterContest>();
        }

        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public System.DateTime BirthDate { get; set; }
        public Nullable<int> CustomersTypeID { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }

        public virtual ContestResult ContestResult { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedBack> FeedBacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegiterContest> RegiterContests { get; set; }
    }
}