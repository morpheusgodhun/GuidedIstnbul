using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AgentDtos
{
    public class AgentApprove
    {
        public Guid AgentId { get; set; }
        public int? Discount { get; set; }
    }
}
