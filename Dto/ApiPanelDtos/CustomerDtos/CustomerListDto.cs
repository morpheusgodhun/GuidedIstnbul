using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomerDtos
{
    public class CustomerListDto
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string MembershipStatus { get; set; }
    }
}
