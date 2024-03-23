using Azure.Core;
using Core.Entities;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.ApiToWebDtos.Payment.Results;
using Dto.ApiWebDtos.WebToApiDtos;
using Dto.ApiWebDtos.WebToApiDtos.Payment;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Service.Payment;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace GuideWeb.Controllers
{
    public class PaymentController : CustomBaseController
    {
        private readonly ICookie _cookie;
        readonly YKConfig _yapiKrediConfig;
        readonly IHttpClientFactory _httpClientFactory;
        public PaymentController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie, IOptions<YKConfig> ykConfig, IHttpClientFactory httpClientFactory) : base(apiHandler, configuration, cookie)
        {
            _cookie = cookie;
            _yapiKrediConfig = ykConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public IActionResult ReservationGeneralInfo(ReservationFormMainInformationDto dto)
        {
            dto.StartDate = dto.StartDate == null ? DateTime.Now : dto.StartDate;
            dto.StartTimeId = dto.StartTimeId == null ? 8 : dto.StartTimeId;


            //eğer kullanıcı giriş yapmışsa onn üstünden gidelim.
            var userId = _cookie.getMemberId();
            var agentId = _cookie.getMemberAgentId();

            // kullanıcı giriş yoksa yeni bir user id oluşturup customer olarak eklemeliyim bence TODO --> Methodu/logici API ya taşı
            if (string.IsNullOrEmpty(userId))
            {

                // yeni customer oluşturalım -
                // eğer aynı mailde member varsa aynnı maille customer açıcam çünkü giriş yapmamış
                // ama aynı maille cutomer varsa o idyi kullanacağım
                // yani bu noktada bana user Id lazım aslında

                RegisterReservationUser customer = new RegisterReservationUser()
                {
                    NameSurname = dto.Fullname,
                    Email = dto.Email,
                    Phone = dto.Phone,
                };

                string urlUser = _url + "Payment/RegisterReservationUser";
                CustomResponseDto<RegisterReservationUser> newUser = _apiHandler.PostApi<CustomResponseDto<RegisterReservationUser>>(customer, urlUser);
                userId = newUser.Data.UserId.ToString(); // string yapalım

            }

            dto.UserId = new Guid(userId);
            if (!string.IsNullOrEmpty(agentId))
                dto.AgentId = new Guid(agentId);

            string url = _url + "Payment/ReservationGeneralInfo";
            CustomResponseDto<Guid> response = _apiHandler.PostApi<CustomResponseDto<Guid>>(dto, url);

            //eğer ürün fiyatı sıfır ise ask for price olarak kaydedecek. bu sebeple talebiniz alındı gibi bir sayfaya yönlendirme yapacağız 
            if (dto.ProductPrice == 0)
            {
                return RedirectToAction("AskForPriceSuccess", "Reservation");
            }
            string redirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/payment-participant/{response.Data}";

            return Redirect(redirectUrl);
        }

        [HttpGet]
        public IActionResult PaymentParticipant(string id)
        {
            int languageId = 1;
            string url = _url + "Payment/GetReservationParticipantInformationDto/" + id + "/" + languageId;
            CustomResponseDto<ReservationParticipantInformationDto> reservationParticipantInformationDto = _apiHandler.GetApi<CustomResponseDto<ReservationParticipantInformationDto>>(url);

            if (reservationParticipantInformationDto is null)
            {
                return View();
            }
            else
            {

                int pc = 1;
                foreach (var pp in reservationParticipantInformationDto.Data.Participants)
                {

                    if (pp.Name == "" && pp.Surname == "")
                    {
                        pp.Name = "Guest Name " + pc;
                        pp.Surname = "Guest Surname " + pc;
                    }
                    pc++; // sırası karışmasın diye ifin dışında artış olmalı
                }

                return View(reservationParticipantInformationDto.Data);
            }

        }

        [HttpPost]
        public IActionResult PaymentParticipant(ReservationParticipantInformationDto dto)
        {
            string url = _url + "Payment/ReservationParticipantInformation";
            CustomResponseDto<Guid> id = _apiHandler.PostApi<CustomResponseDto<Guid>>(dto, url);

            //billing info kısmını kaldıralım hiç gerek yok demiştir o sebeple kaldırdık komple
            //direk  kart bilgileri kısmına gideceğiz
            //return RedirectToAction("PaymentBilling", "Payment", new { id = id.Data });
            string redirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/payment-information/{id.Data}";

            return Redirect(redirectUrl);
        }

        [HttpGet]
        public IActionResult PaymentBilling(string id)
        {
            int languageId = 1;
            string url = _url + "Payment/GetReservationBillingInformationDto/" + id + "/" + languageId;
            CustomResponseDto<ReservationBillingInformationDto> reservationBillingInformationDto = _apiHandler.GetApi<CustomResponseDto<ReservationBillingInformationDto>>(url);

            if (reservationBillingInformationDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationBillingInformationDto.Data);
            }

        }

        [HttpPost]
        public IActionResult PaymentBilling(ReservationBillingInformationDto dto)
        {
            string url = _url + "Payment/ReservationBillingInformation";
            CustomResponseDto<Guid> id = _apiHandler.PostApi<CustomResponseDto<Guid>>(dto, url);
            string redirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/payment-information/{id.Data}";
            return Redirect(redirectUrl);
            //return RedirectToAction("PaymentInformation", "Payment", new { id = id.Data });
        }

        [HttpGet]
        public IActionResult PaymentInformation(string id)
        {
            int languageId = 1;
            string url = _url + "Payment/GetReservationPaymentInformationDto/" + id + "/" + languageId;
            CustomResponseDto<ReservationPaymentInformationDto> reservationPaymentInformationDto = _apiHandler.GetApi<CustomResponseDto<ReservationPaymentInformationDto>>(url);

            if (reservationPaymentInformationDto is null)
            {
                return View();
            }
            else
            {
                return View(reservationPaymentInformationDto.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentInformation(ReservationPaymentInformationDto dto)
        {
            dto.Year = dto.Year[2..]; //2028 --> 28
            string url = _url + "Payment/ReservationPaymentInformation";
            CustomResponseDto<PaymentInformationResultDto> cmp = _apiHandler.PostApi<CustomResponseDto<PaymentInformationResultDto>>(dto, url);
            //return RedirectToAction("PaymentSuccess", dto.ReservationId);
            ViewData["3D"] = cmp.Data.GatewayResult.HtmlFormContent;
            return View("PaymentInformationPost");
            //return Json(cmp);
        }


        [HttpGet]
        public IActionResult PaymentInquiry()
        {
            string paymentCode = Request.Query["PaymentCode"].ToString();
            int languageId = 1;
            string url = _url + "Payment/GetPaymentInquiry/" + paymentCode + "/" + languageId;
            CustomResponseDto<PaymentInquiryDto> reservationPaymentInformationDto = _apiHandler.GetApi<CustomResponseDto<PaymentInquiryDto>>(url);

            if (reservationPaymentInformationDto is null || reservationPaymentInformationDto.StatusCode == StatusCodes.Status400BadRequest)
            {
                return RedirectToRoute("NotFound");
            }
            else
            {
                return View(reservationPaymentInformationDto.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentInquiry(PaymentInquiryDto dto)
        {
            dto.Year = dto.Year[2..]; //2028 --> 28

            string url = _url + "Payment/PaymentInformation";
            CustomResponseDto<PaymentInformationResultDto> cmp = await _apiHandler.PostAsync<CustomResponseDto<PaymentInformationResultDto>>(dto, url);

            ViewData["3D"] = cmp.Data.GatewayResult.HtmlFormContent;
            return View("PaymentInformationPost");
        }

        [HttpGet]
        public IActionResult CompleteWithoutPayment(string id)
        {
            string url = _url + "Payment/CompleteWithoutPayment";
            CompleteWithoutPaymentDto dto = new CompleteWithoutPaymentDto() { ReservationId = new Guid(id) };
            CustomResponseDto<Guid> cmp = _apiHandler.PostApi<CustomResponseDto<Guid>>(dto, url);
            return RedirectToAction("PaymentSuccess", "Payment", new { id = cmp.Data });
        }

        [HttpPost]
        [Route("{language}/payment-success/{id}")]
        public async Task<IActionResult> PaymentSuccess(Guid id)
        {
            var examp = HttpContext.Request.Form;

            string encKey = "10,10,10,10,10,10,10,10";
            string terminalID = "67C22108";
            string xid = HttpContext.Request.Form["Xid"];
            string amount = HttpContext.Request.Form["Amount"];
            string currency = "TL";
            string merchantNo = HttpContext.Request.Form["MerchantId"];
            string bankdata = HttpContext.Request.Form["BankPacket"];
            string merchantData = HttpContext.Request.Form["MerchantPacket"];
            string sign = HttpContext.Request.Form["Sign"];

            string firstHash = HASH(encKey + ';' + terminalID);
            string mac = HASH(xid + ';' + amount + ';' + currency + ';' + merchantNo + ';' + firstHash);

            var httpClient = _httpClientFactory.CreateClient();

            string xmlStr = GetOosRequest(bankdata, merchantData, sign, mac);
            var httpParameters = new Dictionary<string, string>
            {
                { "xmldata", xmlStr }
            };
            var istek = new FormUrlEncodedContent(httpParameters);
            var response = await httpClient.PostAsync("https://setmpos.ykb.com/PosnetWebService/XML", new FormUrlEncodedContent(httpParameters));
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            string responseContent = await reader.ReadToEndAsync();

            bool odemeAlindi = false;

            var responseXmlDocument = new XmlDocument();
            responseXmlDocument.LoadXml(responseContent);

            if (responseXmlDocument.SelectSingleNode("posnetResponse/approved") == null || responseXmlDocument.SelectSingleNode("posnetResponse/approved").InnerText != "1")
            {
            }
            else
            {
                string xmlStrFinans = GetoosTranDataRequest(bankdata, mac);
                var httpParametersFinans = new Dictionary<string, string>();
                httpParametersFinans.Add("xmldata", xmlStrFinans);


                var responseFinans = await httpClient.PostAsync("https://setmpos.ykb.com/PosnetWebService/XML", new FormUrlEncodedContent(httpParametersFinans));
                using var stream2 = await responseFinans.Content.ReadAsStreamAsync();

                using var reader2 = new StreamReader(stream2);
                string responseContent2 = await reader2.ReadToEndAsync();

                var responseXmlDocument2 = new XmlDocument();
                responseXmlDocument2.LoadXml(responseContent2);

                if (responseContent2.Contains("<approved>1</approved>"))
                {
                    odemeAlindi = true;
                    string addpaymenturl = _url + "Payment/ReservationSuccessAddPayment/" + id + "/" + amount;
                    CustomResponseDto<string> addpayment = _apiHandler.GetApi<CustomResponseDto<string>>(addpaymenturl);
                }
            }
            int languageId = 1;
            string url = _url + "Payment/ReservationSuccess/" + id + "/" + languageId;
            CustomResponseDto<ReservationSuccessDto> reservationSuccessDto = _apiHandler.GetApi<CustomResponseDto<ReservationSuccessDto>>(url);

            if (reservationSuccessDto is null)
            {
                return View();
            }
            else
            {
                ViewBag.odemeAlindi = odemeAlindi;
                return View(reservationSuccessDto.Data);
            }
        }
        private string GetoosTranDataRequest(string bankdata, string mac)
        {
            string requestXml = $@"<?xml version=""1.0"" encoding=""ISO-8859-9""?>
                                        <posnetRequest>
                                            <mid>6700972642</mid>
                                            <tid>67C22108</tid>
                                            <oosTranData>
                                                <bankData>{bankdata}</bankData>
                                                <wpAmount>0</wpAmount>
                                                <mac>{mac}</mac>
                                            </oosTranData>
                                        </posnetRequest>";
            return requestXml;
        }
        private string GetOosRequest(string bankdata, string merchantData, string sign, string mac)
        {
            //< XID > GIT1234 </ XID >
            string requestXml = $@"<?xml version=""1.0"" encoding=""ISO-8859-9""?>
                                        <posnetRequest>
                                            <mid>6700972642</mid>
                                            <tid>67C22108</tid>
                                            <oosResolveMerchantData>
                                                <bankData>{bankdata}</bankData>
                                                <merchantData>{merchantData}</merchantData>
                                                <sign>{sign}</sign>
                                                <mac>{mac}</mac>
                                            </oosResolveMerchantData>
                                        </posnetRequest>";
            return requestXml;
        }
        [HttpPost]
        public IActionResult GetCouponDiscount(GetCouponDiscountDto dto)
        {
            string url = _url + "Payment/GetCouponDiscount";
            CustomResponseDto<GetCouponDiscountResponseDto> response = _apiHandler.PostApi<CustomResponseDto<GetCouponDiscountResponseDto>>(dto, url);
            return Json(response);

        }

        [HttpPost]
        public IActionResult RemoveCouponDiscount(RemoveCouponDiscountDto dto)
        {
            string url = _url + "Payment/RemoveCouponDiscount";
            CustomResponseDto<bool> price = _apiHandler.PostApi<CustomResponseDto<bool>>(dto, url);
            return Json(price);
        }
        private string HASH(string originalString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
