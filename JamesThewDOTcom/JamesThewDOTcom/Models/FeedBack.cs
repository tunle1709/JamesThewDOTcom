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
    
    public partial class FeedBack
    {
        public int FeedBackID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> RecipeID { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
