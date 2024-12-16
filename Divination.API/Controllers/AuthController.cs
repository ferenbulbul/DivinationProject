using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Divination.Application;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Divination.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;


        public AuthController(IAppUserService userService, UserManager<AppUser> userManager, IEmailService emailService, IAppUserService appUserService)
        {
            _userService = userService;
            _userManager = userManager;
            _emailService = emailService;
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "fortuneteller")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }


        [HttpPost]
        public async Task<IActionResult> RegisterClient(RegisterClientDto registerDto)
        {

            if (!ModelState.IsValid)
            {
                var validationErrors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Geçersiz model verisi: {validationErrors}",
                    Data = null
                };
                return BadRequest(response);
            }


            var result = await _userService.RegisterClientAsync(registerDto);


            if (!result.Succeeded)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Kayıt işlemi başarısız: {string.Join(", ", result.Errors.Select(e => e.Description))}",
                    Data = null
                };
                return BadRequest(response);
            }


            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Kullanıcı başarıyla kaydedildi.",
                Data = null
            };
            return Ok(successResponse);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterFortuneTeller(RegisterFortuneTellerDto registerDto)
        {

            if (!ModelState.IsValid)
            {
                var validationErrors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Geçersiz model verisi: {validationErrors}",
                    Data = null
                };
                return BadRequest(response);
            }


            var result = await _userService.RegisterFortuneTellertAsync(registerDto);


            if (!result.Succeeded)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Kayıt işlemi başarısız: {string.Join(", ", result.Errors.Select(e => e.Description))}",
                    Data = null
                };
                return BadRequest(response);
            }


            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Falcı başarıyla kaydedildi.",
                Data = null
            };
            return Ok(successResponse);
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz e-posta doğrulama isteği: Token eksik.",
                    Data = null
                };
                return BadRequest(response);
            }


            if (string.IsNullOrEmpty(userId))
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz e-posta doğrulama isteği: Kullanıcı bulunamadı.",
                    Data = null
                };
                return BadRequest(response);
            }


            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (!result.Succeeded)
            {
                var errorDescriptions = result.Errors
                    .Select(error => error.Description)
                    .ToList();

                var response = new ApiResponse<List<string>>
                {
                    Success = false,
                    Message = "E-posta doğrulama başarısız.",
                    Data = errorDescriptions
                };
                return BadRequest(response);
            }


            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "E-posta başarıyla doğrulandı.",
                Data = null
            };
            return Ok(successResponse);
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var loginResponseDto = await _userService.SignInAsync(loginDto);

                if (loginResponseDto == null)
                {
                    var response = new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Giriş başarısız. Kullanıcı adı veya şifre hatalı.",
                        Data = null
                    };
                    return Unauthorized(response);
                }
                var responseSuccess = new ApiResponse<LoginResponseDto>
                {
                    Success = true,
                    Message = "Giriş başarılı.",
                    Data = loginResponseDto
                };
                return Ok(responseSuccess);
            }
            catch (UnauthorizedAccessException ex)
            {
                var responseError = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Yetkilendirme hatası: {ex.Message}",
                    Data = null
                };
                return Unauthorized(responseError);
            }
            catch (Exception ex)
            {
                var responseError = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Bir hata oluştu: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, responseError);
            }
        }



        [HttpPost]
        public async Task<IActionResult> SendEmailConfirmationAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",  // Onaylama için kullanılan action
                "Auth",  // Controller adı
                new { userId = user.Id, token },  // URL parametreleri
                protocol: HttpContext.Request.Scheme);  // Protokolü ayarla (HTTP/HTTPS)

            await _emailService.SendEmailAsync(user.Email, "Email doğrulama Kodu", callbackUrl);
            return Ok(callbackUrl);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz model verisi.",
                    Data = null
                };
                return BadRequest(response);
            }

            var userId = User.FindFirst("UserId")?.Value; // Kullanıcının ID'sini al
            var user = await _userManager.FindByIdAsync(userId);


            if (user == null)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı.",
                    Data = null
                };
                return NotFound(response);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var responseSuccess = new ApiResponse<string>
                {
                    Success = true,
                    Message = "Şifre başarıyla değiştirildi.",
                    Data = null
                };
                return Ok(responseSuccess);
            }

            // Hataları döndür
            var responseError = new ApiResponse<IEnumerable<IdentityError>>
            {
                Success = false,
                Message = "Şifre değişikliği sırasında bir hata oluştu.",
                Data = result.Errors
            };
            return BadRequest(responseError);
        }



        [HttpGet]
        public async Task<IActionResult> GetFortuneTellerById(int fortuneTellerId)
        {
            if (!ModelState.IsValid)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz model verisi.",
                    Data = null
                };
                return BadRequest(response);
            }

            var fortuneTeller = await _userService.GetFortuneTellerById(fortuneTellerId);

            if (fortuneTeller != null)
            {
                var responseSuccess = new ApiResponse<FortuneTellerDto>
                {
                    Success = true,
                    Message = "Fortune Teller başarıyla bulundu.",
                    Data = fortuneTeller
                };
                return Ok(responseSuccess);
            }

            var responseError = new ApiResponse<string>
            {
                Success = false,
                Message = "Fortune Teller bulunamadı.",
                Data = null
            };
            return NotFound(responseError);
        }


        [HttpGet]
        public async Task<IActionResult> GetClientById(int clientId)
        {
            if (!ModelState.IsValid)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz model verisi.",
                    Data = null
                };
                return BadRequest(response);
            }

            var client = await _userService.GetclientById(clientId);

            if (client != null)
            {
                var responseSuccess = new ApiResponse<ClientDto>
                {
                    Success = true,
                    Message = "Client başarıyla bulundu.",
                    Data = client
                };
                return Ok(responseSuccess);
            }

            var responseError = new ApiResponse<string>
            {
                Success = false,
                Message = "Client bulunamadı.",
                Data = null
            };
            return NotFound(responseError);
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> UpdateFortune(FortuneTellerDto fortuneTeller)
        {
            if (!ModelState.IsValid)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz model verisi.",
                    Data = null
                };
                return BadRequest(response);
            }

            var fortuneTellerId = User.FindFirst("UserId")?.Value;
            if (fortuneTellerId == null)
            {
                var responseError = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçerli kullanıcı bulunamadı.",
                    Data = null
                };
                return BadRequest(responseError);
            }
            await _userService.UpdateFortuneTeller(int.Parse(fortuneTellerId), fortuneTeller);

            var responseSuccess = new ApiResponse<string>
            {
                Success = true,
                Message = "Fortune teller başarıyla güncellendi.",
                Data = null
            };
            return Ok(responseSuccess);
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> UpdateClient(ClientDto client)
        {
            if (!ModelState.IsValid)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçersiz model verisi.",
                    Data = null
                };
                return BadRequest(response);
            }

            var clientId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(clientId))
            {
                var responseError = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Geçerli kullanıcı bulunamadı.",
                    Data = null
                };
                return BadRequest(responseError);
            }
            await _userService.UpdateClient(int.Parse(clientId), client);

            var responseSuccess = new ApiResponse<string>
            {
                Success = true,
                Message = "Müşteri başarıyla güncellendi.",
                Data = null
            };

            return Ok(responseSuccess);
        }

        [HttpGet]
        public IActionResult LoginGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Auth", null, Request.Scheme);
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

                if (!authenticateResult.Succeeded)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Google authentication failed."
                    });
                }

                var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;

                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var firstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                var lastName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                var googleId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(googleId))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "GoogleId is required."
                    });
                }

                if (!await _userService.IsThereGoogleId(googleId))
                {
                    await _userService.CreateUserFromGoogleAsync(email, firstName, lastName, googleId);
                }

                var loginResponse = await _userService.SignInGoogleUserAsync(email);

                return Ok(new ApiResponse<LoginResponseDto>
                {
                    Success = true,
                    Message = "Login successful.",
                    Data = loginResponse
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SigninGoogleFlutter(LoginGoogleDto loginDto)
        {
            if (!await _userService.IsThereGoogleId(loginDto.GoogleId))
            {
                await _userService.CreateUserFromGoogleAsync(loginDto.Email, loginDto.FirstName, loginDto.LastName, loginDto.GoogleId);
            }
            var loginResponse = await _userService.SignInGoogleUserAsync(loginDto.Email);
            return Ok(new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful.",
                Data = loginResponse
            });
        }


    }
}