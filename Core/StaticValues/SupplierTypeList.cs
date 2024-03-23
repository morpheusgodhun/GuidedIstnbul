using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class SupplierTypeList
    {

        public List<SelectListOption> SupplierTypes = new()
        {
            new SelectListOption() { ID = 1, Value = "Service" },
            new SelectListOption() { ID = 2, Value = "Shop" },
        };
        public enum No
        {
            Service = 1,
            Shop = 2,
        }
    }
}
