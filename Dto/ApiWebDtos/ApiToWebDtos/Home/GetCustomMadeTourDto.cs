using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Home
{
    public class GetCustomMadeTourDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> Destinations { get; set; }
        public List<SelectListOption> Countries { get; set; }
        public List<SelectListOption> Languages { get; set; }
        public List<SelectListOptionDto> HowFindUs { get; set; }
        public List<SelectListOptionDto> Interests { get; set; }
    }
}
