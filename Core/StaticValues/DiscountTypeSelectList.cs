using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class DiscountTypeSelectList
    {
        public List<SelectListOption> DiscountTypes = new()
        {
            new SelectListOption() { ID = 1, Value = "Amount" },
            new SelectListOption() { ID = 2, Value = "Percentage" },
        };
        public string GetValue(int id)
        {
            return DiscountTypes.Where(x => x.ID == id).FirstOrDefault().Value;
        }
        public int GetValue(string value)
        {
            return DiscountTypes.Where(x => x.Value == value).FirstOrDefault().ID;
        }


    }
}
