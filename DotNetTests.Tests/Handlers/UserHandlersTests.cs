using AutoMapper;
using DotNetTests.Application.Commands.CreateUser;
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
    public class UserHandlersTests : TestBase
    {
        public UserHandlersTests()
        {
        }

        [TestMethod]
        public async Task CreateUserCommand_OK()
        {
            var mapper = ResolveService<IMapper>();
            var repository = ResolveService<IGenericRepository<User, Guid>>();
            var command = new CreateUserCommand
            {
                Name = "User"
            };
            var handler = new CreateUserCommandHandler(mapper, repository);
            var result = await handler.Handle(command, default);
            Assert.IsNotNull(result);
        }
    }
}
