using Core;
using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Data.Migrations;
using Dto.ApiPanelDtos.ReservationEditRequestDtos;
using Dto.ApiPanelDtos.ReservationRequestDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Service.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums;
using static Core.StaticValues.ReservationEditRequestStatus;

namespace Service.Services
{
    public class ReservationEditRequestService : GenericService<ReservationEditRequest>, IReservationEditRequestService
    {
        readonly IReservationEditRequestRepository _reservationEditRequestRepository;
        readonly ISystemOptionService _systemOptionService;
        readonly IMailSenderService _mailSender;
        readonly ISendMailTemplateService _sendMailTemplateService;
        readonly ISendMailService _sendMailService;
        public ReservationEditRequestService(IGenericRepository<ReservationEditRequest> repository, IUnitOfWork unitOfWork, IReservationEditRequestRepository reservationEditRequestRepository, ISystemOptionService systemOptionService, IMailSenderService mailSender, ISendMailTemplateService sendMailTemplateService, ISendMailService sendMailService) : base(repository, unitOfWork)
        {
            _reservationEditRequestRepository = reservationEditRequestRepository;
            _systemOptionService = systemOptionService;
            _mailSender = mailSender;
            _sendMailTemplateService = sendMailTemplateService;
            _sendMailService = sendMailService;
        }

        public List<ReservationEditRequestDto> GetReservationEditRequests(string id, ReservationEditRequestType requestType)
        {
            var reservationId = Guid.Parse(id);
            var data = _reservationEditRequestRepository.GetReservationEditRequestsByReservationId(reservationId);
            List<ReservationEditRequestDto> list = data.Where(x => x.RequestType == (int)requestType).Select(x => new ReservationEditRequestDto
            {
                ReservationId = x.ReservationId,
                RequestId = x.Id.ToString(),
                AdminAnswer = x.AdminAnswer,
                ReasonId = x.ReasonId,
                Reason = x.ReasonId != null ? _systemOptionService.GetSystemOptionById((Guid)x.ReasonId, 1).SystemOptionLanguageItems.FirstOrDefault().Name : null,
                ReasonText = x.Reason,
                RequestStatus = x.RequestStatus,
                RequestType = x.RequestType
            }).ToList();

            return list;
        }
        //alttaki koda bakan sövmesin lütfen
        public void ReplyEditRequest(ReservationEditRequestReplyDto editRequestReply)
        {
            ReservationEditRequest editRequest = _reservationEditRequestRepository.GetEditRequestById(Guid.Parse(editRequestReply.RequestId));
            editRequest.AdminAnswer = editRequestReply.Answer;
            editRequest.RequestStatus = editRequestReply.ApproveStatus ? (int)ReservationEditRequestStatusEnum.Onaylandi : (int)ReservationEditRequestStatusEnum.Reddedildi;

            ReservationEditRequestType requestType = (ReservationEditRequestType)Enum.ToObject(typeof(ReservationEditRequestType), editRequest.RequestType);

            if (requestType == ReservationEditRequestType.Update)
                HandleUpdateRequestMails(editRequest, editRequestReply);

            else if (requestType == ReservationEditRequestType.Cancel)
                HandleCancelRequestMails(editRequest, editRequestReply);

            _reservationEditRequestRepository.Update(editRequest);
            _unitOfWork.saveChanges();
        }
        ///replyEditRequest ve alttaki iki handleMail methodları için summary
        /// <summary>
        /// requestTypelarına göre iki methoda böldüm.
        /// alttaki iki method isteğin onaylanma durumunu kontrol ederek gerekli templateleri hazırlayıp schedule işlemine gönderiyor.
        /// </summary>
        /// <param name="editRequest"></param>
        /// <param name="editRequestReply"></param>
        void HandleUpdateRequestMails(ReservationEditRequest editRequest, ReservationEditRequestReplyDto editRequestReply)
        {
            Reservation reservation = editRequest.Reservation;
            User reservationUser = reservation.User;
            if (editRequestReply.ApproveStatus)
            {
                #region customer/member mails
                if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID)
                {
                    SendMailTemplateDto memberTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiApprovedMember);

                    MailPlaceholderUtil.ReplaceMailContent(memberTemplate, new(name: reservationUser.Name, surname: reservationUser.Surname, reservationCode: reservation.ReservationCode, updateReason: editRequest.Reason, requestDate: editRequest.CreateDate.ToShortDateString()));
                    _mailSender.ScheduleMailForSent(new()
                    {
                        Subject = memberTemplate.Subject,
                        Content = memberTemplate.Content,
                        Email = reservationUser.Email,
                        SendTime = DateTime.Now.AddMinutes(5)
                    });
                }
                else if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID)
                {
                    SendMailTemplateDto customerTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiApprovedCustomer);

                    MailPlaceholderUtil.ReplaceMailContent(customerTemplate, new(guestName: $"{reservationUser.Name} {reservationUser.Surname}", referenceNumber: reservation.ReservationCode, link: ""));
                    _mailSender.ScheduleMailForSent(new()
                    {
                        Content = customerTemplate.Content,
                        Subject = customerTemplate.Subject,
                        Email = reservationUser.Email,
                        SendTime = DateTime.Now.AddMinutes(5)
                    });
                }
                #endregion
                //todo -> diğer roller de gelecek, templateleri girilmemiş
            }
            else
            {
                #region customer/member mails
                if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID)
                {
                    SendMailTemplateDto memberTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiRejectedMember);

                    MailPlaceholderUtil.ReplaceMailContent(memberTemplate, new(name: reservationUser.Name, surname: reservationUser.Surname, reservationCode: reservation.ReservationCode, updateReason: editRequest.Reason, requestDate: editRequest.CreateDate.ToShortDateString(), adminAnswer: editRequestReply.Answer));

                    _mailSender.ScheduleMailForSent(new()
                    {
                        Subject = memberTemplate.Subject,
                        Content = memberTemplate.Content,
                        Email = reservationUser.Email,
                        SendTime = DateTime.Now.AddMinutes(5)
                    });
                }
                else if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID)
                {
                    SendMailTemplateDto customerTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonGuncellemeTalebiRejectedCustomer);

                    MailPlaceholderUtil.ReplaceMailContent(customerTemplate, new(customerName: $"{reservationUser.Name} {reservationUser.Surname}", referenceNumber: reservation.ReservationCode, link: "")); //TODO -> LİNK GÖM

                    _mailSender.ScheduleMailForSent(new()
                    {
                        Subject = customerTemplate.Subject,
                        Content = customerTemplate.Content,
                        Email = reservationUser.Email,
                        SendTime = DateTime.Now.AddMinutes(5)
                    });
                }
                #endregion
            }
        }
        void HandleCancelRequestMails(ReservationEditRequest editRequest, ReservationEditRequestReplyDto editRequestReply)
        {
            Reservation reservation = editRequest.Reservation;
            User reservationUser = reservation.User;
            var reasonItem = editRequest.ReasonId != null ? _systemOptionService.GetSystemOptionById(Guid.Parse(editRequest.ReasonId.ToString()), 1).SystemOptionLanguageItems.FirstOrDefault().Name : null;

            if (editRequestReply.ApproveStatus) //onaylandı
            {
                #region customer/member mails 
                //customer ve member templateleri aynı 
                SendMailTemplateDto memberTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiApprovedCustomer);

                MailPlaceholderUtil.ReplaceMailContent(memberTemplate, new(name: reservationUser.Name, surname: reservationUser.Surname, reservationCode: reservation.ReservationCode, cancellationReasonSelectListItem: reasonItem, cancellationReasonText: editRequest.Reason, date: editRequest.CreateDate.ToShortDateString()));
                _mailSender.ScheduleMailForSent(new()
                {
                    Subject = memberTemplate.Subject,
                    Content = memberTemplate.Content,
                    Email = reservationUser.Email,
                    SendTime = DateTime.Now.AddMinutes(5)
                });
                _sendMailService.CancelScheduledUserMails(reservationUser); //zamanlanmış maillerin statusu false
                #endregion
                //todo -> diğer roller de gelecek, templateleri girilmemiş
            }
            else //reddedildi
            {
                #region customer/member mails
                if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Member).OptionID)
                {
                    SendMailTemplateDto memberTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiRejectedMember);

                    MailPlaceholderUtil.ReplaceMailContent(memberTemplate, new(name: reservationUser.Name, surname: reservationUser.Surname, reservationCode: reservation.ReservationCode, cancellationReasonSelectListItem: reasonItem, cancellationReasonText: editRequest.Reason, date: editRequest.CreateDate.ToShortDateString(), adminAnswer: editRequestReply.Answer));

                    _mailSender.ScheduleMailForSent(new()
                    {
                        Subject = memberTemplate.Subject,
                        Content = memberTemplate.Content,
                        Email = reservationUser.Email,
                        SendTime = DateTime.Now.AddMinutes(5)
                    });
                }
                else if (reservationUser.RoleTemplateId == ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Customer).OptionID)
                {
                    SendMailTemplateDto customerTemplate = _sendMailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.RezervasyonIptalTalebiRejectedCustomer);

                    MailPlaceholderUtil.ReplaceMailContent(customerTemplate, new(guestName: $"{reservationUser.Name} {reservationUser.Surname}", referenceNumber: reservation.ReservationCode, link: "")); //TODO -> LİNK GÖM

                    _mailSender.ScheduleMailForSent(new()
                    {
                        Subject = customerTemplate.Subject,
                        Content = customerTemplate.Content,
                        Email = reservationUser.Email,
                        SendTime = DateTime.Now.AddMinutes(5)
                    });
                }
                #endregion
            }
        }
    }


}

