using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class Answer : IBaseEntity
    {
        public int Id { get; set; }
        public string Answers { get; set; }
        public bool IsActive { get; set; }
        
        public float? Score { get; set; }
        public int ApplicationsId { get; set; }
        public Applications Applications { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}