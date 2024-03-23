using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticClass
{
    public class TourTypeList
    {
        public static List<SelectListOptionDto> TourTypes = new List<SelectListOptionDto>()
        {
            new SelectListOptionDto()
            {
               OptionID = new Guid("436b011d-62d9-4284-ad4b-0c81bca8862a"),
               Option = "Best Deal",
            }
        };
    }

}
