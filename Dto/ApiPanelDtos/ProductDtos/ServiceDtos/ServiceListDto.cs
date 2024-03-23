using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.ServiceDtos
{
    public class ServiceListDto
    {
        public Guid ProductID { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHomepage { get; set; }
        public string ProductName { get; set; }
        public List<string> AdditionalServices { get; set; }
    }
}
