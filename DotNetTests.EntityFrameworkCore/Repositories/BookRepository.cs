﻿using DotNetTests.Domain.Entities;
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

        public async ValueTask<IReadOnlyList<Book>> GetUserReadBookAsync(Guid userId)
        {
            return await DbSet.Where(b => b.Users.Select(u => u.Id).Contains(userId)).ToListAsync();
        }
    }
}