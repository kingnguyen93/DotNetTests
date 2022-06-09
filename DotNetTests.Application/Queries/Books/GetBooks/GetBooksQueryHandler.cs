using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using DotNetTests.Infrastructure.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
    {
        private readonly IGenericRepository<Book, Guid> bookRepository;

        public GetBooksQueryHandler(IGenericRepository<Book, Guid> bookRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await bookRepository.ListAsync(new GetBooksSpecification(request.SearchString));
        }
    }
}
