using Core.StaticClass;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class PageTypeList
    {
        public List<SelectListOption> types = new List<SelectListOption>()
        {
            new SelectListOption() { ID = 1, Value = "Base Page" },
            new SelectListOption() { ID = 2, Value = "Custom Page" },
            new SelectListOption() { ID = 3, Value = "Layout" }, //component / partialView
            new SelectListOption() { ID = 4, Value = "Static" }, // Controller ve Action u belli olan
        };
    }
}
