using EntityFrameworkForMultipleTables.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkForMultipleTables.DbContexts
{
    public class BlogContext : DbContext
    {
        private string connectionString = "Server=localhost;Port=3306;User=root;Password=password;Database=entityframeworkdb";

        public DbSet<Author> Author { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 02)) 
            );
        }
    }
}
