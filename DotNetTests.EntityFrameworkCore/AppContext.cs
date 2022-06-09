using DotNetTests.Domain.Common;
using DotNetTests.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.EntityFrameworkCore
{
    public class AppContext : DbContext, IUnitOfWork
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedTime = DateTime.UtcNow;
                }
                entry.Entity.UpdatedTime = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync(Guid userIdentity, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.SetCreatedTime(userIdentity);
                }
                entry.Entity.SetUpdatedTime(userIdentity);
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
