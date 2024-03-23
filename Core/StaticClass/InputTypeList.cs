using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticClass
{
    public static class InputTypeList
    {
        public static List<SelectListOption> Types = new List<SelectListOption>()
        {
            new SelectListOption()
            {
                ID = 1,
                Value = "Text",
            },
            new SelectListOption()
            {
                ID = 2,
                Value = "Text Area",
            },
            new SelectListOption()
            {
                ID = 3,
                Value = "Number",
            },
            new SelectListOption()
            {
                ID = 4,
                Value = "Date",
            },
            new SelectListOption()
            {
                ID = 5,
                Value = "Time",
            },
            new SelectListOption()
            {
                ID = 6,
                Value = "Checkbox",
            },
            new SelectListOption()
            {
                ID = 7,
                Value = "Label",
            },

        };
    }
}
