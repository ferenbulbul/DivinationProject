using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Application.Models.DTOs
{
    public class ClientDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}