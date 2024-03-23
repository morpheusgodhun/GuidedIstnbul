using Azure.Core;
using Dto.ApiWebDtos.ApiToWebDtos.Payment.Results;
using Dto.ApiWebDtos.WebToApiDtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
namespace Service.Payment
{
    public class YKPaymentService : IYKPaymentService
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly YKConfig _yapiKrediConfig;
        public YKPaymentService(IOptions<YKConfig> ykConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _yapiKrediConfig = ykConfig.Value;
        }
        public async Task<PaymentGatewayResult> ThreeDGatewayRequest(YKPosPaymentDto paymentDto)
        {
            var httpClient = _httpClientFactory.CreateClient();

            string xmlStr = GetOosRequest(paymentDto);
            var httpParameters = new Dictionary<string, string>
            {
                { "xmldata", xmlStr }
            };

            var response = await httpClient.PostAsync(_yapiKrediConfig.XML_SERVICE_URL, new FormUrlEncodedContent(httpParameters));

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            string responseContent = await reader.ReadToEndAsync();

            var responseXmlDocument = new XmlDocument();
            responseXmlDocument.LoadXml(responseContent);

            if (responseXmlDocument.SelectSingleNode("posnetResponse/approved") == null || responseXmlDocument.SelectSingleNode("posnetResponse/approved").InnerText != "1")
            {
                string errorMessage = responseXmlDocument.SelectSingleNode("posnetResponse/respText")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";
            }

            var data1Node = responseXmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/data1");
            var data2Node = responseXmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/data2");
            var signNode = responseXmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/sign");


            var parameters = new Dictionary<string, object>();
            parameters.Add("posnetData", data1Node.InnerText);
            parameters.Add("posnetData2", data2Node.InnerText);
            parameters.Add("digest", signNode.InnerText);

            parameters.Add("mid", _yapiKrediConfig.MERCHANT_ID);
            parameters.Add("posnetID", _yapiKrediConfig.POSNET_ID);

            //Vade Farklı işlemler için kullanılacak olan kampanya kodunu belirler.
            //Üye İşyeri için tanımlı olan kampanya kodu, İşyeri Yönetici Ekranlarına giriş yapıldıktan sonra, Üye İşyeri bilgileri sayfasından öğrenilebilinir.
            parameters.Add("vftCode", string.Empty);

            string returnUrl = _yapiKrediConfig.ReturnURL.Replace("{language}", paymentDto.ReturnUrlLanguagePrefix);
            parameters.Add("merchantReturnURL", returnUrl + paymentDto.ReservationId);//geri dönüş adresi
            parameters.Add("lang", "tr");
            parameters.Add("url", "https://localhost:7115/Payment/PaymentInformation/" + paymentDto.ReservationId + "");//openANewWindow 1 olarak ayarlanırsa buraya gidilecek url verilmeli
            parameters.Add("openANewWindow", "0");//POST edilecek formun yeni bir sayfaya mı yoksa mevcut sayfayı mı yönlendirileceği
            parameters.Add("useJokerVadaa", "0");//yapıkredi kartlarında vadaa kullanılabilirse izin verir
            //parameters.Add("actionUrl", _yapiKrediConfig.OOS_TDS_SERVICE_URL);           

            string paymentFormHtmlContent = CreatePaymentFormHtml(parameters, new Uri(_yapiKrediConfig.OOS_TDS_SERVICE_URL));

            return PaymentGatewayResult.Succeeded(parameters, paymentFormHtmlContent);
        }

        private string GetOosRequest(YKPosPaymentDto paymentDto)
        {
            string amount = (paymentDto.TotalPrice * 100m).ToString("0.##", new CultureInfo("en-US"));
            //< XID > GIT1234 </ XID >
            string requestXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <posnetRequest>
                                            <mid>{_yapiKrediConfig.MERCHANT_ID}</mid>
                                            <tid>{_yapiKrediConfig.TERMINAL_ID}</tid>
                                            <oosRequestData>
                                                <posnetid>{_yapiKrediConfig.POSNET_ID}</posnetid>
                                               <XID>{GenerateRandomCode(20)}</XID>
                                                <amount>{amount}</amount>
                                                <currencyCode>{paymentDto.Currency}</currencyCode>
                                                <installment>00</installment>
                                                <tranType>Sale</tranType>
                                                <cardHolderName>{paymentDto.CardHolderName}</cardHolderName>
                                                <ccno>{paymentDto.CardNumber}</ccno>
                                                <expDate>{paymentDto.Year}{paymentDto.Month}</expDate>
                                                <cvc>{paymentDto.CVC}</cvc>
                                            </oosRequestData>
                                        </posnetRequest>";
            return requestXml;
        }
        //yapıkredi ödemelerini bu random code ile takip edebiliriz
        string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder codeBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                codeBuilder.Append(chars[index]);
            }

            return codeBuilder.ToString();
        }

        public string CreatePaymentFormHtml(IDictionary<string, object> parameters, Uri actionUrl, bool appendSubmitScript = true)
        {
            if (parameters == null || !parameters.Any())
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (actionUrl == null)
            {
                throw new ArgumentNullException(nameof(actionUrl));
            }

            string formId = "PaymentForm";
            StringBuilder formBuilder = new StringBuilder();
            formBuilder.Append($"<!DOCTYPE html>");
            formBuilder.Append($"<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            formBuilder.Append($"<head><meta charset=\"utf-8\" /><title></title>");
            //formBuilder.Append($"<script type=\"text/javascript\" src=\"https://localhost:7115/assets/js/posnet.js\"></script>");
            //formBuilder.Append($"<script type=\"text/javascript\">");
            //formBuilder.Append($"function submitFormEx(Form, OpenNewWindowFlag, WindowName) {{submitForm(Form, OpenNewWindowFlag, WindowName)Form.submit();}}");
            //formBuilder.Append($"</script>");
            //            formBuilder.Append($"<script type=\"text/javascript\">");
            //            formBuilder.Append(@$"

            //myForm.addEventListener('submit', function (event) {{
            //        // Sayfa yenilemeyi önle (varsayılan davranışı engelle)
            //        event.preventDefault();
            //var formData = $(this).serialize();
            //$.ajax({{
            //  url: '{actionUrl}',
            //  method: 'POST',
            //  dataType: 'application/x-www-form-urlencoded',
            //data:formData
            //  success: function (data, status, xhr) {{
            //    // Başarılı yanıt işleme
            //    console.log('Başarılı:', data);
            //  }},
            //  error: function (xhr, status, error) {{
            //    // Hata durumu işleme
            //    console.error('Hata:', error);
            //  }}
            //}});


            //}});");
            //            formBuilder.Append($"</script>");
            formBuilder.Append($"</head>");
            formBuilder.Append($"<body>");
            formBuilder.Append($"<form id=\"{formId}\" name=\"{formId}\" action=\"{actionUrl}\" method=\"POST\" content=\"text/html;charset=UTF-8\">");
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                formBuilder.Append($"<input type=\"hidden\" name=\"{parameter.Key}\" value=\"{parameter.Value}\">");
            }
            formBuilder.Append("<input type=\"submit\" style=\"display:none\" name=\"Submit\" value=\"Doğrulama Yap\"  />"); //onclick=\"submitFormEx(formName, 0, 'YKBWindow')\"
            formBuilder.Append("</form>");
            formBuilder.Append("</body>");
            formBuilder.Append("</html>");
            if (appendSubmitScript)
            {
                StringBuilder scriptBuilder = new StringBuilder();
                scriptBuilder.Append("<script>");
                scriptBuilder.Append($"document.{formId}.submit();");
                scriptBuilder.Append("</script>");
                formBuilder.Append(scriptBuilder.ToString());
            }

            return formBuilder.ToString();
        }
    }

    public interface IYKPaymentService
    {
        Task<PaymentGatewayResult> ThreeDGatewayRequest(YKPosPaymentDto paymentDto);
    }
}

