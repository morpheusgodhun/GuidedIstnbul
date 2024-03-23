using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.AddProductDtos
{
    public class AddAdditionalServiceToProductDto
    {
        public Guid AdditionalServiceID { get; set; }
        public List<Guid>? OptionIDs { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        public bool IsComissionValid { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}
