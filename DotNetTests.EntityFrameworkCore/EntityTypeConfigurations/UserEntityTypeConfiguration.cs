using DotNetTests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.EntityFrameworkCore.EntityTypeConfigurations
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //  builder.ToTable("Books");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired();

            builder.HasMany(e => e.Books).WithMany(e => e.Users).UsingEntity<UserBook>(
                "UserBooks",
                j => j.HasOne<Book>().WithMany().HasForeignKey(e => e.BookId),
                j => j.HasOne<User>().WithMany().HasForeignKey(e => e.UserId));
        }
    }
}
