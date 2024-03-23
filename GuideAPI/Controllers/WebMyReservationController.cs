using Core.IService;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using static Core.Enums;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebMyReservationController : ControllerBase
    {

        private readonly IReservationService _reservationService;
        private readonly IProductLanguageService _productLanguageService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly IReservationEditRequestService _reservationEditRequestService;
        private readonly IConstantValueService _constantValueService;

        public WebMyReservationController(IReservationService reservationService, IProductLanguageService productLanguageService, ISystemOptionService systemOptionService, IReservationEditRequestService reservationEditRequestService, IConstantValueService constantValueService)
        {
            _reservationService = reservationService;
            _productLanguageService = productLanguageService;
            _systemOptionService = systemOptionService;
            _reservationEditRequestService = reservationEditRequestService;
            _constantValueService = constantValueService;
        }
        [HttpGet("{reservationId}/{languageId}")]
        public CustomResponseDto<ProductNameDto> GetProductNameForReservation(Guid reservationId, int languageId)
        {
            var productNameDto = _productLanguageService.GetProductNameForReservation(reservationId, languageId);
            return CustomResponseDto<ProductNameDto>.Success(200, productNameDto);
        }
        [HttpGet("{userId}/{languageId}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public async Task<CustomResponseDto<GetMyReservationDto>> GetMyReservation(Guid userId, int LanguageID = 1) //todo: sabit değerleri kaldıralım
        {
            GetMyReservationDto getMyReservationDto = new GetMyReservationDto()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("MyReservation",LanguageID),
                Reservations = await _reservationService.MyReservationAsync(userId, LanguageID),
                CancellationReasons = _systemOptionService.GetSystemOptionByCategoryId(10, LanguageID),
                //null, //_infoCardService.GetInfoCardDtoList(LanguageID)
            };


            return CustomResponseDto<GetMyReservationDto>.Success(200, getMyReservationDto);
        }

        [HttpGet("{reservationId}/{languageId}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public CustomResponseDto<ReservationDetailDto> ReservationDetail(Guid reservationId, int languageId)
        {
            ReservationDetailDto dto = _reservationService.ReservationDetail(reservationId, languageId);
            return CustomResponseDto<ReservationDetailDto>.Success(200, dto);
        }
        [HttpPost]
        public CustomResponseNullDto CancelReservationRequest(CancelReservationRequestPostDto cancelReservationPostDto)
        {
            _reservationService.CancelReservationRequest(cancelReservationPostDto);
            return CustomResponseNullDto.Success(200);
        }
        [HttpPost]
        public CustomResponseNullDto UpdateReservationRequest(UpdateReservationRequestPostDto updateReservationRequestPostDto)
        {
            _reservationService.UpdateReservationRequest(updateReservationRequestPostDto);
            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{reservationId}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public CustomResponseDto<List<ReservationCancelRequestModalListDto>> GetReservationCancelRequests(string reservationId)
        {
            var data = _reservationEditRequestService.GetReservationEditRequests(reservationId, ReservationEditRequestType.Cancel).Select(x => new ReservationCancelRequestModalListDto
            {
                AdminAnswer = x.AdminAnswer,
                Reason = x.ReasonText,
                RequestStatus = ReservationEditRequestStatus.GetValue(x.RequestStatus),
            }).ToList();

            return CustomResponseDto<List<ReservationCancelRequestModalListDto>>.Success(200, data);
        }
        [HttpGet("{reservationId}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]
        public CustomResponseDto<List<ReservationUpdateRequestModalListDto>> GetReservationUpdateRequests(string reservationId)
        {
            var data = _reservationEditRequestService.GetReservationEditRequests(reservationId, ReservationEditRequestType.Update).Select(x => new ReservationUpdateRequestModalListDto
            {
                AdminAnswer = x.AdminAnswer,
                Reason = x.ReasonText,
                RequestStatus = ReservationEditRequestStatus.GetValue(x.RequestStatus),
            }).ToList();

            return CustomResponseDto<List<ReservationUpdateRequestModalListDto>>.Success(200, data);
        }

        [HttpGet("{agentId}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache120)]

        public async Task <CustomResponseDto<GetAgentReservationsDto>> GetAgentReservations(string agentId)
        {
            GetAgentReservationsDto dto = new()
            {
                ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Agent Reservations", 1),
                Reservations = await _reservationService.AgentReservationsAsync(new Guid(agentId))
            };
            return CustomResponseDto<GetAgentReservationsDto>.Success(200, dto);
        }
    }
}
