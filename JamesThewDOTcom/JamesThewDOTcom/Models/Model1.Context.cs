﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JamesThewDBEntities : DbContext
    {
        public JamesThewDBEntities()
            : base("name=JamesThewDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ContestResult> ContestResults { get; set; }
        public virtual DbSet<CookingContest> CookingContests { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Object> Objects { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeType> RecipeTypes { get; set; }
        public virtual DbSet<Register> Registers { get; set; }
        public virtual DbSet<RegiterContest> RegiterContests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
    }
}
