using DotNetTests.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
