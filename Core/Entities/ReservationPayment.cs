using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReservationPayment : BaseEntity
    {
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? DebtTypeId { get; set; }
        public int? DiscountRate { get; set; }
        public decimal Price { get; set; }
        public bool IsDebt { get; set; } // Else receivable
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
    }
}
