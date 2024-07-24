
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbContexts
{
   public class EmployeeContext : DbContext
    {   
        public DbSet<Employee> Employees { get; set; }

        public string connectionString = @"Data Source=DESKTOP-0E2DNFV\SQLEXPRESS;Initial Catalog=EntityFramework;Integrated Security=True;Connect Timeout=30;Encrypt=False";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

