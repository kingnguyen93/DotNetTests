using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class MapperGenericRepository<TEntity, TKey> : GenericRepository<TEntity, TKey>, IMapperGenericRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IMapper _mapper;

        public MapperGenericRepository(AppContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async ValueTask<IReadOnlyList<TModel>> ListAllAsync<TModel>() where TModel : class => await DbSet.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();
        public virtual async ValueTask<IReadOnlyList<TModel>> ListAsync<TModel>(ISpecification<TEntity> spec) where TModel : class => await ApplySpecification(spec).ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();
        public virtual async ValueTask<IReadOnlyList<TModel>> ListAsync<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class => await DbSet.Where(predicate).ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();

        public virtual async ValueTask<TModel> GetByIdAsync<TModel>(TKey id) where TModel : class => _mapper.Map<TModel>(await DbSet.FindAsync(id));

        public virtual async ValueTask<TModel> FirstOrDefaultAsync<TModel>(ISpecification<TEntity> spec) where TModel : class => await ApplySpecification(spec).ProjectTo<TModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        public virtual async ValueTask<TModel> FirstOrDefaultAsync<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class => await DbSet.Where(predicate).ProjectTo<TModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }
}
