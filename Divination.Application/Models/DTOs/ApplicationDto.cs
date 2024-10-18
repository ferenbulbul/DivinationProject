using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Divination.Application.Models.DTOs
{
    public class ApplicationDto
    {
        public IFormFile Photo { get; set; }

    }
}