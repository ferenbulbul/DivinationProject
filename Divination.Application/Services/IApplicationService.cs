using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Domain.Entities;

namespace Divination.Application.Services
{
    public interface IApplicationService
    {
        Task AddAplication(ApplicationDto applicationDto);
        Task AddAnswer(int id, string answer);
        Task<IEnumerable<GetApplicationDto>> GetApplications(int fortuneTellerId);
    }
}