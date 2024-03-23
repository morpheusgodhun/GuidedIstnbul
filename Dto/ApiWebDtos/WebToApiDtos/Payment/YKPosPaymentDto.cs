using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Payment
{
    public class YKPosPaymentDto
    {
        public YKPosPaymentDto(string cardHolderName, string cardNumber, decimal totalPrice, string month, string year, string cVC, string currency, string reservationId, string languagePrefix)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            TotalPrice = totalPrice;
            Month = month;
            Year = year;
            CVC = cVC;
            Currency = currency;
            ReservationId = reservationId;
            ReturnUrlLanguagePrefix = languagePrefix;
        }
        public YKPosPaymentDto()
        {

        }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string CVC { get; set; } //cvc
        public string Currency { get; set; }
        public string ReservationId { get; set; }
        public string ReturnUrlLanguagePrefix { get; set; }
    }
}
