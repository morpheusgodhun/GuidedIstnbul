using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Discount;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    //rezervasyon için uygulanan discount bilgilerini ekleyeceğim
    public class PaymentReservationDetailDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public Guid ReservationId { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public int ParticipantNumber { get; set; }
        public List<ReservationFormDetailAdditionalServiceDto> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositoPrice { get; set; }
        public string CancellationPolicy { get; set; }
        public PaymentDiscountDto? PaymentDiscountDto { get; set; }
        public int DayNumber { get; set; }
        public List<PaymentListByReservationIdDto> ReservationPayments { get; set; }
        public decimal TourPrice { get; set; }
        /*
        public decimal AgentDiscount { get; set; }
        public decimal GuideDiscount { get; set; }
        public decimal CouponDiscount { get; set; }*/
    }
    //public class PaymentReservationDetailProductDto
    //{
    //    public string ProductName { get; set; }
    //    public decimal Price { get; set; }
    //}


    public class PaymentAgentInfoDto
    {
        // özel olarak 0 olursa indirim yapma demek olacak
        // eğer indirim bilgisi yoksa -1 olarak ekleme yapalım

        //özellerde indirim bilgisi yoksa genele baklır
        //ordada indirim yoksa indirim yapılmaz

        public int DefaultTourDiscount { get; set; } // yüzde kaç indirim genel oalrak
        public int ServicesDiscountRate { get; set; } // servislerde kaç indirim genel
        public bool WithoutPay { get; set; } // ödeme yapmadan alabilirmi

        public int SpecialDiscount { get; set; } //o ürüne özel bir indirim varmı. alttakiler atıl kalacak sanki


        public int SpecialTourDiscount { get; set; } //o tura özel bir indirim varmı
        public int SpecialServicesDiscount { get; set; } // o servise özel bir indirim varmı

    }


    public class PaymentShowDetailDto
    {
        public decimal TotalPrice { get; set; }
        //public decimal TourPrice { get; set; }
        //public decimal AdditionalPrice { get; set; }
        public decimal DepositoPrice { get; set; }
        public decimal LastTotalPrice { get; set; }
        public List<PaymentShowDetailDiscountsDto> Discounts { get; set; }
    }

    public class PaymentShowDetailDiscountsDto
    {
        public string Name { get; set; } //acenta - guide - kupon
        public decimal Amount { get; set; } // ne kadar indirim oldu - amount hep dolu olacak 
        public decimal Percentage { get; set; } // yüzde kaç indirim var - burası oranlı ise dolu olacak hesaplayp amount kısmmına yazacak
    }

}
