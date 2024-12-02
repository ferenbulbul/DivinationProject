using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

namespace Divination.Application.Manager
{
    public class ClientManager : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientManager(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<int> GetCredit(int clientId)
        {
            return _clientRepository.GetClientCredit(clientId);
        }

         public async Task EarnCredit(int clientId,int credit)
        {
             await _clientRepository.EarnCreditAsync(clientId,credit);
        }
    }
}