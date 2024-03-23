using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.GeneralDtos
{
    public class ProductAdditionalServiceListDto
    {
        public Guid ProductAdditionalServiceID { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public List<string> Options { get; set; }
        public Guid AdditionalServiceID { get; set; }
        public List<Guid> OptionIDs { get; set; }
        public bool IsRequired { get; set; }
        public bool IsComissionValid { get; set; }
        public bool IsMultiSelect { get; set; }
        public int Order { get; set; }

    }
}
