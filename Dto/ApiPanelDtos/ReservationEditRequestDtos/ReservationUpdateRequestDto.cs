using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationEditRequestDtos
{
    public class ReservationUpdateRequestDto
    {
        public string RequestId { get; set; }
        public string ReservationCode { get; set; }
        public string Reason { get; set; }
        public string AdminAnswer { get; set; }
        public string RequestStatus { get; set; }
    }
}
