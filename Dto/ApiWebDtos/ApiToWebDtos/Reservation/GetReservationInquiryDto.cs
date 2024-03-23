using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Reservation
{
    public class GetReservationInquiryDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        //public List<ReservationListItemDto> Reservations { get; set; }
    }
}
