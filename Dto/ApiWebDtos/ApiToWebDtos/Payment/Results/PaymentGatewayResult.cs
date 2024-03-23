using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Payment.Results
{
    public class PaymentGatewayResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string HtmlFormContent { get; set; }
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();


        public static PaymentGatewayResult Succeeded(string htmlFormContent,
            string message = null)
        {
            return new PaymentGatewayResult
            {
                Success = true,
                Message = message
            };
        }


        public static PaymentGatewayResult Succeeded(IDictionary<string, object> parameters, string htmlFormContent)
        {
            return new PaymentGatewayResult
            {
                Success = true,
                Parameters = parameters,
                HtmlFormContent = htmlFormContent,
            };

        }
    }
}
