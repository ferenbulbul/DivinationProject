using System;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divination.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly IFortuneTellerService _fortuneTellerService;
        private readonly IApplicationService _applicationService;
        private readonly IClientService _clientService;

        public ClientController(
            IFortuneTellerService fortuneTellerService, 
            IApplicationService applicationService, 
            IClientService clientService)
        {
            _fortuneTellerService = fortuneTellerService;
            _applicationService = applicationService;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFortuneTeller()
        {
            var list = await _fortuneTellerService.FortuneTellerList();

            var response = new ApiResponse<object>
            {
                Success = true,
                Message = "Fortune teller list retrieved successfully.",
                Data = list
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ScoreFortune(int applicationId, float score)
        {
            if (applicationId <= 0 || score < 0)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid application ID or score.",
                    Data = null
                };
                return BadRequest(response);
            }

            await _applicationService.ScoreFotrune(applicationId, score);

            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Score submitted successfully.",
                Data = null
            };
            return Ok(successResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetCredit(int clientId)
        {
            if (clientId <= 0)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid client ID.",
                    Data = null
                };
                return BadRequest(response);
            }

            var credit = await _clientService.GetCredit(clientId);

            var successResponse = new ApiResponse<int>
            {
                Success = true,
                Message = "Credit retrieved successfully.",
                Data = credit
            };
            return Ok(successResponse);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> EarnCredit(int credit)
        {
            if (credit <= 0)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Credit amount must be greater than zero.",
                    Data = null
                };
                return BadRequest(response);
            }

            var clientId = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(clientId))
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "User ID not found in the token.",
                    Data = null
                };
                return Unauthorized(response);
            }

            await _clientService.EarnCredit(int.Parse(clientId), credit);

            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Credit earned successfully.",
                Data = null
            };
            return Ok(successResponse);
        }
    }
}
