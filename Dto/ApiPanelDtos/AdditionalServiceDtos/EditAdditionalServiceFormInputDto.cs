using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class EditAdditionalServiceFormInputDto
    {
        public Guid FormInputID { get; set; }
        public string? AdditionalServiceName { get; set; }
        public string? AdditionalServiceId { get; set; }
        public string? PropertyName { get; set; }
        public int Type { get; set; }   // 7 type 
        public int Order { get; set; }
        public bool IsRequired { get; set; }
    }
}
