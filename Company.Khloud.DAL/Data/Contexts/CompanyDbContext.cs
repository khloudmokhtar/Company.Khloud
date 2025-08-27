using Company.Khloud.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Khloud.DAL.Data.Contexts
{
    internal class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base ()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server = .;Database = CompanyG02; Trusted_Connection = True ; TrustedServserCertificate = True ");
        }


        public DbSet<Department>  Departments { get; set; }
    }


}
