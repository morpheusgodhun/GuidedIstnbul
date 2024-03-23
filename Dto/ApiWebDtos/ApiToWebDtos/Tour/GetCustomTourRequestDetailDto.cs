using Dto.ApiPanelDtos.CustomTourDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class GetCustomTourRequestDetailDto
    {
        public CustomTourRequestDetail RequestDetail { get; set; }
        public List<CustomTourOfferListDto> OfferList { get; set; }
    }

    public class CustomTourOfferListDto
    {
        public Guid requestId { get; set; }
        public Guid OfferId { get; set; }
        public string? Answer { get; set; }
        public DateTime Date { get; set; }
        public string Program { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public string OfferStatus { get; set; } // bu aslında request status olmalı
        public string AdminAnswer { get; set; }
        public string CustomerAnswer { get; set; }
    }
}