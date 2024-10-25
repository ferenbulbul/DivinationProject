using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Divination.Application.Manager
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtSettings _jwtSettings;



        public AppUserManager(UserManager<AppUser> userManager, IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<IdentityResult> RegisterClientAsync(RegisterClientDto registerDto)
        {
            var user = new Client
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Gender = registerDto.Gender,
                DateofBirth = registerDto.DateofBirth,
                MaritalStatus = registerDto.MaritalStatus,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                Occupation = registerDto.Occupation,
                CreatedDate=DateTime.Now,
                IsActive=true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");
            }
            return result;
        }

        public async Task<IdentityResult> RegisterFortuneTellertAsync(RegisterFortuneTellerDto registerDto)
        {
            var user = new Fortuneteller
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Gender = registerDto.Gender,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                Experience = registerDto.Experience,
                CreatedDate=DateTime.Now,
                IsActive=true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Fortuneteller");
            }


            return result;
        }

        public async Task<LoginResponseDto> SignInAsync(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);
                var token = await GenerateToken(user);
                var roles = await _userManager.GetRolesAsync(user);

                return new LoginResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                    Token = token,
                    EmailConfirmed=user.EmailConfirmed
                };
            }

            throw new UnauthorizedAccessException("Invalid login attempt.");
        }


        public async Task<string> GenerateToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }




    }
}