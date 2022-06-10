using AutoMapper;
using DotNetTests.Application.AutoMapper;
using DotNetTests.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Queries.GetUserBooks
{
    public class GetUserBooksQuery : IRequest<IEnumerable<BookDto>>
    {
        public Guid UserId { get; set; }

        public string SearchString { get; set; }
    }

    public class BookDto : IMapFrom<Book>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDto>();
        }
    }
}
