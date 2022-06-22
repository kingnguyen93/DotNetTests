using AutoMapper;
using DotNetTests.Application.Commands.CreateBook;
using DotNetTests.Application.Commands.DeleteBook;
using DotNetTests.Application.Commands.UpdateBook;
using DotNetTests.Application.Queries.GetBook;
using DotNetTests.Application.Queries.GetBooks;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using DotNetTests.Infrastructure.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Tests.Queries
{
    [TestClass]
    public class BookHandlersTests : TestBase
    {
        public BookHandlersTests()
        {
        }

        [TestMethod]
        public async Task GetBooksQuery_OK()
        {
            var repository = new Mock<IGenericRepository<Book, Guid>>();
            var guid = Guid.NewGuid();
            var request = new GetBooksQuery();
            repository.Setup(r => r.ListAsync(new GetBooksSpecification(request.SearchString))).ReturnsAsync(new List<Book>() { new Book() });
            var handler = new GetBooksQueryHandler(repository.Object);
            var result = await handler.Handle(request, default);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 1);
        }

        [TestMethod]
        public async Task GetBookQuery_OK()
        {
            var repository = new Mock<IMapperGenericRepository<Book, Guid>>();
            var guid = Guid.NewGuid();
            var query = new GetBookQuery(guid);
            repository.Setup(r => r.FirstOrDefaultAsync<BookDto>(b => b.Id == query.Id)).ReturnsAsync(new BookDto());
            var handler = new GetBookQueryHandler(repository.Object);
            var result = await handler.Handle(query, default);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateBookCommand_OK()
        {
            var mapper = ResolveService<IMapper>();
            var repository = ResolveService<IGenericRepository<Book, Guid>>();
            var command = new CreateBookCommand
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            };
            var handler = new CreateBookCommandHandler(mapper, repository);
            var result = await handler.Handle(command, default);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateBookCommand_OK()
        {
            var mapper = ResolveService<IMapper>();
            var repository = ResolveService<IGenericRepository<Book, Guid>>();
            var book = await repository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await repository.UnitOfWork.SaveChangesAsync();
            var command = new UpdateBookCommand
            {
                Id = book.Id,
                Name = "Test2",
                Description = "Test",
                Author = "Test",
            };
            var handler = new UpdateBookCommandHandler(mapper, repository);
            var result = await handler.Handle(command, default);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteBookCommand_OK()
        {
            var repository = ResolveService<IGenericRepository<Book, Guid>>();
            var book = await repository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await repository.UnitOfWork.SaveChangesAsync();
            var command = new DeleteBookCommand(book.Id);
            var handler = new DeleteBookCommandHandler(repository);
            var result = await handler.Handle(command, default);
            Assert.IsTrue(result);
        }
    }
}
