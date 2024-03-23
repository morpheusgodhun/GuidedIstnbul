
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class TourPriceListDto
    {
        public Guid PriceID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? Priority { get; set; }
        public List<TourPriceItem> Prices { get; set; }
    }
}
