
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.GeneralPages
{
    public class GetApplyGuideDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> LanguageKnowOptions { get; set; }
        public List<SelectListOptionDto> SpecializeCityOptions { get; set; }
        public string? ApplyGuideFaqSlug { get; set; }
    }
}
