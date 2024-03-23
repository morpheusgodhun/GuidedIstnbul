using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Payment
{
    public class GetPaymentDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<SelectListOptionDto> PickUpPoints { get; set; }
        public PaymentReservationDetail ReservationDetail { get; set; }
    }

    public class PaymentReservationDetail
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public int GuestNumber { get; set; }
        public string LanguageName { get; set; }
        public decimal LanguagePrice { get; set; }
        public List<PaymentReservationDetailAdditionalService> AdditionalService { get; set; }
        public string TourName { get; set; }
        public decimal TourPrice { get; set; }
        public List<SelectListOptionDto> Currencies { get; set; }
    }

    public class PaymentReservationDetailAdditionalService
    {
        public string AdditionalServiceName { get; set; }
        public decimal AdditionalServicePrice { get; set; }
        public List<PaymentReservationDetailAdditionalServiceItem> Items { get; set; }
    }
    public class PaymentReservationDetailAdditionalServiceItem
    {
        public string ItemName { get; set; }
        public string ItemValue { get; set; }
    }
}
