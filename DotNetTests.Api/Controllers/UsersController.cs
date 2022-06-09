using DotNetTests.Application.Commands.CreateUser;
using DotNetTests.Application.Queries.GetUserReadBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("{id}/books/read")]
        public async Task<IActionResult> GetReadBooks(Guid id)
        {
            return Ok(await mediator.Send(new GetUserReadBooksQuery(id)));
        }
    }
}
