using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CancellationPolicyDtos
{
    public class CancellationPolicyListDto
    {
        public Guid CancellationPolicyID { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public int UnCancellableHour { get; set; }
    }
}
