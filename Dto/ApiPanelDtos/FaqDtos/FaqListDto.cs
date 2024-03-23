using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqManagementDtos
{
    public class FaqListDto
    {
        public Guid FaqID { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
        public string FaqCategory { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
