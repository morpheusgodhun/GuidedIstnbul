using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Reservation
{
    public class GetMyReservationDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<Customer_ReservationListItemDto> Reservations { get; set; }
        public List<SelectListOptionDto> CancellationReasons { get; set; }
    }
}
