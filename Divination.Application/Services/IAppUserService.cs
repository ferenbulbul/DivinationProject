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
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<FortuneTellerDto> GetFortuneTellerById(int fortuneTellerId);
        Task UpdateFortuneTeller(int id, FortuneTellerDto fortuneTellerDto);
        Task<ClientDto> GetclientById(int clientId);
        Task UpdateClient(int id, ClientDto clientDto);
        Task<AppUser> CreateUserFromGoogleAsync(string email, string firstName, string lastName, string googleId);
        Task<LoginResponseDto> SignInGoogleUserAsync(string email);
        Task<bool> IsThereGoogleId(string googleId);
    }
}