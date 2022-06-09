using DotNetTests.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
    }
}
