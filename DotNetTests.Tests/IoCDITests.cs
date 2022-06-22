using AutoMapper;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using DotNetTests.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TinyERP.Tests
{
    [TestClass]
    public class IoCDITests : TestBase
    {
        [TestMethod]
        public void IoC_DI_ServiceProvider_OK()
        {
            Assert.IsNotNull(_serviceProvider);
        }

        [TestMethod]
        public void IoC_DI_Mapper_OK()
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            Assert.IsNotNull(mapper);
        }

        [TestMethod]
        public void IoC_DI_GenericRepository_OK()
        {
            var service = _serviceProvider.GetRequiredService<IGenericRepository<Book, Guid>>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void IoC_DI_MapperGenericRepository_OK()
        {
            var service = _serviceProvider.GetRequiredService<IMapperGenericRepository<Book, Guid>>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void IoC_DI_BookRepository_OK()
        {
            var service = _serviceProvider.GetRequiredService<IBookRepository>();
            Assert.IsNotNull(service);
        }
    }
}