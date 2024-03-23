using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomTourDtos
{
    public class OfferListDto
    {
        public Guid OfferId { get; set; }
        public string Program { get; set; }
        public decimal Price { get; set; }
        public string OfferStatus { get; set; }
        public string? Answer { get; set; }
        public DateTime Date { get; set; }
        public string? AdminAnswer { get; set; }
        public string? CustomerAnswer { get; set; }
        public CustomTourRequestDetail RequestDetail { get; set; }
    }

}
