﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace zestork.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ZestorkContainer : DbContext
    {
        public ZestorkContainer()
            : base("name=ZestorkContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Users> Users { get; set; }
        public DbSet<ValidateUserKey> ValidateUserKeys { get; set; }
        public DbSet<ThirdPartyLogin> ThirdPartyLogins { get; set; }
        public DbSet<LinkedInAuthApiData> LinkedInAuthApiDatas { get; set; }
        public DbSet<ClientDetails> ClientDetails { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserPageSetting> UserPageSettings { get; set; }
        public DbSet<ForgetPassword> ForgetPasswords { get; set; }
        public DbSet<UserRecommendation> UserRecommendations { get; set; }
        public DbSet<RecommendedBy> RecommendedBies { get; set; }
        public DbSet<JobData> JobDatas { get; set; }
    }
}
