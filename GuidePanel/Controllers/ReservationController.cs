using Core.StaticClass;
using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiPanelDtos.ReservationEditRequestDtos;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiPanelDtos.ReservationRequestDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Web;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ReservationManagement)]

    public class ReservationController : CustomBaseController
    {
        public ReservationController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult ReservationList()
        {
            string url = _url + "PanelReservation/ReservationList";
            CustomResponseDto<List<ReservationPanelListDto>> reservationListDto = _apiHandler.GetApi<CustomResponseDto<List<ReservationPanelListDto>>>(url);

            if (reservationListDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationListDto.Data);
            }
        }
        public IActionResult UncomplatedReservationList()
        {
            string url = _url + "PanelReservation/UncomplatedReservationList";
            CustomResponseDto<List<ReservationListDto>> reservationListDto = _apiHandler.GetApi<CustomResponseDto<List<ReservationListDto>>>(url);

            if (reservationListDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationListDto.Data);
            }
        }
        public IActionResult AskForPriceReservationList()
        {
            string url = _url + "PanelReservation/AskForPriceReservationList";
            CustomResponseDto<List<ReservationListDto>> reservationListDto = _apiHandler.GetApi<CustomResponseDto<List<ReservationListDto>>>(url);

            if (reservationListDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationListDto.Data);
            }
        }

        public IActionResult ReservationFormDetail(string id)
        {
            string url = _url + "PanelReservation/ReservationFormDetail/" + id;
            CustomResponseDto<ReservationFormDetailDto> reservationListDto = _apiHandler.GetApi<CustomResponseDto<ReservationFormDetailDto>>(url);

            if (reservationListDto is null)
            {
                return View();
            }
            else
            {
                decimal totalDebtPrice = 0;
                decimal totalReceivedPrice = 0;
                decimal currentDebt = 0;
                foreach (var item in reservationListDto.Data.ReservationPayments)
                {
                    if (item.IsDebt) totalDebtPrice += item.Price;
                    else totalReceivedPrice += item.Price;
                }
                currentDebt = totalDebtPrice - totalReceivedPrice;
                ViewBag.CurrentDebt = currentDebt;

                return View(reservationListDto.Data);
            }
        }

        public IActionResult ComplatedReservationList()
        {
            string url = _url + "PanelReservation/ComplatedReservationList";
            CustomResponseDto<List<ReservationListDto>> reservationListDto = _apiHandler.GetApi<CustomResponseDto<List<ReservationListDto>>>(url);

            if (reservationListDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationListDto.Data);
            }
        }

        public IActionResult ConfirmReservation(string id)
        {
            int bookingStatus = 5;
            string url = _url + "PanelReservation/SetReservationBookingStatus/" + id + "/" + bookingStatus;
            _apiHandler.GetApi<CustomResponseDto<List<ReservationListDto>>>(url);
            return RedirectToAction("ReservationList");
        }
        public IActionResult RejectReservation(string id)
        {
            int bookingStatus = 6;
            string url = _url + "PanelReservation/SetReservationBookingStatus/" + id + "/" + bookingStatus;
            _apiHandler.GetApi<CustomResponseDto<List<ReservationListDto>>>(url);
            return RedirectToAction("ReservationList");
        }
        [HttpPost]
        public IActionResult ReplyEditRequest(ReservationEditRequestReplyDto replyEditRequestDto)
        {
            string url = _url + "PanelReservation/ReplyEditRequest/";
            _apiHandler.PostApi<CustomResponseNullDto>(replyEditRequestDto, url);
            return RedirectToAction("ReservationList");
        }
        [HttpGet]
        public IActionResult GetReservationCancelRequests(GetReservationsRequestsDto getDto)
        {
            string url = _url + $"PanelReservation/GetReservationCancelRequests/{getDto.ReservationId}";
            var data = _apiHandler.GetApi<CustomResponseDto<List<ReservationCancelRequestDto>>>(url);
            return Ok(data);
        }
        [HttpGet]
        public IActionResult GetReservationUpdateRequests(GetReservationsRequestsDto getDto)
        {
            string url = _url + $"PanelReservation/GetReservationUpdateRequests/{getDto.ReservationId}";
            var data = _apiHandler.GetApi<CustomResponseDto<List<ReservationCancelRequestDto>>>(url);
            return Ok(data);
        }
        [HttpGet]

        public IActionResult SendToOperation(string id)
        {
            string url = _url + $"PanelReservation/SendReservationToOperation";
            _apiHandler.PostApi<CustomResponseNullDto>(new SendReservationToOperationPostDto
            {
                ReservationId = id
            }, url);
            return RedirectToAction("ReservationList");
        }


        [HttpPost]
        public IActionResult SendPaymentLink(decimal amount, string email, Guid reservationId, int validaty = 2)
        {
            string url = _url + $"PanelReservation/ReservationPaymentSendMail";
            ReservationPaymentSendMailDto paymentSendMailDto = new()
            {
                ReservationId = reservationId,
                Amount = amount,
                Email = email,
                ValidatyPeriod = validaty,
                
            };


            _apiHandler.PostApi<CustomResponseNullDto>(paymentSendMailDto, url);

            return Json(new { url = paymentSendMailDto.Url });
        }
    }
}
