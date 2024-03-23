using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationRequestDtos
{
    public class ReservationCancelRequestDto
    {
        public string RequestId { get; set; }
        public string Reason { get; set; } //reasonid ye göre serviste doldurulacak
        public string ReasonText { get; set; }
        public string RequestStatus { get; set; }
        public string AdminAnswer { get; set; }
    }
}
