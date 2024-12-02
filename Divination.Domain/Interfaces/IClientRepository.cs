using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;

namespace Divination.Domain.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
       Task<int> GetClientCredit(int clientId);
       Task UpdateCredit(int credit,int clientId);
       Task<int> GetCreditAsync(int clientId);
       Task EarnCreditAsync(int clientId, int Credit);
    }
}