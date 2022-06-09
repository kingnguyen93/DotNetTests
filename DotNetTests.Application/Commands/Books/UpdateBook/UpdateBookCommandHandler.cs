using AutoMapper;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<Book, Guid> bookRepository;

        public UpdateBookCommandHandler(IMapper mapper, IGenericRepository<Book, Guid> bookRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                return false;
            }
            mapper.Map(request, book);
            await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
