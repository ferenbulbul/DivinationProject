using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;

namespace Divination.Application.Models.DTOs
{
    public class GetApplicationByClientIdDto
    {
        public int Id { get; set; }
        public string? Answer { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? FortunetellerFirstName { get; set; }
        public string? FortunetellerLastName { get; set; }
        public List<string> Categories { get; set; }
        public float? Score { get; set; }
    }
}