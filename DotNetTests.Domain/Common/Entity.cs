using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Domain.Common
{
    public abstract class Entity
    {
        public DateTime CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public Guid UpdatedBy { get; set; }

        public void SetCreatedTime(Guid? userIdentity = default)
        {
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
            CreatedBy = userIdentity ?? default;
            UpdatedBy = userIdentity ?? default;
        }

        public void SetUpdatedTime(Guid? userIdentity = default)
        {
            UpdatedTime = DateTime.UtcNow;
            UpdatedBy = userIdentity ?? default;
        }
    }

    public abstract class Entity<TKey> : Entity where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
