using Core.Entities;
using Dto.ApiPanelDtos.ReservationDtos;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.ApiToWebDtos.Reservation;
using Dto.ApiWebDtos.WebToApiDtos.Reservation;

namespace Core.IService
{
    public interface IReservationService : IGenericService<Reservation>
    {
        public Guid ReservationGeneralInfo(ReservationFormMainInformationDto dto);
        public RegisterReservationUser RegisterReservationUser(RegisterReservationUser dto);
        public ReservationParticipantInformationDto GetReservationParticipantInformationDto(Guid reservationId, int languageId);
        public Guid ReservationParticipantInformation(ReservationParticipantInformationDto dto);
        public ReservationBillingInformationDto GetReservationBillingInformationDto(Guid reservationId, int languageId);
        public Guid ReservationBillingInformation(ReservationBillingInformationDto dto);
        public ReservationPaymentInformationDto GetReservationPaymentInformationDto(Guid reservationId, int languageId);
        public Guid ReservationPaymentInformation(ReservationPaymentInformationDto dto);
        public Guid CompleteWithoutPayment(CompleteWithoutPaymentDto dto);
        public string ReservationSuccessAddPayment(string id, decimal amount);
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
        void CancelReservationRequest(CancelReservationRequestPostDto dto);
        void UpdateReservationRequest(UpdateReservationRequestPostDto dto);
        void SendReservationToOperation(string reservationId);
        Task<List<Agent_ReservationListItemDto>> AgentReservationsAsync(Guid agentId);
        void ReservationPaymentSendMail(ReservationPaymentSendMailDto paymentSendMail);
        PaymentInquiryDto GetPaymentInquiryDto(Guid reservationId);
    }
}
