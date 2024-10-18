using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Divination.Infrastructure.EntityTypeConfig
{
    public class BaseEnitityConfig<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.IsActive)
               .HasDefaultValue(true);

        builder.Property(e => e.CreatedDate)
               .HasColumnType("datetime");

        builder.Property(e => e.UpdatedDate)
               .HasColumnType("datetime");

        builder.Property(e => e.DeletedDate)
               .HasColumnType("datetime");
    }
    }
}