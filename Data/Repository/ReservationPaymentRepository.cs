using Core.Entities;
using Core.IRepository;
using Core.StaticValues;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ReservationPaymentRepository : GenericRepository<ReservationPayment>, IReservationPaymentRepository
    {
        //Context _context;


        DbSet<ReservationPayment> _reservationPayments;
        public ReservationPaymentRepository(Context context) : base(context)
        {
            //_context = context;
            
            _reservationPayments = _context.ReservationPayments;
        }

       

        public List<PaymentListByReservationIdDto> PaymentListByReservationId(Guid reservationId)
        {
            var a = _reservationPayments.ToList();
            List<PaymentListByReservationIdDto> paymentListByReservationIdDtos = new(from payment in _reservationPayments.ToList()
                                                                                     where payment.ReservationId == reservationId
                                                                                     && payment.Status && !payment.IsDeleted
                                                                                     select new PaymentListByReservationIdDto
                                                                                     {
                                                                                         Id = payment.Id,
                                                                                         CardHolderName = payment.CardHolderName,
                                                                                         CardNumber = payment.CardNumber,
                                                                                         CreateDate = payment.CreateDate,
                                                                                         DebtType = PaymentDebtTypeList.GetValue(payment.DebtTypeId),
                                                                                         DiscountRate = payment.DiscountRate,
                                                                                         PaymentMethod = PaymentMethodList.GetValue(payment.PaymentMethodId),
                                                                                         Price = payment.Price,
                                                                                         IsDebt = payment.IsDebt,
                                                                                         PaymentType = PaymentTypeList.GetValue(payment.PaymentTypeId),
                                                                                         UpdateDate = payment.UpdateDate,
                                                                                         PaymentTypeId = payment.PaymentTypeId,
                                                                                         PaymentMethodId = payment.PaymentMethodId,
                                                                                         DebtTypeId = payment.DebtTypeId,
                                                                                     });
            return paymentListByReservationIdDtos;
        }
    }
}
