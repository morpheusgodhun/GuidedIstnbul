using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqCategoryDtos
{
    public class AddFaqCategoryDto
    {
        public string CategoryName { get; set; }
        public int Order { get; set; }
        public string PageId { get; set; } //only custom pages
    }
}
