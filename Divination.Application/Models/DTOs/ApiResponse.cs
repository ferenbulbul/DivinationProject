using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Application.Models.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } 
        public string Message { get; set; }
        public T Data { get; set; } 
    }
}