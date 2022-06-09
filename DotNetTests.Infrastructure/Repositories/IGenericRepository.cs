using DotNetTests.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Infrastructure.Repositories
{
    public interface IGenericRepository<TEntity, in TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
