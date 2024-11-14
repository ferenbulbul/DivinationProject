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
    public class FortuneTellerRepository : BaseRepository<Fortuneteller>, IFortuneTellerRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<Fortuneteller> _entities;
        public FortuneTellerRepository(IdentityContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Fortuneteller>();
        }

        public async Task UpdateRating(int id, float rating)
        {

            var entity = await _entities.FindAsync(id);

            if (entity != null)
            {
                entity.Rating = rating;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

    }
}