using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PaymentType : BaseEntity
    {
        public string Type { get; set; }
        public ICollection<ReservationPayment> ReservationPayments { get; set; }
    }
}
