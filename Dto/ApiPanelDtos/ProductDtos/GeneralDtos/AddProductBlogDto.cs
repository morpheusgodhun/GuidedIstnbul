using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.GeneralDtos
{
    public class AddProductBlogDto
    {
        public Guid ProductID { get; set; }
        public Guid BlogID { get; set; }
    }
}
