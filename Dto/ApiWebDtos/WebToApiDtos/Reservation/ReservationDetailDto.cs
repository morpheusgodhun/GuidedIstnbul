using Dto.ApiPanelDtos.UserDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationDetailDto
    {
        public string ReservationCode { get; set; }
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<ParticipantDto> Participants { get; set; }
        public decimal TotalPrice { get; set; }
        public string StartDate { get; set; }
        public List<TourDetailPlan> Plan { get; set; }
        //public string AlsoInterestedNote { get; set; }
        public string? PickUpPoint { get; set; }
        public string? ReservationNote { get; set; }
        public string ProductName { get; set; }
        public decimal TourPrice { get; set; }
        public int BookingStatus { get; set; }
        public bool IsTour { get; set; }
        public List<ReservationSuccessAdditionalServiceDto> AdditionalServices { get; set; }
        public UserDto User { get; set; }
    }
}
