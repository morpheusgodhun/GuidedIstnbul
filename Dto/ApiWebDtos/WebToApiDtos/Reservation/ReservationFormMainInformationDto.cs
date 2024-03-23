using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationFormMainInformationDto
    {
        public Guid? ProductId { get; set; }
        public DateTime? StartDate { get; set; }
        public int? StartTimeId { get; set; }
        public List<ReservationAdditionalServiceDto>? AdditionalServiceOptions { get; set; }
        public int? ParticipantNumber { get; set; }
        public Guid? FindUsId { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Hotel { get; set; }
        public List<Guid>? AlsoInterestedIDs { get; set; }
        public string? AlsoInterestedNote { get; set; }
        public Guid? UserId { get; set; } //rezervasyoonu bir user yaptıysa bunu dolduralım - yok değilse customer oluşturuyorum
        public Guid? AgentId { get; set; } //rezervasyonu bir user yaptıysa veya rezervasyonun yapan kişi bir agenta bağlı ise dolduralım
        public decimal? TotalPrice { get; set; }
        public decimal? ProductPrice { get; set; }
        public int DurationSelect { get; set; }
        public string? LanguagePrefix { get; set; }
        public ReservationFormMainInformationDto()
        {
            AlsoInterestedIDs = new List<Guid>();
            AdditionalServiceOptions = new List<ReservationAdditionalServiceDto>();
        }
    }


}
