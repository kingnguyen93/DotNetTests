using DotNetTests.Application.Commands.CreateBook;
using DotNetTests.Application.Commands.DeleteBook;
using DotNetTests.Application.Commands.UpdateBook;
using DotNetTests.Application.Commands.UserRead;
using DotNetTests.Application.Queries.GetBook;
using DotNetTests.Application.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator mediator;

        public BooksController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetBooksQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await mediator.Send(new GetBookQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBookCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);
            return result ? Ok("Success") : NotFound("Not Found");
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> Read(Guid id, [FromBody] UserReadCommand command)
        {
            command.BookId = id;
            var result = await mediator.Send(command);
            return result ? Ok("Success") : NotFound("Not Found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteBookCommand(id));
            return result ? Ok("Success") : NotFound("Not Found");
        }
    }
}
