using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class BookServicePostDto
    {
        public string ServiceID { get; set; }
        public List<BookServiceAdditionalServiceDto> AdditionalServices { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Hotel { get; set; }
        public string HowFindUsID { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class BookServiceAdditionalServiceDto
    {
        public string AdditionalServiceID { get; set; }
        public string AdditionalServiceOptionID { get; set; }
        public List<AdditionalServiceInputDto>? Inputs { get; set; }
    }
    public class AdditionalServiceInputDto
    {
        public string AdditionalServiceInputID { get; set; }
        public string InputValue { get; set; }

    }
}
