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
    public class FortuneTellerRepository : BaseRepository<Fortuneteller>,IFortuneTellerRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<Fortuneteller> _entities;
        public FortuneTellerRepository(IdentityContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Fortuneteller>();
        }

        
    }
}