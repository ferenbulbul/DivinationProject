using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Domain.Entities
{
    public class Applications : IBaseEntity
    {
        public int Id { get; set; }
        public byte[] ImageData1 { get; set; }
        public byte[] ImageData2 { get; set; }
        public byte[] ImageData3 { get; set; }

        public bool IsAnswer { get; set; }
        

        public int ClientId { get; set; }
        public Client Client { get; set; }


        public int FortunetellerId { get; set; }
        public Fortuneteller FortuneTeller { get; set; }


        public ICollection<Category> Categories { get; set; }

        public Answer Answer { get; set; }

        public int FalCategoryId { get; set; }
        public FalCategory FalCategory { get; set; }
        
        

        
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}