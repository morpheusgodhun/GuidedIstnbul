using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationParticipantInformationDto
    {
        public List<ConstantValueDto>? ConstantValues { get; set; }
        public Guid ReservationId { get; set; }
        public List<ParticipantDto> Participants { get; set; }
        public string PickUpPoint { get; set; }
        public string? ReservationNote { get; set; }
    }
    public class ParticipantDto
    {
        public Guid ParticipantId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? BirthDate { get; set; }
    }
}
