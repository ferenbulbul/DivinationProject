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
    public class ApplicationRepository : BaseRepository<Applications>, IApplicationRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<Applications> _entities;
        public ApplicationRepository(IdentityContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Applications>();
        }

        public async Task<IEnumerable<Applications>> GetApplicationsIsAnswerFalseAsync(int id)
        {

            return await _entities
             .Include(a => a.Client)
             .Include(a=>a.Categories)
             .Where(a => a.FortunetellerId == id && a.IsActive == true && a.IsAnswer==false)
             .ToListAsync();
        }

        public async Task IsAnswerTrue(int id)
        {

            var entity = await _entities.FindAsync(id);

            if (entity != null)
            {
                entity.IsAnswer = true;
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