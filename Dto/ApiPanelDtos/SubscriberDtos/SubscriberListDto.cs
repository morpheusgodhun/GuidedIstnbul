using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SubscriberDtos
{
    public class SubscriberListDto
    {
        public string SubscriberID { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public DateTime MembershipDate { get; set; }
    }
}
