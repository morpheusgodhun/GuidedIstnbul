using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public static class PaymentMethodList
    {
        public static List<SelectListOption> PaymentMethods = new()
        {
            new () { ID = 1, Value = "Kredi Kartı" },
            new () { ID = 2, Value = "Nakit" },
            new () { ID = 3, Value = "Kupon Kodu İndirimi" },
            new () { ID = 4, Value = "Peşin Ödeme İndirimi" },
            new () { ID = 5, Value = "Acente Tur İndirimi" },
            new () { ID = 6, Value = "Acente Servis İndirimi" },
            new () { ID = 7, Value = "Rehber Tur İndirimi" },
            new () { ID = 8, Value = "Rehber Servis İndirimi" },

        };
        public enum No
        {
            KrediKarti = 1,
            Nakit = 2,
            KuponKodu = 3,
            PesinOdemeIndirimi = 4,
            AcenteTurIndirimi = 5,
            AcenteServisIndirimi = 6,
            RehberTurIndirimi = 7,
            RehberServisIndirimi = 8
        }
        public static string GetValue(int? id)
        {
            return id != null ? PaymentMethods.FirstOrDefault(x => x.ID == id).Value : "";
        }
    }
}
