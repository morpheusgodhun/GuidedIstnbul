using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqCategoryDtos
{
    public class FaqCategoryListDto
    {
        public Guid CategoryID { get; set; }
        public bool Status { get; set; }
        public string CategoryName { get; set; }
        public int Order { get; set; }
    }
}
