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
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        protected new readonly IdentityContext _context;
        DbSet<Answer> _entities;
        public AnswerRepository(IdentityContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Answer>();
        }

        public async Task ScoreFortuneAsync(int applicationId, float score)
        {
            var entity = await _entities
                    .Where(a => a.ApplicationsId == applicationId)
                    .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Score = score;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public async Task<float> GetAverageScoreForFortuneTellerAsync(int fortuneTellerId)
        {
            var averageScore = await _context.Answers
                                              .Where(a => a.Applications.FortunetellerId == fortuneTellerId)
                                              .AverageAsync(a =>a.Score) ?? 0; 

            return averageScore;
        }

    }
}