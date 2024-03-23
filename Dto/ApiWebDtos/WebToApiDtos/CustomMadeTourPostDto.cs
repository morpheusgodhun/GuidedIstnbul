using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class CustomMadeTourPostDto
    {
        public Guid? RequestId { get; set; }
        public Guid? MemberId { get; set; }
        public List<Guid> DestinationIDs { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DeperatureDate { get; set; }
        public int NumberOfAdult { get; set; }
        //public List<ChildNumberDto> ChildNumbers { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int CountryID { get; set; }
        public string CustomerPhone { get; set; }
        public Guid HowFindUsID { get; set; }
        public string CustomerNote { get; set; }
        public List<Guid> AlsoInterestedIDs { get; set; }
        public int LanguageID { get; set; }
    }
    public class ChildNumberDto
    {
        public string ChildPolicyID { get; set; }
        public int ChildCount { get; set; }
    }

    public class CustomTourOfferCustomerAnswer
    {
        public Guid RequestId { get; set; }
        public Guid OfferId { get; set; }
        public string CustomerAnswer { get; set; }
        public string Answer { get; set; }
    }
}
