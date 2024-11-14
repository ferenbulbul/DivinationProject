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
             .Include(a => a.Categories)
             .Where(a => a.FortunetellerId == id && a.IsActive == true && a.IsAnswer == false)
             .ToListAsync();
        }

        public async Task<IEnumerable<Applications>> GetApplicationsIsAnswerTrueAsync(int id)
        {

            return await _entities
             .Include(a => a.Client)
             .Include(a => a.Categories)
             .Include(a => a.Answer)
             .Where(a => a.FortunetellerId == id && a.IsActive == true && a.IsAnswer == true)
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

        public async Task<IEnumerable<Applications>> GetApplicationByClientIdIsAnsweredTrueAsync(int id)
        {
            return await _entities.Include(a => a.Answer)
                       .Include(a => a.Client)
                       .Include(a => a.FortuneTeller)
                       .Include(a => a.Categories)
                       .Where(a => a.ClientId == id && a.IsAnswer == true && a.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<Applications>> GetApplicationByClientIdIsAnsweredFalseAsync(int id)
        {
            return await _entities.Include(a => a.Answer)
                       .Include(a => a.Client)
                       .Include(a => a.FortuneTeller)
                       .Include(a => a.Categories)
                       .Where(a => a.ClientId == id && a.IsAnswer == false && a.IsActive == true).ToListAsync();
        }

        public async Task<int> GetFortuneTellerIdByApplicationId(int ApplicationId)
        {
            return await _entities.Where(a => a.Id == ApplicationId).Select(a => a.FortunetellerId).FirstOrDefaultAsync();
        }
    }
}