using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Service
{

    public class AdditionalServicePriceDto
    {
        public Guid Id { get; set; } // AdditionalServiceOptionPrices id
        public Guid AdditionalServiceOptionPriceDatesId { get; set; } //AdditionalServiceOptionPriceDates id
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Priorty { get; set; }
        public int PricePerson { get; set; } //eğer isperson ise ilk fiyatı alıp çarpıyıruz
        public int PricePersonEnd { get; set; } // isgroup ise burdaki başla bitişleri sorgulayacağız
        public decimal Price { get; set; }
    }
}

//Bu iki yapı birbirinin aynısı -> tourpriceDto ve AdditionalServicePriceDto
//TourPriceItems - AdditionalServiceOptionPrices
//AdditionalServiceOptionPriceDates - TourPrices 
//
