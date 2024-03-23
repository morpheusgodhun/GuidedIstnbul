using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomPageManagementDto
{
    public class CustomPageListDto
    {
        public Guid PageID { get; set; }
        public bool Status { get; set; }
        public string PageName { get; set; }
        public bool IsAllActive { get; set; }
    }
}
