using AutoMapper;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Queries.GetUserBooks
{
    public class GetUserBooksQueryHandler : IRequestHandler<GetUserBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IMapper mapper;
        private readonly IBookRepository bookRepository;

        public GetUserBooksQueryHandler(IMapper mapper, IBookRepository bookRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<IEnumerable<BookDto>> Handle(GetUserBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await bookRepository.GetUserBooksAsync(request.UserId, request.SearchString);
            return mapper.Map<IEnumerable<BookDto>>(result);
        }
    }
}
