using DotNetTests.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IEnumerable<Book>>
    {
        public string SearchString { get; set; }
    }
}
