using EntityFrameworkForMultipleTables.DbContexts;
using EntityFrameworkForMultipleTables.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkForMultipleTables.Repositories
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
            var context = new BlogContext();
            {
                var authorsInfo = context.Author.Include(a => a.Blogs).ThenInclude(b => b.Posts).ToList();

                foreach (var author in authorsInfo)
                {
                    Console.WriteLine($"Author: {author.Name}");
                    foreach (var blog in author.Blogs)
                    {
                        Console.WriteLine($"Blog Title : {blog.Title}");
                        foreach (var post in blog.Posts)
                        {
                            Console.WriteLine($"Post Title : {post.Title}");
                        }
                    }

                }
            }
        }

        public void AddNew()
        {
            var context = new BlogContext();

            var author = new Author { Name = "Ravi Tambade" };
            author.Blogs = new List<Blog> {
                 new Blog{Title = "education" ,
                    Posts= new List<Post>
                    {
                        new Post{Title="coding" },
                        new Post{Title="programming" }
                    }
                },
                new Blog{Title = "teaching" ,
                    Posts= new List<Post>
                    {
                        new Post{Title="c#" },
                        new Post{Title="dot net"}
                    }
                }

            };

            context.Author.Add(author);
            context.SaveChanges();
        }
    }
}
