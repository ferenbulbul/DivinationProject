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
        private readonly IFortuneTellerRepository _fortuneManager;
        private readonly IClientRepository _clientService;




        public AppUserManager(UserManager<AppUser> userManager, IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, IOptions<JwtSettings> jwtSettings, IFortuneTellerRepository fortuneManager, IClientRepository clientService)
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _fortuneManager = fortuneManager;
            _clientService = clientService;
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
                CreatedDate = DateTime.Now,
                IsActive = true,
                Credit = 50,
                IsGoogleUser = false,
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
                DateofBirth=registerDto.DateOfBirth,
                LastName = registerDto.LastName,
                Gender = registerDto.Gender,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                Experience = registerDto.Experience,
                CreatedDate = DateTime.Now,
                IsActive = true,
                Rating = 5,
                RequirementCredit = 10,
                TotalVoted = 0,
                IsGoogleUser = false
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

                var fortuneTeller = user as Fortuneteller;  // 'as' ile dönüşüm yapıyoruz
                float? rating = fortuneTeller?.Rating;

                return new LoginResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                    Token = token,
                    EmailConfirmed = user.EmailConfirmed,
                    Rating = rating
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
             new Claim("UserId", user.Id.ToString())
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

        public async Task<FortuneTellerDto> GetFortuneTellerById(int fortuneTellerId)
        {
            var user = await _fortuneManager.GetByIdAsync(fortuneTellerId);
            var fortuneTeller = new FortuneTellerDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateofBirth = user.DateofBirth,
                Experience = user.Experience,
                RequirementCredit = user.RequirementCredit,
                Email = user.Email,
                Rating = user.Rating,
                TotalCredit = user.TotalCredit
            };

            return fortuneTeller;
        }

        public async Task<ClientDto> GetclientById(int clientId)
        {
            var user = await _clientService.GetByIdAsync(clientId);
            var client = new ClientDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateofBirth = user.DateofBirth,
                Email = user.Email,
                Occupation = user.Occupation,
                MaritalStatus = user.MaritalStatus
            };
            return client;
        }

        public async Task UpdateFortuneTeller(int id, FortuneTellerDto fortuneTellerDto)
        {
            var existingEntity = await _fortuneManager.GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new Exception("Fortuneteller not found.");
            }
            existingEntity.UserName = fortuneTellerDto.UserName;
            existingEntity.FirstName = fortuneTellerDto.FirstName;
            existingEntity.LastName = fortuneTellerDto.LastName;
            existingEntity.Gender = fortuneTellerDto.Gender;
            existingEntity.DateofBirth = fortuneTellerDto.DateofBirth;
            existingEntity.Experience = fortuneTellerDto.Experience;
            existingEntity.Email = fortuneTellerDto.Email;
            existingEntity.RequirementCredit=fortuneTellerDto.RequirementCredit;

            await _fortuneManager.UpdateAsync(existingEntity);
        }

        public async Task UpdateClient(int id, ClientDto clientDto)
        {
            var existingEntity = await _clientService.GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new Exception("Fortuneteller not found.");
            }
            existingEntity.UserName = clientDto.UserName;
            existingEntity.FirstName = clientDto.FirstName;
            existingEntity.LastName = clientDto.LastName;
            existingEntity.Gender = clientDto.Gender;
            existingEntity.DateofBirth = clientDto.DateofBirth;
            existingEntity.Email = clientDto.Email;
            existingEntity.Occupation = clientDto.Occupation;
            existingEntity.MaritalStatus = clientDto.MaritalStatus;

            await _clientService.UpdateAsync(existingEntity);
        }


        public async Task<AppUser> CreateUserFromGoogleAsync(string email, string firstName, string lastName, string googleId)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return existingUser;
            }
            var newUser = new Client
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                GoogleId = googleId,
                IsGoogleUser = true,
                EmailConfirmed = true,
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedDate=DateTime.Now,
                Credit=50,
                
            };

            var result = await _userManager.CreateAsync(newUser);
            await _userManager.AddToRoleAsync(newUser, "Client");

            if (result.Succeeded)
            {
                return newUser;
            }
            else
            {
                throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<LoginResponseDto> SignInGoogleUserAsync(string email)
        {

            var existingUser = await _appUserRepository.GetEmailGoogle(email);
            var roles = new List<string> { "client" };
            var token = await GenerateToken(existingUser);

            var user = new LoginResponseDto
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Roles = roles.ToList(),
                EmailConfirmed = existingUser.EmailConfirmed,
                Token = token,
                

            };
            return user;
        }

        public async Task<bool> IsThereGoogleId(string googleId)
        {
            var result = await _appUserRepository.IsThereGoogleIdAsync(googleId);
            return result;
        }

    }
}