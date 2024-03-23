using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;

namespace Dto.ApiPanelDtos.ReservationPaymentDtos
{
    public class PaymentListByReservationIdDto
    {
        public Guid Id { get; set; }
        public string? PaymentType { get; set; }
        public int? PaymentTypeId { get; set; } //silenebilir - silinmesin kullanıyorum 
        public string? PaymentMethod { get; set; }
        public int? PaymentMethodId { get; set; } //silenebilir - silinmesin kullanıyorum 
        public string? DebtType { get; set; }
        public int? DebtTypeId { get; set; } //silenebilir - silinmesin kullanıyorum 
        public int? DiscountRate { get; set; }
        public decimal Price { get; set; }
        public bool IsDebt { get; set; } 
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
