using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class ContactPostDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid FindUsID { get; set; }
        public int CountryID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
