using System;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbContexts
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        private string connectionString = "Server=localhost;Port=3306;User=root;Password=password;Database=assessmentdb";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 02)) 
            );
        }
    }
}
