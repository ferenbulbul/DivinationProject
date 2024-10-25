using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class Fortuneteller:AppUser
    {
         public string Experience { get; set; } 
         public ICollection<Applications> Applications { get; set; }
    }
}