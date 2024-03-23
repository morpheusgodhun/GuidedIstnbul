using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SystemOptionDtos
{
    public class SystemOptionListDto
    {
        public Guid SystemOptionID { get; set; }
        public string SystemOptionCategoryName { get; set; }
        public string SystemOptionName { get; set; }
        public int Order { get; set; }
        public bool Status { get; set; }
    }
}
