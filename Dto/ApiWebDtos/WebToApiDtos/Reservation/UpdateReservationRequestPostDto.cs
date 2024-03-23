using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class UpdateReservationRequestPostDto
    {
        public string ReservationId { get; set; }

        public string UpdateReasonText { get; set; }
    }
}
