using Core.IService;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuideAPI.Models.ApiToWebModels;
using GuideAPI.Models.GeneralDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebPaymentController : ControllerBase
    {
        private readonly IConstantValueService _constantValueService;

        public WebPaymentController(IConstantValueService constantValueService)
        {
            _constantValueService = constantValueService;
        }

        //yapma
        [HttpGet("{LanguageID}")]
        public async Task<CustomResponseDto<GetPaymentDto>> GetPayment(string LanguageID)
        {
            var getPaymentDto = new GetPaymentDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Payment",Convert.ToInt32(LanguageID)),                        
                PickUpPoints = new List<SelectListOptionDto>()
                {
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "1",
                    //    Option = "Divan Hotel"
                    //},
                    //new SelectListOptionDto()
                    //{
                    //    OptionID = "2",
                    //    Option = "Demircode"
                    //},
                },
                ReservationDetail = new PaymentReservationDetail()
                {
                    ConstantValues = new List<ConstantValueDto>()
                    {
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailTitle",
                            Value = "Reservation Detail"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailStartDateLabel",
                            Value = "Start Date"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailStartTimeLabel",
                            Value = "Start Time"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailGuestNumberLabel",
                            Value = "Guest(s) Number"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailLanguageLabel",
                            Value = "Language"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailCouponCodePlaceholder",
                            Value = "Enter your coupon code.."
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailApplyButton",
                            Value = "Apply"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailDiscountAmounLabel",
                            Value = "Discount Amount"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailTotalPriceLabel",
                            Value = "Total Price"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailDepositFeeLabel",
                            Value = "Deposit Fee"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailReservationWithoutPaymentButton",
                            Value = "Reservation Without Payment"
                        },
                        new ConstantValueDto()
                        {
                            Key = "PaymentReservationDetailCurrencyTypePlaceholder",
                            Value = "Currency"
                        },
                    },

                    StartDate = DateTime.Now,
                    StartTime = "09:00 AM",
                    GuestNumber = 3,
                    LanguageName = "German",
                    LanguagePrice = 100,
                    AdditionalService = new List<PaymentReservationDetailAdditionalService>()
                    {
                        new PaymentReservationDetailAdditionalService()
                        {
                            AdditionalServiceName = "2-Hour Private Yacht",
                            AdditionalServicePrice = 1500,
                            Items = new List<PaymentReservationDetailAdditionalServiceItem>(){
                                new PaymentReservationDetailAdditionalServiceItem()
                                {
                                    ItemName = "Yacht Tour Time",
                                    ItemValue = "22.06.2023 12:00"
                                }
                            }
                        },
                        new PaymentReservationDetailAdditionalService()
                        {
                            AdditionalServiceName = "No Transfer",
                            AdditionalServicePrice = 0,
                        },

                    },
                    TourName = "2-Day Tour",
                    TourPrice = 900,
                    Currencies = new List<SelectListOptionDto>()
                    {
                        //new SelectListOptionDto()
                        //{
                        //    OptionID = "1",
                        //    Option = "$"
                        //},
                        //new SelectListOptionDto()
                        //{
                        //    OptionID = "2",
                        //    Option = "€"
                        //},
                        //new SelectListOptionDto()
                        //{
                        //    OptionID = "3",
                        //    Option = "₺"
                        //},
                    },
                },
            };



            return CustomResponseDto<GetPaymentDto>.Success(200, getPaymentDto);
        }
    }
}
