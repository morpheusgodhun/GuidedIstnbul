using Dto.ApiWebDtos.ApiToWebDtos.Payment.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Payment
{
    public class PaymentInformationResultDto
    {
        public PaymentInformationResultDto(Guid reservationId, PaymentGatewayResult gatewayResult)
        {
            ReservationId = reservationId;
            GatewayResult = gatewayResult;
        }
        public PaymentInformationResultDto()
        {

        }
        public Guid ReservationId { get; set; }
        public PaymentGatewayResult GatewayResult { get; set; }
    }
}
