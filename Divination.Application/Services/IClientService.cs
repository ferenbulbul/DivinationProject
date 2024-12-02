using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Domain.Entities;

namespace Divination.Application.Services
{
    public interface IClientService
    {
        Task<int> GetCredit(int clientId);
        Task EarnCredit(int clientId,int credit);
    }
}