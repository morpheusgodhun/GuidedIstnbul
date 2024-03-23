using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class AddTourFaqDto
    {
        public Guid ProductID { get; set; }
        public int Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
