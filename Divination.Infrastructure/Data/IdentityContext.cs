using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Divination.Infrastructure.EntityTypeConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Divination.Infrastructure.Data
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, int>
    {
        // public IdentityContext() { }
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Fortuneteller> Fortunetellers { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Answer> Answers { get; set; }


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=abc12345;database=divinationdb", new MySqlServerVersion(new Version(9, 0, 0)));
        // }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Fortuneteller>().ToTable("FortuneTellers");
            builder.Entity<Applications>().ToTable("Applications");
            builder.Entity<Answer>().ToTable("Answers");

            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new ApplicationsConfig());


            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                  new Category { Id = 1, CategoryName = "Aşk", IsActive = true, CreatedDate = DateTime.Now },
                  new Category { Id = 2, CategoryName = "Kariyer", IsActive = true, CreatedDate = DateTime.Now },
                  new Category { Id = 3, CategoryName = "Aile", IsActive = true, CreatedDate = DateTime.Now },
                  new Category { Id = 4, CategoryName = "Para", IsActive = true, CreatedDate = DateTime.Now },
                  new Category { Id = 5, CategoryName = "Akrabağlık İlişkileri", IsActive = true, CreatedDate = DateTime.Now });


            builder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "admin", NormalizedName = "ADMIN".ToUpper() },
                new AppRole { Id = 2, Name = "fortuneteller", NormalizedName = "FORTUNETELLER".ToUpper() },
                new AppRole { Id = 3, Name = "client", NormalizedName = "CLIENT".ToUpper() });

        }
    }
}