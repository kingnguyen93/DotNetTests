using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.EntityFrameworkCore
{
    public static class EntityFrameworkCoreModule
    {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<AppContext>(opt => opt.UseInMemoryDatabase("DotNetTests"));
            return services;
        }
    }
}
