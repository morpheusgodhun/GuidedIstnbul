using Core.Entities;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IReservationPaymentRepository : IGenericRepository<ReservationPayment>
    {
        List<PaymentListByReservationIdDto> PaymentListByReservationId(Guid reservationId);
    }
}
