using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Divination.Infrastructure.EntityTypeConfig
{
    public class FalCategoryConfig : BaseEnitityConfig<FalCategory>
    {
        public void Configure(EntityTypeBuilder<FalCategory> builder)
        {


            builder
              .HasMany(s => s.Fortunetellers)
              .WithMany(c => c.FalCategories)
              .UsingEntity<Dictionary<string, object>>(
                  "FortuneTellerFalCategoiry",
                  j => j.HasOne<Fortuneteller>().WithMany().HasForeignKey("FortunetellerId"),
                  j => j.HasOne<FalCategory>().WithMany().HasForeignKey("FalCategoryId")
              );

              


        }
    }
}

