using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.ApiToWebDtos.Payment.Results;
using Dto.ApiWebDtos.WebToApiDtos.Payment;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Service.Payment;

using static Dto.ApiWebDtos.WebToApiDtos.Reservation.PaymentReservationDetailDto;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : CustomBaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IDiscountService _discountService;
        readonly IYKPaymentService _ykPaymentService;
        private readonly IUserExtensionMailService _userExtensionMailService;

        private readonly IReservationPaymentService _reservationPaymentService;

        public PaymentController(IReservationService reservationService, IDiscountService discountService, IYKPaymentService ykPaymentService, IReservationPaymentService reservationPaymentService, IUserExtensionMailService userExtensionMailService)
        {
            _reservationService = reservationService;
            _discountService = discountService;
            _ykPaymentService = ykPaymentService;
            _reservationPaymentService = reservationPaymentService;
            _userExtensionMailService = userExtensionMailService;
        }

        [HttpPost]
        public CustomResponseDto<Guid> ReservationGeneralInfo(ReservationFormMainInformationDto dto)
        {
            string language = HttpContext.Request.Headers["language"];
            dto.LanguagePrefix = string.IsNullOrEmpty(language) ? LanguageList.BaseLanguage.UrlPrefix : language;
            Guid reservationId = _reservationService.ReservationGeneralInfo(dto);
            return CustomResponseDto<Guid>.Success(204, reservationId);
        }

        [HttpPost]
        public CustomResponseDto<RegisterReservationUser> RegisterReservationUser(RegisterReservationUser dto)
        {
            RegisterReservationUser newUser = _reservationService.RegisterReservationUser(dto);
            return CustomResponseDto<RegisterReservationUser>.Success(204, newUser);
        }



        [HttpGet("{reservationId}/{languageId}")]
        public CustomResponseDto<ReservationParticipantInformationDto> GetReservationParticipantInformationDto(Guid reservationId, int languageId)
        {
            ReservationParticipantInformationDto reservationParticipantInformationDto = _reservationService.GetReservationParticipantInformationDto(reservationId, languageId);

            return CustomResponseDto<ReservationParticipantInformationDto>.Success(200, reservationParticipantInformationDto);
        }

        [HttpPost]
        public CustomResponseDto<Guid> ReservationParticipantInformation(ReservationParticipantInformationDto dto)
        {
            Guid reservationId = _reservationService.ReservationParticipantInformation(dto);
            return CustomResponseDto<Guid>.Success(204, reservationId);
        }

        [HttpGet("{reservationId}/{languageId}")]
        public CustomResponseDto<ReservationBillingInformationDto> GetReservationBillingInformationDto(Guid reservationId, int languageId)
        {
            ReservationBillingInformationDto reservationBillingInformationDto = _reservationService.GetReservationBillingInformationDto(reservationId, languageId);
            return CustomResponseDto<ReservationBillingInformationDto>.Success(200, reservationBillingInformationDto);
        }

        [HttpPost]
        public CustomResponseDto<Guid> ReservationBillingInformation(ReservationBillingInformationDto dto)
        {
            Guid reservationId = _reservationService.ReservationBillingInformation(dto);
            return CustomResponseDto<Guid>.Success(204, reservationId);
        }

        [HttpGet("{reservationId}/{languageId}")]
        public CustomResponseDto<ReservationPaymentInformationDto> GetReservationPaymentInformationDto(Guid reservationId, int languageId)
        {
            ReservationPaymentInformationDto reservationPaymentInformationDto = _reservationService.GetReservationPaymentInformationDto(reservationId, languageId);

            // son öödenmesi gereken miktar nedir onu çıkaralım
            var reservationPayments = _reservationPaymentService.PaymentListByReservationId(reservationId);

            var d = reservationPayments.Where(x => x.IsDebt).Sum(x => x.Price);
            var f = reservationPayments.Where(x => !x.IsDebt).Sum(x => x.Price);

            reservationPaymentInformationDto.LastTotalPrice = d - f;

            return CustomResponseDto<ReservationPaymentInformationDto>.Success(200, reservationPaymentInformationDto);
        }

        [HttpPost]
        public async Task<CustomResponseDto<PaymentInformationResultDto>> ReservationPaymentInformation(ReservationPaymentInformationDto dto)
        {
            //önce ykpaymentservice yanıtını bekle, sonra reservationService e gönder
            PaymentInformationResultDto? result = await DoThreeDRequest(new YKPosPaymentDto(
                dto.CardHolderName,
                dto.CardNumber,
                (decimal)dto.TotalPrice,
                dto.Month,
                dto.Year,
                dto.SecurityCode,
                "TL",
                dto.ReservationId.ToString(),
                base.GetLanguage(HttpContext.Request)));

            return CustomResponseDto<PaymentInformationResultDto>.Success(200, result);
        }
        [HttpPost]
        public async Task<CustomResponseDto<PaymentInformationResultDto>> PaymentInformation(PaymentInquiryDto dto)
        {

            PaymentInformationResultDto? result = await DoThreeDRequest(new YKPosPaymentDto(
                dto.CardHolderName,
                dto.CardNumber,
                (decimal)dto.Amount,
                dto.Month,
                dto.Year,
                dto.SecurityCode,
                "TL",
                dto.ReservationId.ToString(),
                base.GetLanguage(HttpContext.Request)));

            return CustomResponseDto<PaymentInformationResultDto>.Success(200, result);
        }
        async Task<PaymentInformationResultDto> DoThreeDRequest(YKPosPaymentDto yKPosPaymentDto)
        {
            var paymentResult = await _ykPaymentService.ThreeDGatewayRequest(yKPosPaymentDto);
            PaymentInformationResultDto result = new(new Guid(yKPosPaymentDto.ReservationId), paymentResult);

            return result;
        }

        [HttpGet("{id}/{amount}")]
        public CustomResponseDto<string> ReservationSuccessAddPayment(string id, decimal amount)
        {
            String reservationId = _reservationService.ReservationSuccessAddPayment(id, amount);
            return CustomResponseDto<string>.Success(204, reservationId);
        }


        [HttpPost]
        public CustomResponseDto<Guid> CompleteWithoutPayment(CompleteWithoutPaymentDto dto)
        {
            Guid reservationId = _reservationService.CompleteWithoutPayment(dto);
            return CustomResponseDto<Guid>.Success(204, reservationId);
        }


        [HttpGet("{reservationId}/{languageId}")]
        public CustomResponseDto<ReservationSuccessDto> ReservationSuccess(Guid reservationId, int languageId)
        {
            ReservationSuccessDto dto = _reservationService.GetReservationSuccessDto(reservationId, languageId);
            return CustomResponseDto<ReservationSuccessDto>.Success(200, dto);
        }

        [HttpGet("{reservationId}/{languageId}")]
        public CustomResponseDto<PaymentReservationDetailDto> PaymentReservationDetail(Guid reservationId, int languageId)
        {
            PaymentReservationDetailDto dto = _reservationService.GetPaymentReservationDetailDto(reservationId, languageId);
            return CustomResponseDto<PaymentReservationDetailDto>.Success(200, dto);
        }


        [HttpGet("{productId}/{agentId}")]
        public CustomResponseDto<PaymentAgentInfoDto> GetPaymentAgentInfo(Guid productId, Guid agentId)
        {
            PaymentAgentInfoDto dto = _reservationService.GetPaymentAgentInfo(productId, agentId);
            return CustomResponseDto<PaymentAgentInfoDto>.Success(200, dto);
        }
        [HttpPost]
        public CustomResponseDto<GetCouponDiscountResponseDto> GetCouponDiscount(GetCouponDiscountDto getCouponDiscountDto)
        {
            CustomResponseDto<GetCouponDiscountResponseDto> serviceResponse = _discountService.GetCouponDiscountForReservation(getCouponDiscountDto);
            return serviceResponse;
        }
        [HttpPost]
        public CustomResponseDto<bool> RemoveCouponDiscount(RemoveCouponDiscountDto removeDiscountDto)
        {
            _discountService.RemoveCouponDiscountFromReservation(removeDiscountDto);
            return CustomResponseDto<bool>.Success(200, true);
        }


        [HttpGet("{paymentCode}/{languageId}")]
        public CustomResponseDto<PaymentInquiryDto> GetPaymentInquiry(string paymentCode, int languageId)
        {
            //if (reservationPayment.ExpireDate < DateTime.Now)
            //    return CustomResponseDto<PaymentInquiryDto>.Fail(400, "Expired Payment Link"); //Süresi Dolmuş Ödeme Bağlantısı

            if (!_userExtensionMailService.IsUrlCodeValid(paymentCode, SendMailTemplateName.No.PaymentLinkMail))
                return CustomResponseDto<PaymentInquiryDto>.Fail(400, "Invalid URL");

            ReservationPaymentSendMailDto reservationPayment = _userExtensionMailService.UrlCodeByPayment(paymentCode);
            PaymentInquiryDto paymentInquiryDto = _reservationService.GetPaymentInquiryDto(reservationPayment.ReservationId);

            // son öödenmesi gereken miktar nedir onu çıkaralım
            //var reservationPayments = _reservationPaymentService.PaymentListByReservationId(reservationPayment.ReservationId);

            //var d = reservationPayments.Where(x => x.IsDebt).Sum(x => x.Price);
            //var f = reservationPayments.Where(x => !x.IsDebt).Sum(x => x.Price);

            paymentInquiryDto.Amount = reservationPayment.Amount;

            return CustomResponseDto<PaymentInquiryDto>.Success(200, paymentInquiryDto);
        }
    }
}
