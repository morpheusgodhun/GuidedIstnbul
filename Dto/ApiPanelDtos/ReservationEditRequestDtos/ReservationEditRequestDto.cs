using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationEditRequestDtos
{
    //cancel ve updatei de kapsar 
    public class ReservationEditRequestDto
    {
        public Guid ReservationId { get; set; }
        public string RequestId { get; set; }
        public string ReservationCode { get; set; }
        public Guid? ReasonId { get; set; } //dropdowndan seçilen reasonid
        public string? Reason { get; set; } //reasonid ye göre serviste doldurulacak
        public string ReasonText { get; set; }
        public int RequestStatus { get; set; }
        public int RequestType { get; set; }
        public string AdminAnswer { get; set; }
    }
}
