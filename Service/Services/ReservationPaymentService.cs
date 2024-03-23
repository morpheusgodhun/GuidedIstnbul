using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReservationPaymentService : GenericService<ReservationPayment>, IReservationPaymentService
    {
        private readonly IReservationPaymentRepository _reservationPaymentRepository;
        public ReservationPaymentService(IGenericRepository<ReservationPayment> repository, IUnitOfWork unitOfWork, IReservationPaymentRepository reservationPaymentRepository) : base(repository, unitOfWork)
        {
            _reservationPaymentRepository = reservationPaymentRepository;
        }

       

        public List<PaymentListByReservationIdDto> PaymentListByReservationId(Guid reservationId)
        {
            return _reservationPaymentRepository.PaymentListByReservationId(reservationId);
        }
    }
}
