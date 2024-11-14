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
    }
}