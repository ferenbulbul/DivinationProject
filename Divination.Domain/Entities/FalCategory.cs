using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class FalCategory:IBaseEntity
    {
        public int Id { get; set; }
        public string FortuneCategory { get; set; }
        public ICollection<Fortuneteller> Fortunetellers { get; set; }
        public ICollection<Applications> Applications { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}