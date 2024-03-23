using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SystemOptionDtos
{
    public class AddSystemOptionDto
    {
        public int SystemOptionCategoryID { get; set; }
        public string SystemOptionName { get; set; }
        public int Order { get; set; }
    }
}
