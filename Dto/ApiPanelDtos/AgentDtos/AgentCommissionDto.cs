using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AgentDtos
{
    public class AgentCommissionDto
    {
        public int ServicesDiscountRate { get; set; }
        public int DefaultTourDiscount { get; set; }
        public bool WithoutPay { get; set; }
    }
}
