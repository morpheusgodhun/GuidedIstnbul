using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class AddTourSellLimitDto
    {
        public Guid ProductID { get; set; }
        public DateTime Date { get; set; }
        public int SellLimit { get; set; }
    }
}
