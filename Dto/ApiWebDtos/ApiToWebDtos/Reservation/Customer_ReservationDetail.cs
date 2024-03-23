using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Reservation
{
    public class Customer_ReservationDetail
    {
        public DateTime StartDate { get; set; }
        public string ReservationNumber { get; set; }
        public string TourTitle { get; set; }
        public string StartCity { get; set; }
        public int TourType { get; set; }
    }
}
