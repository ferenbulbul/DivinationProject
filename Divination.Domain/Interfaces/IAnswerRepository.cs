using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;

namespace Divination.Domain.Interfaces
{
    public interface IAnswerRepository:IBaseRepository<Answer>
    {   
       Task ScoreFortuneAsync(int applicationId,float score);
       Task<float> GetAverageScoreForFortuneTellerAsync(int fortuneTellerId);
       Task<int> GetTotalVoted(int fortuneTellerId);
    }
}