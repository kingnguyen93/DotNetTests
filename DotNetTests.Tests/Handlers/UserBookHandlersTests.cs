using DotNetTests.Application.Commands.CreateUserBook;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Tests.Handlers
{
    [TestClass]
    public class UserBookHandlersTests : TestBase
    {
        public UserBookHandlersTests()
        {
        }

        [TestMethod]
        public async Task CreateUserBookCommand_OK()
        {
            var repository1 = ResolveService<IGenericRepository<Book, Guid>>();
            var repository2 = ResolveService<IGenericRepository<User, Guid>>();
            var book = await repository1.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            var user = await repository2.AddAsync(new User
            {
                Name = "User"
            });
            await repository1.UnitOfWork.SaveChangesAsync();
            var command = new CreateUserBookCommand
            {
                BookId = book.Id,
                UserId = user.Id
            };
            var handler = new CreateUserBookCommandHandler(repository1, repository2);
            var result = await handler.Handle(command, default);
            Assert.IsTrue(result);
        }
    }
}
