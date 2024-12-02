using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Application.Models.DTOs
{
    public class FortuneTellerDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public DateTime DateofBirth { get; set; }
        public string Experience { get; set; }
        public int RequirementCredit { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        public float? Rating { get; set; }
        public int TotalCredit { get; set; }
    }
}