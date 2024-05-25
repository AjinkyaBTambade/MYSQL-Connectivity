using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreNew.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishedDate { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}

