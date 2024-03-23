using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomTourDtos
{
    public class AddOfferDto
    {
        public Guid CustomTourRequestId { get; set; }
        public string Program { get; set; }
        public string AdminAnswer { get; set; }
        public decimal Price { get; set; }
    }
}
