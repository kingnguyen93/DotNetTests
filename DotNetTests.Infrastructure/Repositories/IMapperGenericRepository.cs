using DotNetTests.Domain.Common;
using DotNetTests.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Infrastructure.Repositories
{
    public interface IMapperGenericRepository<TEntity, in TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        ValueTask<IReadOnlyList<TModel>> ListAllAsync<TModel>() where TModel : class;
        ValueTask<IReadOnlyList<TModel>> ListAsync<TModel>(ISpecification<TEntity> spec) where TModel : class;
        ValueTask<IReadOnlyList<TModel>> ListAsync<TModel>(Expression<Func<TEntity, bool>> criteria) where TModel : class;

        ValueTask<TModel> GetByIdAsync<TModel>(TKey id) where TModel : class;

        ValueTask<TModel> FirstOrDefaultAsync<TModel>(ISpecification<TEntity> spec) where TModel : class;
        ValueTask<TModel> FirstOrDefaultAsync<TModel>(Expression<Func<TEntity, bool>> criteria) where TModel : class;
    }
}
