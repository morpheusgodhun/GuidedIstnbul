using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class BookTourPostDto
    {
        public string TourID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DeperatureDate { get; set; }
        public List<int> LangaugeIDs { get; set; }
        public int ParticipantCount { get; set; }
        public string HowDidFindID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HotelCruiseName { get; set; }
        public List<string> AlsoInterestedIDs { get; set; }
        public string ReservationNote { get; set; }
    }
}
