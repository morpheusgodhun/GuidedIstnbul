using Core.StaticValues;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public static class PaymentDebtTypeList
    {
        //borç tipi
        public static List<SelectListOption> DebtTypes = new List<SelectListOption>()
        {
            new SelectListOption() { ID = 1, Value = "Product" },
            new SelectListOption() { ID = 2, Value = "Additional" },
            new SelectListOption() { ID = 3, Value = "KDV" }, // Bu tedbir olsun diye dursun kdvyi ayrı hesaplayalım denilirse
            
            //new SelectListOption() { ID = 4, Value = "Guide" },
        };
        public static string GetValue(int? id)
        {
            return id != null ? DebtTypes.FirstOrDefault(x => x.ID == id).Value : "";
        }

        public enum No
        {
            Product = 1,
            Additional = 2,
            Kdv = 3,
        }
    }
}