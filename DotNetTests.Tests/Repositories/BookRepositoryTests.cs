using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Tests.Repositories
{
    [TestClass]
    public class BookRepositoryTests : TestBase
    {
        private readonly IBookRepository bookRepository;

        public BookRepositoryTests()
        {
            bookRepository = ResolveService<IBookRepository>();
        }

        [TestMethod]
        public async Task BookRepository_Create_OK()
        {
            var book = await bookRepository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync();
            Assert.IsFalse(book.Id.Equals(Guid.Empty));
        }

        [TestMethod]
        public async Task BookRepository_Get_OK()
        {
            var book = await bookRepository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync();
            var book2 = await bookRepository.GetByIdAsync(book.Id);
            Assert.IsNotNull(book2);
        }

        [TestMethod]
        public async Task BookRepository_Find_OK()
        {
            var book = await bookRepository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync();
            var book2 = await bookRepository.FirstOrDefaultAsync(x => x.Id == book.Id);
            Assert.IsNotNull(book2);
        }

        [TestMethod]
        public async Task BookRepository_Update_OK()
        {
            var book = await bookRepository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync();
            book.Name = "Test2";
            await bookRepository.UnitOfWork.SaveChangesAsync(book.CreatedBy);
            var book2 = await bookRepository.GetByIdAsync(book.Id);
            Assert.IsTrue(book2.Name == "Test2");
        }

        [TestMethod]
        public async Task BookRepository_Delete_OK()
        {
            var book = await bookRepository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
            });
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync();
            await bookRepository.DeleteAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync(book.CreatedBy);
            var book2 = await bookRepository.GetByIdAsync(book.Id);
            Assert.IsNull(book2);
        }

        [TestMethod]
        public async Task BookRepository_GetUserBooks_OK()
        {
            var book = await bookRepository.AddAsync(new Book
            {
                Name = "Test",
                Description = "Test",
                Author = "Test",
                Users = new List<User>
                {
                    new User
                    {
                        Name = "User"
                    }
                }
            });
            await bookRepository.AddAsync(book);
            await bookRepository.UnitOfWork.SaveChangesAsync();
            var userId = book.Users.FirstOrDefault()?.Id ?? Guid.Empty;
            var books = await bookRepository.GetUserBooksAsync(userId, default);
            Assert.IsTrue(books.Count > 0);
        }
    }
}
