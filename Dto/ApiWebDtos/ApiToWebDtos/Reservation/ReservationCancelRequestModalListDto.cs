using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Reservation
{
    public class ReservationCancelRequestModalListDto
    {
        public string RequestId { get; set; }
        public string Reason { get; set; }
        public string RequestStatus { get; set; }
        public string AdminAnswer { get; set; }
    }
}
