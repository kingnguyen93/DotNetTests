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
    public class GenericRepositoryTests : TestBase
    {
        private readonly IGenericRepository<Book, Guid> bookRepository;

        public GenericRepositoryTests()
        {
            bookRepository = ResolveService<IGenericRepository<Book, Guid>>();
        }

        [TestMethod]
        public async Task GenericRepository_Create_OK()
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
        public async Task GenericRepository_Get_OK()
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
        public async Task GenericRepository_Find_OK()
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
        public async Task GenericRepository_Update_OK()
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
            await bookRepository.UnitOfWork.SaveChangesAsync();
            var book2 = await bookRepository.GetByIdAsync(book.Id);
            Assert.IsTrue(book2.Name == "Test2");
        }

        [TestMethod]
        public async Task GenericRepository_Delete_OK()
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
            await bookRepository.UnitOfWork.SaveChangesAsync();
            var book2 = await bookRepository.GetByIdAsync(book.Id);
            Assert.IsNull(book2);
        }
    }
}
