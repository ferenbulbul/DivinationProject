using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divination.Application.Models.DTOs
{
    public class GetApplicationDto
    {

        public string ImageData1 { get; set; }
        public string ImageData2 { get; set; }
        public string ImageData3 { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public List<string> Categories { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? BirthDate { get; set; }
        
        
    }

}
