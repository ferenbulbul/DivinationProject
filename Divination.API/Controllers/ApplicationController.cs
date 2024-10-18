using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Divination.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApplicationController : Controller
    {
        private readonly IClientService _clientservice;

        public ApplicationController(IClientService clientService)
        {
            _clientservice = clientService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(ApplicationDto applicationDto)
        {
            if (applicationDto.Photo == null)
            {
                return BadRequest("Photo is required.");
            }

            try
            {
                await _clientservice.AddAplication(applicationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpGet]
        public async Task<IActionResult> GetPhoto(int id)
        {
            try
            {
                // Base64 string formatında resmi geri döndür
                var base64Photo = await _clientservice.GetPhotoAsync(id);

                // Resmi JSON formatında geri döndür
                return Ok(new { Photo = base64Photo });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
