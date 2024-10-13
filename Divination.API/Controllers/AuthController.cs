using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Divination.Application;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
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

        public AuthController(IAppUserService userService, UserManager<AppUser> userManager ,IEmailService emailService)
        {
            _userService = userService;
            _userManager = userManager;
            _emailService=emailService;
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
                return BadRequest(ModelState);
            }
            var result = await _userService.RegisterClientAsync(registerDto);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok("Client registered successfully");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFortuneTeller(RegisterFortuneTellerDto registerDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RegisterFortuneTellertAsync(registerDto);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (token == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found.");
            }

            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully!");
            }

            return BadRequest("Error confirming your email.");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var loginResponseDto = await _userService.SignInAsync(loginDto);
                var user = _userManager.FindByIdAsync(loginResponseDto.Id.ToString());
                return Ok(loginResponseDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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
                new { userId = user.Id, token = token },  // URL parametreleri
                protocol: HttpContext.Request.Scheme);  // Protokolü ayarla (HTTP/HTTPS)

            await _emailService.SendEmailAsync(user.Email,"Email doğrulama Kodu",callbackUrl);
            return Ok(callbackUrl) ;
        }

      


    }
}