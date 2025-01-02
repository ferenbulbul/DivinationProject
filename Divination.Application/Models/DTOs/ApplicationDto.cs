using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Divination.Application.Models.DTOs
{
    public class ApplicationDto
    {
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public int FalCategoryId { get; set; }
        public List<int> CategoryIds { get; set; }
        public int ClientId { get; set; }
        public int FortunetellerId { get; set; }

    }
}