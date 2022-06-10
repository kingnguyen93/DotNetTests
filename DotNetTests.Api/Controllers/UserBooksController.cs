using DotNetTests.Application.Commands.CreateUserBook;
using DotNetTests.Application.Queries.GetUserBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Api.Controllers
{
    [Route("api/user-books")]
    [ApiController]
    public class UserBooksController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserBooksController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserBooksQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserBookCommand command)
        {
            var result = await mediator.Send(command);
            return result ? Ok("Success") : NotFound("Bad Request");
        }
    }
}
