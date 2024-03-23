using Dto.ApiPanelDtos.ReservationRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Reservation
{
    public class Customer_ReservationListItemDto
    {
        public string ReservationID { get; set; }
        public bool IsCommentSendable { get; set; }
        public bool IsPayable { get; set; }
        public bool IsCancelable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool HasUpdateRequest { get; set; } = false;
        public bool HasCancelRequest { get; set; } = false;
        public int ReservationType { get; set; }  // 1 = BestSeller - 2 = Private - 3 = Custom - 4 = Service
        public int ReservationStatus { get; set; }  // 1 = Incomplate - 2 = Next - 3 = Past
        public string ReservationNumber { get; set; }
        public decimal ReservationPrice { get; set; }
        public DateTime Date { get; set; }
        public List<ReservationCancelRequestDto>? EditRequests { get; set; }

    }
}
