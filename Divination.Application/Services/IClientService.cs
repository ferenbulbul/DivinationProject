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
        Task AddAplication(ApplicationDto applicationDto);
        Task<string> GetPhotoAsync(int id);
    }
}