using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class Client : AppUser
    {
        public string? Occupation { get; set; } = string.Empty;
        public string? MaritalStatus { get; set; } = string.Empty;
        public ICollection<Applications>? Applications { get; set; }
        public int Credit { get; set; }
    }
}