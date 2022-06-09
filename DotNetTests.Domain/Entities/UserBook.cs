using DotNetTests.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Domain.Entities
{
    public class UserBook : Entity
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public int Status { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }
    }
}
