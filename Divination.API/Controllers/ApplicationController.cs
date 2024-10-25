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
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> AddApplication(ApplicationDto applicationDto)
        {
            if (applicationDto.Photo1 == null)
            {
                return BadRequest("Photo is required.");
            }

            try
            {
                await _service.AddAplication(applicationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AnswerApplication(int id, string answer)
        {
            try
            {
                await _service.AddAnswer(id, answer);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationByFortuneTeller(int id)
        {
           var application= await _service.GetApplications(id);
           return Ok(application);
        }
    }
}
