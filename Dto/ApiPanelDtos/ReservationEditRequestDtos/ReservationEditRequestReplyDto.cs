using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ReservationEditRequestDtos
{
    public class ReservationEditRequestReplyDto
    {
        public string ReservationId { get; set; }
        public string RequestId { get; set; }
        public string? Answer { get; set; }
        public bool ApproveStatus { get; set; }
    }
}
