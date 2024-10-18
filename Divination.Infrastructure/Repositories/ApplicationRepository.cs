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
    public class ApplicationRepository:BaseRepository<Applications>,IApplicationRepository
    {
        protected new readonly IdentityContext _context;
        private DbSet<Applications> _entities;
        public ApplicationRepository(IdentityContext context):base(context)
        {
            _context = context;
            _entities = _context.Set<Applications>();
        }

        
       
    }
}