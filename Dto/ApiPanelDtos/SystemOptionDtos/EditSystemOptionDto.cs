using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SystemOptionDtos
{
    public class EditSystemOptionDto
    {
        public Guid SystemOptionID { get; set; }
        public int SystemOptionCategoryID { get; set; }
        public int Order { get; set; }
    }
}
