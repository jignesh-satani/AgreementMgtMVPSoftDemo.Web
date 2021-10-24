using AgreementMgtMVPSoftDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class ApplicationContext : DbContext
     {
          public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               base.OnModelCreating(modelBuilder);
          }
          public DbSet<AspNetUsers> AspNetUsers { get; set; }
          public DbSet<ProductGroup> ProductGroup { get; set; }
          public DbSet<Product> Product { get; set; }
          public DbSet<Agreement> Agreement { get; set; }
          public DbSet<UserAgreements> UserAgreements { get; set; }

     }
}
