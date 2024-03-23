using Core.Entities;
using Core.StaticClass;
using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dto.ApiWebDtos.WebToApiDtos.Reservation.PaymentReservationDetailDto;

namespace Core.IRepository
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Reservation GetReservationById(Guid reservationId);
        public Guid ReservationGeneralInfo(ReservationFormMainInformationDto dto);
        public ReservationParticipantInformationDto GetReservationParticipantInformationDto(Guid reservationId, int languageId);
        public Guid ReservationParticipantInformation(ReservationParticipantInformationDto dto);
        public ReservationBillingInformationDto GetReservationBillingInformationDto(Guid reservationId, int languageId);
        public Guid ReservationBillingInformation(ReservationBillingInformationDto dto);
        public ReservationPaymentInformationDto GetReservationPaymentInformationDto(Guid reservationId, int languageId);
        public Guid ReservationPaymentInformation(ReservationPaymentInformationDto dto);
        public Guid CompleteWithoutPayment(CompleteWithoutPaymentDto dto);
        public ReservationSuccessDto GetReservationSuccessDto(Guid reservationId, int languageId);
        public List<ReservationListDto> UncomplatedReservationList();
        public List<ReservationListDto> AskForPriceReservationList();
        public List<ReservationListDto> ReservationList();
        public List<ReservationPanelListDto> ReservationPanelList();
        public List<ReservationListDto> ComplatedReservationList();
        public void SetReservationBookingStatus(Guid reservationId, int bookingStatus);
        public ReservationFormDetailDto GetReservationFormDetailDto(Guid reservationId);
        public PaymentReservationDetailDto GetPaymentReservationDetailDto(Guid reservationId, int languageId);
        public PaymentAgentInfoDto GetPaymentAgentInfo(Guid productId, Guid agentId);
        public WebReservationListDto ReservationInquiry(string code, int languageId);
        public ReservationDetailDto ReservationDetail(Guid reservationId, int languageId);
        public Task<List<Customer_ReservationListItemDto>> MyReservationAsync(Guid userId, int languageId);
        public List<Agent_ReservationListItemDto> AgentReservations(Guid userId, int languageId);
        PaymentInquiryDto GetPaymentInquiryDto(Guid reservationId);
    }
}
