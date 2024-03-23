using DocumentFormat.OpenXml.Spreadsheet;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using DTO;
using GuidePanel.APIHandler;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GuidePanel.ViewComponents.ReservationPayments
{
    public class ReservationPaymentSendMailViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;
        public ReservationPaymentSendMailViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"]; 
        }

        [HttpPost]
        public async Task<IViewComponentResult> InvokeAsync(ReservationPaymentSendMailDto paymentSendMailDto)
        {
            string url = _url + $"PanelReservation/ReservationPaymentSendMail";

            //var encodedAmount = HttpUtility.UrlEncode(amount);

            //ReservationPaymentSendMailDto paymentSendMailDto = new()
            //{
            //    ReservationId = reservationId,
            //    Amount = amount,
            //    Email = email,
            //    ValidatyPeriod = validaty
            //};

            _apiHandler.PostApi<CustomResponseNullDto>(paymentSendMailDto, url);
            return View(new { url = paymentSendMailDto.Url });
            //return View();
        }
    }
}
