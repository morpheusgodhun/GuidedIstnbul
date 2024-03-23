using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ReservationEditRequestRepository : GenericRepository<ReservationEditRequest>, IReservationEditRequestRepository
    {
        public ReservationEditRequestRepository(Context context) : base(context)
        {
        }

        public ReservationEditRequest GetEditRequestById(Guid id)
        {
            return _context.ReservationEditRequests.Include(x => x.Reservation).ThenInclude(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        //bir rezervasyona ait editRequestler
        public List<ReservationEditRequest> GetReservationEditRequestsByReservationId(Guid reservationId)
        {
            var query1 = from requests in _context.ReservationEditRequests
                         where requests.ReservationId == reservationId
                         select requests;

            var query = _context.ReservationEditRequests.Where(x => x.ReservationId == reservationId);
            return query1.OrderByDescending(x => x.CreateDate).ToList();
        }
    }
}
