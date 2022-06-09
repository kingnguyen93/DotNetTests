using DotNetTests.Domain.Common;
using DotNetTests.Infrastructure.Repositories;
using DotNetTests.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.EntityFrameworkCore.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Repository setting

        protected readonly AppContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public IUnitOfWork UnitOfWork => _context;

        public GenericRepository(AppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _context.Set<TEntity>();
        }

        #endregion

        public virtual async ValueTask<IReadOnlyList<TEntity>> ListAllAsync() => await DbSet.ToListAsync();
        public virtual async ValueTask<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec) => await ApplySpecification(spec).ToListAsync();
        public virtual async ValueTask<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.Where(predicate).ToListAsync();

        public virtual ValueTask<TEntity> GetByIdAsync(TKey id) => DbSet.FindAsync(id);

        public virtual async ValueTask<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();
        public virtual async ValueTask<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.FirstOrDefaultAsync(predicate);

        public virtual async ValueTask<int> CountAsync() => await DbSet.CountAsync();
        public virtual async ValueTask<int> CountAsync(ISpecification<TEntity> spec) => await ApplySpecification(spec).CountAsync();
        public virtual async ValueTask<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.CountAsync(predicate);

        public virtual async ValueTask<bool> AnyAsync(ISpecification<TEntity> spec) => await ApplySpecification(spec).AnyAsync();
        public virtual async ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.AnyAsync(predicate);

        public virtual async ValueTask<bool> AllAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, bool>> predicate) => await DbSet.Where(condition).AllAsync(predicate);

        public virtual async ValueTask<TEntity> AddAsync(TEntity entity) => (await DbSet.AddAsync(entity)).Entity;

        public virtual ValueTask<TEntity> UpdateAsync(TEntity entity) => ValueTask.FromResult(DbSet.Update(entity).Entity);

        public virtual Task DeleteAsync(TEntity entity) => Task.FromResult(DbSet.Remove(entity));
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec) => SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet, spec);
    }
}
