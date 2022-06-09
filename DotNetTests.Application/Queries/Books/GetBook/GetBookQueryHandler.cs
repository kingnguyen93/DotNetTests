using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IMapperGenericRepository<Book, Guid> bookRepository;

        public GetBookQueryHandler(IMapperGenericRepository<Book, Guid> bookRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            return await bookRepository.FirstOrDefaultAsync<BookDto>(b => b.Id == request.Id);
        }
    }
}
