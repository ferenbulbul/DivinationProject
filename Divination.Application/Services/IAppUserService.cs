using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Divination.Application.Services
{
    public interface IAppUserService
    {
        Task<IdentityResult> RegisterClientAsync(RegisterClientDto registerDto);
        Task<IdentityResult> RegisterFortuneTellertAsync(RegisterFortuneTellerDto registerDto);
        Task<LoginResponseDto> SignInAsync(LoginDto loginDto);
        Task<string> GenerateToken(AppUser user);
        Task<IdentityResult> ConfirmEmailAsync(string userId,string token);
    }
}