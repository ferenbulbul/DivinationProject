using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

namespace Divination.Application.Manager
{
    public class ClientManager : IClientService
    {
        private readonly IApplicationRepository _service;
        public ClientManager(IApplicationRepository service)
        {
            _service = service;
        }
        public async Task AddAplication(ApplicationDto applicationDto)
        {

            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await applicationDto.Photo.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(applicationDto.Photo.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(applicationDto.Photo.FileName)}";
            var application = new Applications
            {
                FileName = fileName,
                ImageData = imageData,
            };

            await _service.AddAsync(application);
        }

        public async Task<string> GetPhotoAsync(int id)
        {
            var application = await _service.GetByIdAsync(id);

            if (application == null || application.ImageData == null)
            {
                throw new Exception("Photo not found.");
            }

            // Binary veriyi Base64 string formatına dönüştür
            var base64String = Convert.ToBase64String(application.ImageData);

            return base64String;
        }

    }
}