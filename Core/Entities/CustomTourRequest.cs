using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomTourRequest : BaseEntity
    {

        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public int ParticipantNumber { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public int OfferStatusId { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        //[NotMapped]
        //[ForeignKey("Language")]
        //public Guid LanguageId { get; set; }
        //public SystemOption Language { get; set; }

        [ForeignKey("SystemOption")]
        public Guid FindUsId { get; set; }
        public SystemOption SystemOption { get; set; }

        public string? MailContent { get; set; }
        public string RequestNote { get; set; }

        public ICollection<Many_CustomTourRequest_Destination> Many_CustomTourRequest_Destinations { get; set; }
        public ICollection<Many_CustomTourRequest_AlsoInterested> Many_CustomTourRequest_AlsoInteresteds { get; set; }
        public ICollection<Offer> Offers { get; set; }

        [ForeignKey("Tour")]
        public Guid? TourId { get; set; }
        public Tour? Tour { get; set; }

    }
}
