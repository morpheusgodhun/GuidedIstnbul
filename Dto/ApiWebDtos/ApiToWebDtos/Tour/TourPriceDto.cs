using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class TourPriceDto
    {
        /*public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Priorty { get; set; }
        public decimal Price { get; set; }
        */

        public Guid Id { get; set; } // turpirceitem id
        public Guid TourPriceId { get; set; } //turpirce id
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Priorty { get; set; }
        public int PricePerson { get; set; } //eğer isperson ise ilk fiyatı alıp çarpıyıruz
        public int PricePersonEnd { get; set; } // isgroup ise burdaki başla bitişleri sorgulayacağız
        public decimal Price { get; set; } //
        //public bool IsPerPerson { get; set; } //true ise perpeson demek olacak yani
        //public bool IsPerDay { get; set; } // true ise perday olacak day ile çarpacağız

    }
}
