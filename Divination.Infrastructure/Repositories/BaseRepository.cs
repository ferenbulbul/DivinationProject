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
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly IdentityContext _context;
        private DbSet<T> _entities;


        public BaseRepository(IdentityContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.Where(e => e.IsActive == true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task IsActiveAsync(int id)
        {
            var entity = await _entities.FindAsync(id);

            if (entity != null)
            {
                entity.IsActive = false;
                entity.DeletedDate = DateTime.Now;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedDate=DateTime.Now;
            _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(entity).Property(x => x.DeletedDate).IsModified = false; 
            _context.Entry(entity).Property(x => x.IsActive).IsModified = false; 

            await _context.SaveChangesAsync();
        }
    }
}