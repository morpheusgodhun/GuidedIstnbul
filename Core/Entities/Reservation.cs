using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Reservation : BaseEntity
    {
        public string ReservationCode { get; set; }
        public int BookingStatus { get; set; }
        public DateTime StartDate { get; set; }
        public int StartTimeId { get; set; }
        public int ParticipantNumber { get; set; }
        //Contact Person
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //Contact Person End
        public string Hotel { get; set; }
        public string? AlsoInterestedNote { get; set; }
        public string? PickUpPoint { get; set; }
        public string? ReservationNote { get; set; }
        public int? SelectedDayCount { get; set; } //kaç günlük seçti

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<Many_Reservation_AdditionalServiceOption> Many_Reservation_AdditionalServiceOptions { get; set; }

        [ForeignKey("ReservationBillingInfo")]
        public Guid? ReservationBillingInfoId { get; set; }
        public ReservationBillingInfo? ReservationBillingInfo { get; set; }

        [ForeignKey("FindUs")]
        public Guid? FindUsId { get; set; }
        public SystemOption FindUs { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositoPrice { get; set; }
        public Discount? Discount { get; set; }
        [ForeignKey("Discount")]
        public Guid? DiscountId { get; set; }
        public ICollection<ReservationParticipant> ReservationParticipants
        { get; set; }

        public ICollection<Many_ReservationForm_AlsoInterested> Many_ReservationForm_AlsoInteresteds { get; set; }

        public ICollection<ReservationPayment> ReservationPayments { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public Guid? AgentId { get; set; } //rezervasyonu yapan kullanıcı agent ise agentId - rezervasyonu yapan kullanıcı bir agentın alt kullanıcısı ise bağlı olduğu agentId 
        public Agent? Agent { get; set; }
        public Reservation()
        {
            ReservationPayments = new List<ReservationPayment>();
            Many_ReservationForm_AlsoInteresteds = new List<Many_ReservationForm_AlsoInterested>();
            ReservationPayments = new List<ReservationPayment>();
            ReservationParticipants = new List<ReservationParticipant>();
            //ReservationBillingInfo = new ReservationBillingInfo();
            Many_Reservation_AdditionalServiceOptions = new List<Many_Reservation_AdditionalServiceOption>();
        }
    }
}
