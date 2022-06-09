using AutoMapper;
using DotNetTests.Application.AutoMapper;
using DotNetTests.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Queries.GetBook
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public Guid Id { get; set; }

        public GetBookQuery(Guid id)
        {
            Id = id;
        }
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
