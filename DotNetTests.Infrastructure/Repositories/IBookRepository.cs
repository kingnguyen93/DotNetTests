using DotNetTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Infrastructure.Repositories
{
    public interface IBookRepository : IGenericRepository<Book, Guid>
    {
        ValueTask<IReadOnlyList<Book>> GetUserReadBookAsync(Guid userId);
    }
}
