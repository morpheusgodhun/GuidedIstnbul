using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationDtos
{
    public class ReservationFormDetailDto
    {
        public Guid ReservationId { get; set; }
        public string ReservationCode { get; set; }
        public int BookingStatus { get; set; }
        public string ProductName { get; set; }
        public string StartDateTime { get; set; }
        public int ParticipantNumber { get; set; }
        public string FindUs { get; set; }
        public string ContactFullname { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Hotel { get; set; }
        public List<string> AlsoInterested { get; set; }
        public string AlsoInterestedNote { get; set; }
        public List<ReservationFormDetailAdditionalServiceDto> AdditionalServices { get; set; }
        public List<ParticipantDto> Participants { get; set; }
        public string ReservationNote { get; set; }
        public string PickUpPoint { get; set; }
        public ReservationFormDetailBillingInfoDto BillingInfo { get; set; }
        public List<PaymentListByReservationIdDto> ReservationPayments{ get; set; }
    }

    public class ReservationFormDetailAdditionalServiceDto
    {
        public string AdditionalServiceName { get; set; }
        public string AdditionalServiceOptionName { get; set; }
        public int ParcitipantNumber { get; set; }
        public decimal Price { get; set; }
    }
    public class ReservationFormDetailBillingInfoDto
    {
        public bool IsIndividual { get; set; } // Else Corparate
        public string? Fullname { get; set; }  //For Just Individual
        public string? TcOrPassportNo { get; set; }  //For Just Individual
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? TaxNumber { get; set; } //For Just Corporate
        public string? TaxAdministration { get; set; }//For Just Corporate
    }
}
