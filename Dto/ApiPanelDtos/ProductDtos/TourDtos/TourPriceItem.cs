using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class TourPriceItem
    {
        public decimal Price { get; set; }
        public Guid? PersonPolicyID { get; set; }
    }
}
