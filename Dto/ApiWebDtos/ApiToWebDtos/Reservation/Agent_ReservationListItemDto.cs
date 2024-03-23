﻿using Dto.ApiPanelDtos.ReservationRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Reservation
{
    public class Agent_ReservationListItemDto
    {
        public string ReservationID { get; set; }
        public string Username { get; set; }
        public int ReservationType { get; set; }  // 1 = BestSeller - 2 = Private - 3 = Custom - 4 = Service
        public string ReservationStatus { get; set; }  // 1 = Incomplate - 2 = Next - 3 = Past
        public string ReservationCode { get; set; }
        public decimal ReservationPrice { get; set; }
        public DateTime Date { get; set; }
        public List<ReservationCancelRequestDto>? EditRequests { get; set; }
    }
}
