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
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<Client> _entities;
        public ClientRepository(IdentityContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Client>();
        }

        public async Task<int> GetClientCredit(int clientId)
        {
            var client = await _entities.FindAsync(clientId);
            if (client != null)
            {
                return client.Credit;
            }
            else
            {
                throw new Exception("entity is not found");
            }
        }

        public async Task UpdateCredit(int clientId, int credit)
        {

            var entity = await _entities.FindAsync(clientId);

            if (entity != null)
            {
                entity.Credit = credit;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public async Task<int> GetCreditAsync(int clientId)
        {
            var entity = await _entities.FindAsync(clientId);
            if (entity != null)
            {
                return entity.Credit;
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public async Task EarnCreditAsync(int clientId, int Credit)
        {
            var entity = await _entities.FindAsync(clientId);
            if (entity != null)
            {
                entity.Credit += Credit;
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