using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemData.Models;

namespace SystemData
{
    public class SystemContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
    
        public SystemContext(DbContextOptions options)
      : base(options)
        {
        }

        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<Boxes> Boxes { get; set; }
        public virtual DbSet<Region> Region { get; set; }

        public virtual DbSet<Customer> Customer { get; set; }
         public virtual DbSet<CustomerIdentity> CustomerIdentity { get; set; }
        public virtual DbSet<CustomerJobs> CustomerJobs { get; set; }
        public virtual DbSet<CustomerType> CustomerType { get; set; }
        public virtual DbSet<DetailsCost> DetailsCost { get; set; }
        public virtual DbSet<DetailsRent> DetailsRent { get; set; }
        public virtual DbSet<ExtraCost> ExtraCost { get; set; }
        public virtual DbSet<OtherDetailsRent> OtherDetailsRent { get; set; }
        public virtual DbSet<TypeBox> TypeBox { get; set; }
         public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<NoteType> NoteType { get; set; }
        public virtual DbSet<NoteTypeBills> NoteTypeBills { get; set; }
        public virtual DbSet<PayType> PayType { get; set; }

        public virtual DbSet<Test> Test { get; set; }

    }
}

