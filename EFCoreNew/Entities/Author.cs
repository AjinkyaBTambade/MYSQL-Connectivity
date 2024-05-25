using EFCoreNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreNew.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
