using AutoMapper;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<Book, Guid> bookRepository;

        public CreateBookCommandHandler(IMapper mapper, IGenericRepository<Book, Guid> bookRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = mapper.Map<Book>(request);
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return book;
        }
    }
}
