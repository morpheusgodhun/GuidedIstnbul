using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationPaymentInformationDto
    {
        public List<ConstantValueDto>? ConstantValues { get; set; }
        public Guid ReservationId { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? DepositoPrice { get; set; }
        public decimal? LastTotalPrice { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string SecurityCode { get; set; } //cvc
        public bool IsDeposit { get; set; }
    }

    public class CompleteWithoutPaymentDto
    {
        public Guid ReservationId { get; set; }
    }

    public class GetCouponDiscountDto
    {
        public Guid Reservation { get; set; }
        public string CouponCode { get; set; }
    }
    public class GetCouponDiscountResponseDto
    {
        public int DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public PaymentShowDetailDto PaymentDetail  { get; set; }
    }
    public class RemoveCouponDiscountDto
    {
        public Guid ReservationId { get; set; }
        public string CouponCode { get; set; }
    }
}
