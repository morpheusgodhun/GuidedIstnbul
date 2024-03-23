using Core.Entities;
using Core.StaticClass;
using Data.Migrations;
using Dto.ApiPanelDtos.ReservationRequestDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Profile;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using GuideWeb.Validations.ReservationValidations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace GuideWeb.Controllers
{
    public class ReservationController : CustomBaseController
    {
        private readonly ICookie _cookie;
        public ReservationController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
            _cookie = cookie;
        }

        //bir rez no vererek detayları getirme işlemi için
        [HttpGet]
        public async Task<IActionResult> ReservationInquiry()
        {
            int languageId = GetLanguage().Id;
            string url = _url + "WebUserPage/GetReservationInquery/" + languageId;
            CustomResponseDto<GetReservationInquiryDto> getReservationInquiryDto = await _apiHandler.GetAsync<CustomResponseDto<GetReservationInquiryDto>>(url);

            if (getReservationInquiryDto is null)
            {
                return View();
            }
            else
            {
                return View(getReservationInquiryDto.Data);
            }
        }


        [HttpGet]
        public IActionResult ReservationInquiryResponse(string reservationCodeP)
        {
            StringValues reservationCode = HttpContext.Request.Query["reservationCode"];

            if (StringValues.IsNullOrEmpty(reservationCode))
                reservationCode = HttpContext.Request.Query["ReservationCode"];

            ReservationInquiryPostDto reservationInquiryPostDto = new()
            {
                LanguageId = GetLanguage().Id,
                ReservationCode = reservationCode.ToString()
            };
            string url = _url + "WebUserPage/PostReservationInquery";
            CustomResponseDto<WebReservationListDto> dto = _apiHandler.PostApi<CustomResponseDto<WebReservationListDto>>(reservationInquiryPostDto, url);

            return View(dto.Data);
        }

        [HttpGet]
        public async Task<IActionResult> MyReservation()
        {
            //string url = _url + "MyReservationPage/GetMyReservation/1";

            var memberId = _cookie.getMemberId();

            string url = _url + $"WebMyReservation/GetMyReservation/{memberId}/1";
            CustomResponseDto<GetMyReservationDto> getMyReservationDto = await _apiHandler.GetAsync<CustomResponseDto<GetMyReservationDto>>(url);

            if (getMyReservationDto is null)
            {
                return View();
            }
            else
            {
                return View(getMyReservationDto.Data);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AgentReservations()
        {
            string memberAgentId = _cookie.getMemberAgentId();
            string url = _url + $"WebMyReservation/GetAgentReservations/{memberAgentId}";
            CustomResponseDto<GetAgentReservationsDto> getMyReservationDto = await _apiHandler.GetAsync<CustomResponseDto<GetAgentReservationsDto>>(url);

            if (getMyReservationDto is null)
            {
                return View();
            }
            else
            {
                return View(getMyReservationDto.Data);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ReservationDetail(string id)
        {
            int languageId = 1;
            string url = _url + "WebMyReservation/ReservationDetail/" + id + "/" + languageId;
            CustomResponseDto<ReservationDetailDto> reservationSuccessDto = await _apiHandler.GetAsync<CustomResponseDto<ReservationDetailDto>>(url);

            if (reservationSuccessDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationSuccessDto.Data);
            }
        }

        public async Task<IActionResult> CustomTourRequest(string id)
        {
            int languageId = 1;
            string url = _url + "WebUserPage/CustomTourRequestDetail/" + id + "/" + languageId;
            CustomResponseDto<GetCustomTourRequestDetailDto> requestDetail = await _apiHandler.GetAsync<CustomResponseDto<GetCustomTourRequestDetailDto>>(url);
            return View(requestDetail.Data);
        }
        [HttpPost]
        public IActionResult CancelReservation(CancelReservationRequestPostDto cancelReservationRequestPostDto)
        {
            /*
            //CancelReservationValidator validator = new CancelReservationValidator();
            //var validationResult = validator.Validate(cancelReservationPostDto);
            //if (!validationResult.IsValid)
            //{
            //    var err = validationResult.Errors;
            //    ViewBag.CancellationTextValidationMessage = err.
            //        Where(x => x.PropertyName == nameof(CancelReservationPostDto.CancellationReasonText))?.
            //        FirstOrDefault()?.ErrorMessage;

            //    ViewBag.CancellationReasonNotSelectedValidationMessage = err.
            //        Where(x => x.PropertyName == nameof(CancelReservationPostDto.CancellationReasonId))?.
            //        FirstOrDefault()?.ErrorMessage;

            //    ViewBag.ReadCancellationPolicyValidationMessage = err.Where(x => x.PropertyName == nameof(CancelReservationPostDto.ReadCancellationPolicy))?.FirstOrDefault()?.ErrorMessage;

            //    return ValidationProblem();
            //}
            */
            string url = _url + "WebMyReservation/CancelReservationRequest/";
            _apiHandler.PostApi<CustomResponseNullDto>(cancelReservationRequestPostDto, url);

            return RedirectToAction("MyReservation");
        }

        [HttpPost]
        public IActionResult UpdateReservation(UpdateReservationRequestPostDto updateReservationRequestPostDto)
        {
            string url = _url + "WebMyReservation/UpdateReservationRequest/";
            _apiHandler.PostApi<CustomResponseNullDto>(updateReservationRequestPostDto, url);
            return RedirectToAction("MyReservation");
        }
        [HttpGet]
        public async Task<IActionResult> GetReservationCancelRequests(GetReservationsRequestsDto getDto)
        {
            string url = _url + $"WebMyReservation/GetReservationCancelRequests/{getDto.ReservationId}";
            var data = await _apiHandler.GetAsync<CustomResponseDto<List<ReservationCancelRequestModalListDto>>>(url);
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetReservationUpdateRequests(GetReservationsRequestsDto getDto)
        {
            string url = _url + $"WebMyReservation/GetReservationUpdateRequests/{getDto.ReservationId}";
            var data = await _apiHandler.GetAsync<CustomResponseDto<List<ReservationCancelRequestModalListDto>>>(url);
            return Ok(data);
        }


        [HttpGet]
        public IActionResult AskForPriceSuccess(string id)
        {
            /*
            int languageId = 1;
            string url = _url + "Payment/ReservationSuccess/" + id + "/" + languageId;
            CustomResponseDto<ReservationSuccessDto> reservationSuccessDto = _apiHandler.GetApi<CustomResponseDto<ReservationSuccessDto>>(url);
            
                return View(reservationSuccessDto.Data);
            */
            return View();

        }
    }
}
