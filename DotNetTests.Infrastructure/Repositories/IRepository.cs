using DotNetTests.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Infrastructure.Repositories
{
    public interface IRepository<TEntity, in TKey>
        where TKey : IEquatable<TKey>
    {
        ValueTask<IReadOnlyList<TEntity>> ListAllAsync();
        ValueTask<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);
        ValueTask<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);

        ValueTask<TEntity> GetByIdAsync(TKey id);

        ValueTask<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec);
        ValueTask<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        ValueTask<int> CountAsync();
        ValueTask<int> CountAsync(ISpecification<TEntity> spec);
        ValueTask<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        ValueTask<bool> AnyAsync(ISpecification<TEntity> spec);
        ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        ValueTask<bool> AllAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, bool>> predicate);

        ValueTask<TEntity> AddAsync(TEntity entity);

        ValueTask<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
