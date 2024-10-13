using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Divination.Infrastructure.EntityTypeConfig
{
    public class AppUserConfig:IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(u => u.Gender)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(u => u.DateofBirth)
                         .IsRequired()
                         .HasMaxLength(10);

        }
    }
}