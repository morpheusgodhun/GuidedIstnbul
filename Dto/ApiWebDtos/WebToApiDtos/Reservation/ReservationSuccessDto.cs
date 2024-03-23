using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationSuccessDto
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
        public int ParticipantNumber { get; set; }
        public bool IsTour { get; set; }
        public bool IsDaysCanChange { get; set; }
        public List<ReservationSuccessAdditionalServiceDto> AdditionalServices { get; set; }

    }
    public class ReservationSuccessAdditionalServiceDto
    {
        public string AdditionalServiceName { get; set; }
        public string OptionName { get; set; }
        public decimal Price { get; set; }
        public int ParticipantNumber { get; set; }
    }
}
