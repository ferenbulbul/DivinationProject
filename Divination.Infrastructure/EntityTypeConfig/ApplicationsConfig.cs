using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Divination.Infrastructure.EntityTypeConfig
{
    public class ApplicationsConfig : BaseEnitityConfig<Applications>
    {
        public void Configure(EntityTypeBuilder<Applications> builder)
        {

            builder.HasOne(a => a.Client)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.ClientId);

            builder.HasOne(a => a.FortuneTeller)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.FortunetellerId);

            builder.HasOne(a => a.Answer)
            .WithOne(u => u.Applications)  
            .HasForeignKey<Answer>(u => u.ApplicationsId);

            builder.HasMany(a => a.Categories)
         .WithMany(c => c.Applications)
         .UsingEntity<Dictionary<string, object>>(
             "ApplicationCategory",  // Ara tablo adı
             j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
             j => j.HasOne<Applications>().WithMany().HasForeignKey("ApplicationsId")
         );
        }
    }
}