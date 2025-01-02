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
    public class FalCategoryRepository:BaseRepository<FalCategory>,IFalCategoryRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<FalCategory> _entities;
         public FalCategoryRepository(IdentityContext context):base(context)
        {
            _context = context;
            _entities = _context.Set<FalCategory>();
        }

        public async Task<ICollection<FalCategory>> GetFalCategoriesAsync(List<int> falcategoryId)
        {
            var existingEntity = await _entities.Where(x => falcategoryId.Contains(x.Id)).ToListAsync();

            if (existingEntity != null && existingEntity.Any())
            {
                return existingEntity;
            }
            else
            {
                 throw new ArgumentException("FalCategories cannot be null or empty.");;
            }
        }
    
    }
}