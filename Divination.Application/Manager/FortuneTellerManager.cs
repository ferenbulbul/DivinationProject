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
    public class FortuneTellerManager : IFortuneTellerService
    {
        private readonly IFortuneTellerRepository _fortuneTeller;

        public FortuneTellerManager(IFortuneTellerRepository fortuneTellerRepository)
        {
            _fortuneTeller = fortuneTellerRepository;
        }
        public async Task<IEnumerable<FortuneTellerListDto>> FortuneTellerList()
        {
            var list = await _fortuneTeller.GetAllAsync();
            var dtoList = list
            .Where(f=>f.IsActive==true)
            .Select(fortuneTeller => new FortuneTellerListDto
            {
                Id=fortuneTeller.Id,
                FirstName = fortuneTeller.FirstName,
                LastName = fortuneTeller.LastName
            }).ToList();
            return dtoList;
        }

    }
}