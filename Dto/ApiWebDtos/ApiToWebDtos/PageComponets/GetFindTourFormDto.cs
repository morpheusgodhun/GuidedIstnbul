
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.PageComponets
{
    public class GetFindTourFormDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<OrderedSelectListOptionDto> WhereToOptions { get; set; }
        public List<SelectListOptionDto> SelectMonthOptions { get; set; }
        public List<SelectListOptionDto> TourTypeOptions { get; set; }

    }
}
