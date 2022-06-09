using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using DotNetTests.Infrastructure.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.UserRead
{
    public class UserReadCommandHandler : IRequestHandler<UserReadCommand, bool>
    {
        private readonly IGenericRepository<Book, Guid> bookRepository;
        private readonly IGenericRepository<User, Guid> userRepository;

        public UserReadCommandHandler(IGenericRepository<Book, Guid> bookRepository, IGenericRepository<User, Guid> userRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(UserReadCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.FirstOrDefaultAsync(new GetBookSpecification(request.BookId));
            if (book == null)
            {
                return false;
            }
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return false;
            }
            book.Users.Add(user);
            await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
