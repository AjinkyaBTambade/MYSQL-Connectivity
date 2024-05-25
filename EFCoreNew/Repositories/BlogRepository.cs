using EFCoreNew.DbContexts;
using EFCoreNew.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreNew.Repositories
{
    public class BlogRepository
    {
            private BlogContext _blogContext;

            public BlogRepository(BlogContext blogContext)
            {
                _blogContext = blogContext;
            }


            public void ShowAll()
        {
            var authorsInfo = _blogContext.Authors.Include(a => a.Blogs).ThenInclude(b => b.Posts).ToList();

            foreach (var author in authorsInfo)
            {
                Console.WriteLine($"Author: {author.Name}");
                foreach (var blog in author.Blogs)
                {
                    Console.WriteLine($"Blog Title: {blog.Title}");
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($"Post Title: {post.Title}");
                    }
                }
            }
        }

        public void AddNew()
        {
            var author = new Author { Name = "Ravi Tambade" };
            author.Blogs = new List<Blog>
            {
                new Blog
                {
                    Title = "Education",
                    Content = "Content about education.",
                    PublishedDate = DateTime.Now,
                    Posts = new List<Post>
                    {
                        new Post { Title = "Coding", Description = "Description about coding.", Content = "Detailed content about coding.", PublishedDate = DateTime.Now },
                        new Post { Title = "Programming", Description = "Description about programming.", Content = "Detailed content about programming.", PublishedDate = DateTime.Now }
                    }
                },
                new Blog
                {
                    Title = "Teaching",
                    Content = "Content about teaching.",
                    PublishedDate = DateTime.Now,
                    Posts = new List<Post>
                    {
                        new Post { Title = "C#", Description = "Description about C#.", Content = "Detailed content about C#.", PublishedDate = DateTime.Now },
                        new Post { Title = ".NET", Description = "Description about .NET.", Content = "Detailed content about .NET.", PublishedDate = DateTime.Now }
                    }
                }
            };

            _blogContext.Authors.Add(author);
            _blogContext.SaveChanges();
        }
    }
}
