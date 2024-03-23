using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class WebReservationListDto
    {
        public Guid ReservationId { get; set; }
        public string ReservationCode { get; set; }
        public string StartDate { get; set; }
        public string ProductName { get; set; }

        public string Type { get; set; }
        public bool Cancellable { get; set; }
        public bool Updateable { get; set; }
        public bool CommentSendable { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}
