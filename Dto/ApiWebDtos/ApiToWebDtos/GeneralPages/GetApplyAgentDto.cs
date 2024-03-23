
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.GeneralPages
{
    public class GetApplyAgentDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOption> CountryOptions { get; set; }
        public List<SelectListOptionDto> CityOptions { get; set; }
        public string ApplyAgentFaqSlug { get; set; }
    }
}
