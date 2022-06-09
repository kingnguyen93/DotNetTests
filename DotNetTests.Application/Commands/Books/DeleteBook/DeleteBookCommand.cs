using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}
