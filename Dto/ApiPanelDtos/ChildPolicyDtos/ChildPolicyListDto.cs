using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ChildPolicyDtos
{
    public class ChildPolicyListDto
    {
        public Guid ChildPolicyID { get; set; }
        public bool Status { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int Order { get; set; }
        public int PayPercent { get; set; }
    }
}
