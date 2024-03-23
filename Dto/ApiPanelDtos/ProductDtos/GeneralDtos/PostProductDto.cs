using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.GeneralDtos
{
    public class PostProductDto
    {
        public string ProductID { get; set; }
        public List<Guid> RoleTemplateIds { get; set; }
    }
}
