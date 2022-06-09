using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.AutoMapper
{
    public interface IMapFrom<T> where T : new()
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
