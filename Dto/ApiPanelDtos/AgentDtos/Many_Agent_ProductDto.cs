using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AgentDtos
{
    public class Many_Agent_ProductDto
    {
        public string AgentId { get; set; }
        public string ProductId { get; set; }
        public int DiscountRate { get; set; }
    }
}
