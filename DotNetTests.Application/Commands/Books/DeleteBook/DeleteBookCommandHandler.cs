using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IGenericRepository<Book, Guid> bookRepository;

        public DeleteBookCommandHandler(IGenericRepository<Book, Guid> bookRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                return false;
            }
            await bookRepository.DeleteAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
