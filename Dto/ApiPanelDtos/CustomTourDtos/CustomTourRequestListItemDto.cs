using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomTourDtos
{
    public class CustomTourRequestListItemDto
    {
        public Guid RequestId { get; set; }
        //public Guid? MemberId { get; set; }
        //public List<string> Destinations { get; set; }
        //public string ArrivalDate { get; set; }
        //public string DeperatureDate { get; set; }
        //public int NumberOfAldult { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        //public string SenderPhone { get; set; }
        //public string Country { get; set; }
        //public string HowFindUs { get; set; }
        //public string CustomerNote { get; set; }
        public string OfferStatus { get; set; }
        public string MailContent { get; set; }

        //public List<string> AlsoInteresteds { get; set; }
        //public string Language { get; set; }
        public decimal? Price { get; set; }
        public string Program { get; set; }
        public CustomTourRequestDetail RequestDetail { get; set; }

    }

    public class CustomTourRequestDetail
    {
        public Guid RequestId { get; set; }
        public List<string> Destinations { get; set; }
        public string ArrivalDate { get; set; }
        public string DeperatureDate { get; set; }
        public int NumberOfAldult { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string Country { get; set; }
        public string HowFindUs { get; set; }
        public string CustomerNote { get; set; }
        public List<string> AlsoInteresteds { get; set; }
        public string Language { get; set; }
        public int RequestStatus { get; set; }
        public string MailContent { get; set; }
    }
}
