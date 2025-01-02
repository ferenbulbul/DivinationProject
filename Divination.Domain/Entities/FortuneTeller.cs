using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class Fortuneteller:AppUser
    {
         public string Experience { get; set; } 
         public float? Rating { get; set; }
         public int RequirementCredit { get; set; }
         public int TotalCredit { get; set; }
         public int TotalVoted { get; set; }
         public ICollection<Applications> Applications { get; set; }
         public ICollection<FalCategory> FalCategories { get; set; }
    }
}