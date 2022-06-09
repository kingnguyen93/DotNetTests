using DotNetTests.EntityFrameworkCore.Repositories;
using DotNetTests.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection ResolveApplicationDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(ApplicationModule).Assembly);
            serviceCollection.AddFluentValidation().AddValidatorsFromAssemblies(new[] { typeof(ApplicationModule).Assembly });
            serviceCollection.AddAutoMapper(mc =>
            {
                mc.AllowNullCollections = true;
            },
            new Assembly[] { Assembly.GetExecutingAssembly() },
            ServiceLifetime.Singleton);

            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
            serviceCollection.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            serviceCollection.AddScoped(typeof(IMapperGenericRepository<,>), typeof(MapperGenericRepository<,>));

            serviceCollection.AddScoped<IBookRepository, BookRepository>();

            return serviceCollection;
        }
    }
}
