using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationPaymentDtos
{
    public class ReservationPaymentSendMailDto
    {
        public Guid ReservationId { get; set; }
        public string Email { get; set; }
        public decimal? Amount { get; set; }
        public string? Url { get; set; }
        public double ValidatyPeriod { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
