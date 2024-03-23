using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReservationBillingInfo : BaseEntity
    {
        //billingInfo
        public bool IsIndividual { get; set; }  //Else Corporate
        public string? BillingFullname { get; set; } //Just For Individual
        public string? TcOrPassportNo { get; set; } //Just For Individual
        public string BillingEmail { get; set; }
        public string BillingPhoneNumber { get; set; }
        public string BillingAddress { get; set; }
        public string? BillingTaxAdministration { get; set; } //Just For Corporate
        public string? BillingTaxNumber { get; set; } //Just For Corporate7

        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
