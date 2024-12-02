using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;
using Divination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Divination.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<AppUser> _entities;
        public AppUserRepository(IdentityContext context) : base(context)
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
        public async Task<AppUser> GetEmailGoogle(string email)
        {
            var existingEntity = await _entities.FirstOrDefaultAsync(x => x.Email == email);
            if (existingEntity != null)
            {
                return existingEntity;
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

         public async Task<bool> IsThereGoogleIdAsync(string googleId)
        {
            var existingEntity = await _entities.FirstOrDefaultAsync(x => x.GoogleId == googleId);
            if (existingEntity != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}