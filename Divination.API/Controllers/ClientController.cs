using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Divination.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly IFortuneTellerService _fortuneTellerService;
        public ClientController(IFortuneTellerService fortuneTellerService)
        {
            _fortuneTellerService = fortuneTellerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFortuneTeller()
        {
            var list= await _fortuneTellerService.FortuneTellerList();
            return Ok(list);
        }

        
    }
}