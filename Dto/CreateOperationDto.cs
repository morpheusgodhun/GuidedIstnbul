using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CreateOperationDto
    {
        public CreateOperationDto(Guid reservationId)
        {
            ReservationId = reservationId;
        }
        public Guid ReservationId { get; set; }
    }
}
