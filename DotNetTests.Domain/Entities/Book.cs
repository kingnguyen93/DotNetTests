using DotNetTests.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Domain.Entities
{
    public class Book : Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
