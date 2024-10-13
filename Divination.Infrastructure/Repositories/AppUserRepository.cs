using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Divination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Divination.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>,IAppUserRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<AppUser> _entities;
        public AppUserRepository(IdentityContext context):base(context)
        {
            _context = context;
            _entities = _context.Set<AppUser>();
        }
        public async Task<bool> Register(AppUser appUser)
        {
            try
            {
                await _entities.AddAsync(appUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
                return false;
            }
        }
    }
}