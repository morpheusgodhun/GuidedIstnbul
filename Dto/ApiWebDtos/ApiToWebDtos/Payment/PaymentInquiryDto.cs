using Dto.ApiWebDtos.GeneralDtos;

namespace Dto.ApiWebDtos.ApiToWebDtos.Payment
{
    public class PaymentInquiryDto
    {
        public List<ConstantValueDto>? ConstantValues { get; set; }
        public Guid ReservationId { get; set; }
        public Guid UrlCode { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string SecurityCode { get; set; } //cvc
        public decimal? Amount { get; set; }
    }
}
