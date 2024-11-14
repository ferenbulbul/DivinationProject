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
        private readonly IApplicationService _applicationService;
        
        public ClientController(IFortuneTellerService fortuneTellerService,IApplicationService applicationService)
        {
            _fortuneTellerService = fortuneTellerService;
            _applicationService=applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFortuneTeller()
        {
            var list= await _fortuneTellerService.FortuneTellerList();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> ScoreFortune(int ApplicationId,float score)
        {
            await _applicationService.ScoreFotrune(ApplicationId,score);
            return Ok();
        }


        
    }
}