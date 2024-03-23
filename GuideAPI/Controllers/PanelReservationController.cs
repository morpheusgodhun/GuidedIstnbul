using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiPanelDtos.ReservationEditRequestDtos;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiPanelDtos.ReservationRequestDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideAPI.Models.ApiToWebModels.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Mail;
using Service.Services;
using static Core.Enums;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationEditRequestService _reservationEditRequestService;
        private readonly IConfiguration _configuration;

        public PanelReservationController(IReservationService reservationService, IReservationEditRequestService reservationEditRequestService, IConfiguration configuration)
        {
            _reservationService = reservationService;
            _reservationEditRequestService = reservationEditRequestService;
            _configuration = configuration;
        }

        [HttpGet]
        public CustomResponseDto<List<ReservationPanelListDto>> ReservationList()
        {
            List<ReservationPanelListDto> reservationList = _reservationService.ReservationPanelList();
            return CustomResponseDto<List<ReservationPanelListDto>>.Success(200, reservationList);
        }

        [HttpGet]
        public CustomResponseDto<List<ReservationListDto>> UncomplatedReservationList()
        {
            List<ReservationListDto> reservationList = _reservationService.UncomplatedReservationList();
            return CustomResponseDto<List<ReservationListDto>>.Success(200, reservationList);
        }
        [HttpGet]
        public CustomResponseDto<List<ReservationListDto>> AskForPriceReservationList()
        {
            List<ReservationListDto> reservationList = _reservationService.AskForPriceReservationList();
            return CustomResponseDto<List<ReservationListDto>>.Success(200, reservationList);
        }

        [HttpGet]
        public CustomResponseDto<List<ReservationListDto>> ComplatedReservationList()
        {
            List<ReservationListDto> reservationList = _reservationService.ComplatedReservationList();
            return CustomResponseDto<List<ReservationListDto>>.Success(200, reservationList);
        }

        [HttpGet("{reservationId}/{bookingStatus}")]
        public CustomResponseNullDto SetReservationBookingStatus(Guid reservationId, int bookingStatus)
        {
            _reservationService.SetReservationBookingStatus(reservationId, bookingStatus);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{reservationId}")]
        public CustomResponseDto<ReservationFormDetailDto> ReservationFormDetail(Guid reservationId)
        {
            ReservationFormDetailDto dto = _reservationService.GetReservationFormDetailDto(reservationId);
            return CustomResponseDto<ReservationFormDetailDto>.Success(200, dto);
        }
        [HttpGet("{reservationId}")]
        public CustomResponseDto<List<ReservationCancelRequestDto>> GetReservationCancelRequests(string reservationId)
        {
            var data = _reservationEditRequestService.GetReservationEditRequests(reservationId, ReservationEditRequestType.Cancel).Select(x => new ReservationCancelRequestDto
            {
                RequestId = x.RequestId,
                ReasonText = x.ReasonText,
                AdminAnswer = x.AdminAnswer,
                Reason = x.Reason,
                RequestStatus = ReservationEditRequestStatus.GetValue(x.RequestStatus),
            }).ToList();

            return CustomResponseDto<List<ReservationCancelRequestDto>>.Success(200, data);
        }
        [HttpGet("{reservationId}")]
        public CustomResponseDto<List<ReservationUpdateRequestDto>> GetReservationUpdateRequests(string reservationId)
        {
            var data = _reservationEditRequestService.GetReservationEditRequests(reservationId, ReservationEditRequestType.Update).Select(x => new ReservationUpdateRequestDto
            {
                RequestId = x.RequestId,
                AdminAnswer = x.AdminAnswer,
                Reason = x.ReasonText,
                RequestStatus = ReservationEditRequestStatus.GetValue(x.RequestStatus),
            }).ToList();

            return CustomResponseDto<List<ReservationUpdateRequestDto>>.Success(200, data);
        }
        public CustomResponseNullDto ReplyEditRequest(ReservationEditRequestReplyDto replyEditRequestDto)
        {
            _reservationEditRequestService.ReplyEditRequest(replyEditRequestDto);
            return CustomResponseNullDto.Success(200);
        }
        [HttpPost]
        public CustomResponseNullDto SendReservationToOperation(SendReservationToOperationPostDto sendReservation)
        {
            _reservationService.SendReservationToOperation(sendReservation.ReservationId);
            return CustomResponseNullDto.Success(200);
        }

        [HttpPost]
        public CustomResponseNullDto ReservationPaymentSendMail(ReservationPaymentSendMailDto paymentSendMail)
        {
            string language = HttpContext.Request.Headers["language"];
            language = string.IsNullOrEmpty(language) ? LanguageList.BaseLanguage.UrlPrefix : language;
            string linkUrl = _configuration.GetSection("CreatedPaymentLink").Value;
            linkUrl = linkUrl.Replace("{language}", language);
            paymentSendMail.Url = linkUrl;
            _reservationService.ReservationPaymentSendMail(paymentSendMail);

            return CustomResponseNullDto.Success(200);
        }
    }
}
