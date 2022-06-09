using DotNetTests.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.CreateUser
{
    public  class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }
    }
}
