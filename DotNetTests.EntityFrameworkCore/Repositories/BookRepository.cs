using DotNetTests.Domain.Entities;
using DotNetTests.EntityFrameworkCore.Extensions;
using DotNetTests.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.EntityFrameworkCore.Repositories
{
    public class BookRepository : GenericRepository<Book, Guid>, IBookRepository
    {
        public BookRepository(AppContext context) : base(context)
        {
        }

        public async ValueTask<IReadOnlyList<Book>> GetUserReadBookAsync(Guid userId, string searchString)
        {
            return await DbSet
                .Where(b => b.Users.Select(u => u.Id).Contains(userId))
                .WhereIf(!string.IsNullOrWhiteSpace(searchString), b => b.Name.ToUpper().Contains(searchString.ToUpper()))
                .ToListAsync();
        }
    }
}
