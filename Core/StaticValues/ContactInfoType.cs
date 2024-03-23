using Core.StaticClass;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class ContactInfoType
    {
        public List<SelectListOption> Types = new List<SelectListOption>()
        {
            new SelectListOption()
            {
                ID = 1,
                Value = "Address"
            },
            new SelectListOption()
            {
                ID = 2,
                Value = "Phone"
            },
            new SelectListOption()
            {
                ID = 3,
                Value = "Email"
            },
        };
    }
}
