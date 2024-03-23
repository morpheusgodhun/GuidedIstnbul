using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogCategoryDtos
{
    public class BlogCategoryListDto
    {
        public Guid BlogCategoryID { get; set; }
        public string BlogCategoryName { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
