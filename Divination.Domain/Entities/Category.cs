using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class Category:IBaseEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

         public ICollection<Applications> Applications { get; set; }
    }
}