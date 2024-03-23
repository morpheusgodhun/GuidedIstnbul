using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class CancelReservationRequestPostDto
    {
        public string ReservationId { get; set; }
        public string CancellationReasonId { get; set; }
        public string CancellationReasonText { get; set; }
        public bool ReadCancellationPolicy { get; set; }
    }
}
