using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Application.Models.DTOs
{
    public class FortuneTellerListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float? Rating { get; set; }
        public int RequirementCredit { get; set; }
        public int TotalVoted { get; set; }
    }
}