using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public static class PaymentTypeList
    {
        public static List<SelectListOption> PaymentTypes = new()
        {
            new () { ID = 1, Value = "Peşin Odeme" },
            new () { ID = 2, Value = "Ön Ödeme" },
            new () { ID = 3, Value = "Kısmi Ödeme" },
            new () { ID = 4, Value = "İndirim" },
        };
        public enum No
        {
            PesinOdeme = 1,
            OnOdeme = 2,
            KismiOdeme = 3,
            Indirim = 4
        }
        public static string GetValue(int? id)
        {
            return id != null ? PaymentTypes.FirstOrDefault(x => x.ID == id).Value : "";
        }
    }
}
