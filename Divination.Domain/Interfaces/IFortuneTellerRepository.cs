using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;

namespace Divination.Domain.Interfaces
{
    public interface IFortuneTellerRepository:IBaseRepository<Fortuneteller>
    {
        Task UpdateRating(int id,float rating);
        Task<int> GetFortuneRequirmentCredit(int fortuneTellerId);
        Task UpdateCredit(int credit,int fortuneTellerId);
        Task UpdateTotalVoted(int fortuneTellerId,int totalVoted);
    }
}