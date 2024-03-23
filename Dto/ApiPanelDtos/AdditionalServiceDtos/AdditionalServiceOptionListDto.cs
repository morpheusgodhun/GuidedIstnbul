using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class AdditionalServiceOptionListDto
    {
        public Guid OptionID { get; set; }
        public bool Status { get; set; }
        public string OptionName { get; set; }
        public int Order { get; set; }
        public List<AdditionalServiceOptionListFormInputDto> FormInputs { get; set; }
    }

    public class AdditionalServiceOptionListFormInputDto
    {
        public string PropertyName { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
    }
}
