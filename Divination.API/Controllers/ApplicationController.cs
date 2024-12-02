using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                await _service.AddAplication(applicationDto);

                var response = new ApiResponse<string>
                {
                    Success = true,
                    Message = "Fal başarıyla kaydedildi.",
                    Data = null
                };

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Anahtar bulunamadı: {ex.Message}",
                    Data = null
                };

                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                    Data = null
                };

                return StatusCode(500, response);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AnswerApplication(int id, string answer)
        {
            try
            {
                await _service.AddAnswer(id, answer);

                var response = new ApiResponse<string>
                {
                    Success = true,
                    Message = "Cevap başarıyla eklendi.",
                    Data = null
                };

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Başvuru bulunamadı: {ex.Message}",
                    Data = null
                };

                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin."+ex.Message,
                    Data = null
                };

                return StatusCode(500, response);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetApplicationByFortuneTeller(int id)
        {
            try
            {
                var applications = await _service.GetApplications(id);

                var response = new ApiResponse<IEnumerable<GetApplicationDto>>
                {
                    Success = true,
                    Message = "Başvurular başarıyla getirildi.",
                    Data = applications
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Başvurular getirilirken bir hata oluştu.}",
                    Data = null
                };

                return StatusCode(500, response);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetApplicationByFortuneTellerIsAnsweredTrue(int id)
        {
            try
            {
                var applications = await _service.GetApplicationAnsweredTrue(id);

                var response = new ApiResponse<IEnumerable<GetApplicationAnsweredTrueDto>>
                {
                    Success = true,
                    Message = "Yanıtlanmış başvurular başarıyla getirildi.",
                    Data = applications
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Başvurular getirilirken bir hata oluştu.",
                    Data = null
                };

                return StatusCode(500, response);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetApplicationByClientIdIsAnsweredFalse(int id)
        {
            try
            {
                var applications = await _service.GetApplicationsByClientIdIsAnsweredFalse(id);

                var response = new ApiResponse<IEnumerable<GetApplicationByClientIsAnsweredFalseDto>>
                {
                    Success = true,
                    Message = "Yanıtlanmamış başvurular başarıyla getirildi.",
                    Data = applications
                };

                return Ok(response);
            }
            catch (Exception)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Başvurular getirilirken bir hata oluştu.",
                    Data = null
                };

                return StatusCode(500, response);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetApplicationByClientIdIsAnsweredTrue(int id)
        {
            try
            {
                var applications = await _service.GetApplicationsByClientIdIsAnsweredTrue(id);

                var response = new ApiResponse<IEnumerable<GetApplicationByClientIdDto>>
                {
                    Success = true,
                    Message = "Yanıtlanmış başvurular başarıyla getirildi.",
                    Data = applications
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Başvurular getirilirken bir hata oluştu.",
                    Data = null
                };

                return StatusCode(500, response);
            }
        }
    }
}
