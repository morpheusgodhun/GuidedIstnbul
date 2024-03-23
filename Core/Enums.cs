using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Enums
    {
        public enum ApproveStatus
        {
            [Description("Onaylandı")]
            Onaylandi = 1,
            [Description("Onay Bekliyor")]
            OnayBekliyor = 2,
            [Description("Reddedildi")]
            Reddedildi = 3
        }
        public enum ReservationEditRequestType
        {
            Update = 1,
            Cancel = 2,
        }
        public enum DiscountType
        {
            Amount = 1,
            Percentage = 2
        }

    }
}
