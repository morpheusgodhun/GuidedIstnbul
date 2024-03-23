using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IReservationEditRequestRepository : IGenericRepository<ReservationEditRequest>
    {
        List<ReservationEditRequest> GetReservationEditRequestsByReservationId(Guid reservationId);
        ReservationEditRequest GetEditRequestById(Guid id);
    }
}
