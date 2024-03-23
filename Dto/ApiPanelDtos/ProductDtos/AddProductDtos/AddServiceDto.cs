using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.AddProductDtos
{
    public class AddServiceDto
    {
        public Guid ProductID { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public List<AddAdditionalServiceToProductDto> AdditionalServices { get; set; }
    }
}
