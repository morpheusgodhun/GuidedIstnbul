using Core.Entities;
using Core.IRepository;
using Dto.ApiPanelDtos.ReservationEditRequestDtos;
using Dto.ApiPanelDtos.ReservationRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums;

namespace Core.IService
{
    public interface IReservationEditRequestService : IGenericService<
        ReservationEditRequest>
    {
        List<ReservationEditRequestDto> GetReservationEditRequests(string id, ReservationEditRequestType requestType);
        void ReplyEditRequest(ReservationEditRequestReplyDto reservationReply);
    }
}
