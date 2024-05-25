using EFCoreNew.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNew.DbContexts
{
    public class BlogContext : DbContext
    {
        private readonly string connectionString = @"Data Source=DESKTOP-0E2DNFV\SQLEXPRESS;Initial Catalog=EntityFrameworkExample;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .ToTable("Author")
                .HasMany(a => a.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog>()
                .ToTable("Blog")
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .ToTable("Post");
        }
    }
}
