using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.GeneralDtos
{
    public class ProductBlogListDto
    {
        public Guid ProductBlogID { get; set; }
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }
}
