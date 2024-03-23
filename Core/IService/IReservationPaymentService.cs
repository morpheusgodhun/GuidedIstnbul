using Core.Entities;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IReservationPaymentService : IGenericService<ReservationPayment>
    {
        List<PaymentListByReservationIdDto> PaymentListByReservationId(Guid reservationId);
    }
}
