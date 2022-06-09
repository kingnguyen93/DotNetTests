using DotNetTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Infrastructure.Specifications
{
    public class GetBooksSpecification : SpecificationBase<Book>
    {
        public GetBooksSpecification(string searchString)
        {
            AddConditionIf(!string.IsNullOrWhiteSpace(searchString), e => e.Name.ToUpper().Contains(searchString.ToUpper()));

            ApplyOrderByDescending(e => e.CreatedTime);
        }
    }

    public class GetBookSpecification : SpecificationBase<Book>
    {
        public GetBookSpecification(Guid id) : base(b => b.Id == id)
        {
            AddInclude(b => b.Users);
        }
    }
}
