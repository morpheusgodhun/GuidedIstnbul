using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.StaticClass;
using Dto.ApiWebDtos.GeneralDtos;

namespace Core.StaticValues
{
    public class TagCategory
    {
        public List<SelectListOption> Categories = new List<SelectListOption>()
        {
            new SelectListOption()
            {
                ID = 1,
                Value = "Blog"
            },
            new SelectListOption()
            {
                ID = 2,
                Value = "Product"
            },
        };
    }
}
