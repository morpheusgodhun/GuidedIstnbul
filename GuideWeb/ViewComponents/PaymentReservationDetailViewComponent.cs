using Core.Entities;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "PaymentReservationDetailViewComponent")]
    public class PaymentReservationDetailViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private readonly ICookie _cookie;

        public PaymentReservationDetailViewComponent(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _cookie = cookie;
            _url = this._configuration["BaseUrl"];
        }

        public IViewComponentResult Invoke(string reservationId)
        {
            //TODO: language statiklerinin düzenlenmesi lazım
            int languageId = 1;
            string url = _url + "Payment/PaymentReservationDetail/" + reservationId + "/" + languageId;
            string url2 = _url + "WebMyReservation/GetProductNameForReservation/" + reservationId + "/" + languageId;
            CustomResponseDto<PaymentReservationDetailDto> paymentReservationDetailDto = _apiHandler.GetApi<CustomResponseDto<PaymentReservationDetailDto>>(url);
            CustomResponseDto<ProductNameDto> ProductNameDto = _apiHandler.GetApi<CustomResponseDto<ProductNameDto>>(url2);

            var paymentReservationDetail = paymentReservationDetailDto.Data;


            //Ödeme hesaplama 
            var allPayments = paymentReservationDetail.ReservationPayments;
            paymentReservationDetail.TourPrice = allPayments.Where(p => p.IsDebt && p.DebtTypeId == (int)PaymentDebtTypeList.No.Product).Select(x => x.Price).FirstOrDefault();


            //total priceyi tekrar hesaplıyorum
            paymentReservationDetail.TotalPrice = allPayments.Where(p => p.IsDebt).Sum(x => x.Price);


            PaymentShowDetailDto paymentDetail = new PaymentShowDetailDto()
            {
                TotalPrice = paymentReservationDetail.TotalPrice,
                DepositoPrice = paymentReservationDetail.DepositoPrice,
                Discounts = new List<PaymentShowDetailDiscountsDto>(){}
            };



            //TODO: Buradaki statik alanları constant value alanına ekleme yaacağız sonra
            // şuanda yüzde göstermeyeceğim sadece indirim oranını göstereceğim
            //eğer yüzdeli göstermek istenirse guide veya acentanın tur için ve servis için olan indirimlerini ayrı ayrı göstermek gerekecektir 
            var agentDiscountPrice = allPayments.Where(p => !p.IsDebt && p.PaymentTypeId == (int)PaymentTypeList.No.Indirim && (p.PaymentMethodId == (int)PaymentMethodList.No.AcenteTurIndirimi || p.PaymentMethodId == (int)PaymentMethodList.No.AcenteServisIndirimi)).Sum(x => x.Price);
            if (agentDiscountPrice > 0)
            {
                paymentDetail.Discounts.Add(new PaymentShowDetailDiscountsDto()
                {
                    Name = "Agent Discount",
                    Percentage = 0, 
                    Amount = agentDiscountPrice
                });
            }

            var guideDiscountPrice = allPayments.Where(p => !p.IsDebt && p.PaymentTypeId == (int)PaymentTypeList.No.Indirim && (p.PaymentMethodId == (int)PaymentMethodList.No.RehberTurIndirimi || p.PaymentMethodId == (int)PaymentMethodList.No.RehberServisIndirimi)).Sum(x => x.Price);
            if (guideDiscountPrice > 0)
            {
                paymentDetail.Discounts.Add(new PaymentShowDetailDiscountsDto()
                {
                    Name = "Guide Discount",
                    Percentage = 0,
                    Amount = guideDiscountPrice
                });
            }

            var couponDiscountPrice = allPayments.Where(p => !p.IsDebt && p.PaymentTypeId == (int)PaymentTypeList.No.Indirim && (p.PaymentMethodId == (int)PaymentMethodList.No.KuponKodu)).Sum(x => x.Price);
            if (couponDiscountPrice > 0)
            {
                paymentDetail.Discounts.Add(new PaymentShowDetailDiscountsDto()
                {
                    Name = "Coupon Discount",
                    Percentage = 0,
                    Amount = couponDiscountPrice
                });
            }

            var d = allPayments.Where(x => x.IsDebt).Sum(x => x.Price);
            var f = allPayments.Where(x => !x.IsDebt).Sum(x => x.Price);

            paymentDetail.LastTotalPrice = d - f; 


            //TODO: eğer kişi agent veya guide değilse kupon kodu kısmı gösterelim
            //SpecialServicesDiscount=-1,SpecialTourDiscount=-1,
            string action = ViewContext.RouteData.Values["action"].ToString();
            ViewBag.RenderCouponCode = action != "PaymentParticipant" && action != "PaymentBilling";


            PaymentAgentInfoDto paymentAgentInfoDto = new() { DefaultTourDiscount = 0, ServicesDiscountRate = 0, SpecialDiscount = -1, WithoutPay = false };
            if (User.IsInRole("Agent"))
            {
                var agentId = _cookie.getMemberAgentId();
                string url3 = _url + "Payment/GetPaymentAgentInfo/" + ProductNameDto.Data.Id + "/" + agentId;
                paymentAgentInfoDto = _apiHandler.GetApi<CustomResponseDto<PaymentAgentInfoDto>>(url3).Data;
            }

            var pageModel = (paymentReservationDetailDto.Data, ProductNameDto.Data, paymentAgentInfoDto, paymentDetail);
            if (paymentReservationDetailDto is null)
            {
                return View();
            }
            else
            {
                return View(pageModel);
            }
        }
    }
}
