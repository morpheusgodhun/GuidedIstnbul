using Dto.ApiPanelDtos.ReservationEditRequestDtos;
using Dto.ApiPanelDtos.ReservationRequestDtos;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationDtos
{
    public class ReservationListDto
    {
        public Guid ReservationId { get; set; }
        public string ReservationCode { get; set; }
        public string ProductName { get; set; }
        public string BookingStatus { get; set; }
        public string Booker { get; set; }
        public string StartDateTime { get; set; }
        public DateTime BookingTime { get; set; }

    }
    public class ReservationPanelListDto : ReservationListDto
    {
        public bool HasActiveUpdateRequest { get; set; }
        public bool HasActiveCancelRequest { get; set; }
        //public List<ReservationCancelRequestDto> CancelRequestDtos { get; set; }
        //public List<ReservationUpdateRequestDto> UpdateRequestDtos { get; set; }
    }
}
