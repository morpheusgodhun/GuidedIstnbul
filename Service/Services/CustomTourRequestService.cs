using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Data.Migrations;
using Data.Repository;
using Dto.ApiPanelDtos.CustomTourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Home;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;
using Service.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CustomTourRequestService : GenericService<CustomTourRequest>, ICustomTourRequestService
    {
        private readonly ICustomTourRequestRepository _customTourRequestRepository;
        private readonly IMailSenderService _mailSenderService;
        private readonly ISendMailTemplateService _mailTemplateService;
        private readonly IUserService _userService;
        private readonly IMany_CustomTourRequest_DestinationRepository _many_customTourRequestDestinationRepository;

        public CustomTourRequestService(IGenericRepository<CustomTourRequest> repository, IUnitOfWork unitOfWork, ICustomTourRequestRepository customTourRequestRepository, IMailSenderService mailSenderService, ISendMailTemplateService mailTemplateService, IUserService userService, IMany_CustomTourRequest_DestinationRepository many_customTourRequestDestinationRepository) : base(repository, unitOfWork)
        {
            _customTourRequestRepository = customTourRequestRepository;
            _mailSenderService = mailSenderService;
            _mailTemplateService = mailTemplateService;
            _userService = userService;
            _many_customTourRequestDestinationRepository = many_customTourRequestDestinationRepository;
        }

        public void AddOffer(AddOfferDto addOfferDto)
        {
            _customTourRequestRepository.AddOffer(addOfferDto);
            _unitOfWork.saveChanges();
        }

        public CustomMadeTourPostDto AddRequest(CustomMadeTourPostDto customTour)
        {
            _customTourRequestRepository.AddRequest(customTour);

            HandleCustomTourRequestMails(customTour);
            _unitOfWork.saveChanges();
            return customTour;
        }
        void HandleCustomTourRequestMails(CustomMadeTourPostDto customTour)
        {
            List<string> requestDestinations = _many_customTourRequestDestinationRepository.GetDestinationsByRequestId((Guid)customTour.RequestId).Select(x => x.DestinationName).ToList();
            string destinationsString = string.Join(',', requestDestinations);

            var operationTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.CustomTurTalebiReceivedOperation);

            #region Operation
            MailPlaceholderUtil.ReplaceMailContent(
                template: operationTemplate,
                new(name: customTour.CustomerName,
                email: customTour.CustomerEmail,
                phone: customTour.CustomerPhone,
                arrivalDate: customTour.ArrivalDate.ToShortDateString(),
                departureDate: customTour.DeperatureDate.ToShortDateString(),
                destination: destinationsString));

            _mailSenderService.ScheduleMailForSent(new()
            {
                Content = operationTemplate.Content,
                Subject = operationTemplate.Subject,
                Email = SendMailAddressConstants.OperationMail,
                SendTime = DateTime.Now.AddMinutes(5),
                IsSended = false
            });
            #endregion
            #region Manager

            var managerTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.CustomTurTalebiReceivedManager);
            MailPlaceholderUtil.ReplaceMailContent(
                template: managerTemplate,
                new(name: customTour.CustomerName,
                email: customTour.CustomerEmail,
                phone: customTour.CustomerPhone,
                arrivalDate: customTour.ArrivalDate.ToShortDateString(),
                departureDate: customTour.DeperatureDate.ToShortDateString(),
                destination: destinationsString,
                selectCountry: CountryList.GetValue(customTour.CountryID)));

            SendMail managerMail = new()
            {
                Content = managerTemplate.Content,
                Subject = managerTemplate.Subject,
                Email = SendMailAddressConstants.ManagerMail, //
                SendTime = DateTime.Now.AddMinutes(5),
                IsSended = false
            };
            _mailSenderService.ScheduleMailForSent(managerMail);
            #endregion

            //burada kaldın.

            if (!_userService.IsEmailExist(customTour.CustomerEmail))
            {
                var customerTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.CustomTurTalebiReceivedCustomer); //ikisi de aynı templatee sahip
                MailPlaceholderUtil.ReplaceMailContent(
                    customerTemplate,
                    new(guestName: customTour.CustomerName,
                         email: customTour.CustomerEmail,
                         phone: customTour.CustomerPhone,
                         name: customTour.CustomerName,
                         arrivalDate: customTour.ArrivalDate.ToShortDateString(),
                         destinations: destinationsString,
                         numberOfParticipants: customTour.NumberOfAdult.ToString(),
                         selectCountry: CountryList.GetValue(customTour.CountryID)));

                SendMail customerMail = new()
                {
                    Content = customerTemplate.Content,
                    Subject = customerTemplate.Subject,
                    Email = customTour.CustomerEmail,
                    SendTime = DateTime.Now.AddMinutes(5),
                    IsSended = false
                };
                _mailSenderService.ScheduleMailForSent(customerMail);
            }
            else
            {

                var memberTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.CustomTurTalebiReceivedMember); //ikisi de aynı templatee sahip


                MailPlaceholderUtil.ReplaceMailContent(
                    memberTemplate,
                    new(email: customTour.CustomerEmail,
                         phone: customTour.CustomerPhone,
                         name: customTour.CustomerName,
                         arrivalDate: customTour.ArrivalDate.ToShortDateString(),
                         destinations: destinationsString,
                         numberOfParticipants: customTour.NumberOfAdult.ToString(),
                         selectCountry: CountryList.GetValue(customTour.CountryID)));

                SendMail memberMail = new()
                {
                    Content = memberTemplate.Content,
                    Subject = memberTemplate.Subject,
                    Email = customTour.CustomerEmail,
                    SendTime = DateTime.Now.AddMinutes(5),
                    IsSended = false
                };
                _mailSenderService.ScheduleMailForSent(memberMail);
            }
        }
        public CustomTourOfferCustomerAnswer AddCustomerAnswer(CustomTourOfferCustomerAnswer dto)
        {
            var answer = _customTourRequestRepository.AddCustomerAnswer(dto);
            _unitOfWork.saveChanges();
            return answer;
        }
        public void AnswerOffer(AnswerOfferDto dto)
        {
            _customTourRequestRepository.AnswerOffer(dto);
        }
        public List<CustomTourRequestListItemDto> CustomTourRequestList()
        {
            List<CustomTourRequestListItemDto> dto = _customTourRequestRepository.CustomTourRequestList();
            return dto;
        }
        public List<OfferListDto> OfferListByRequestId(Guid requestId)
        {
            return _customTourRequestRepository.OfferListByRequestId(requestId);
        }
        //webden
        public GetCustomTourRequestDetailDto CustomTourRequestDetail(Guid requestId, int languageId)
        {
            return _customTourRequestRepository.CustomTourRequestDetail(requestId, languageId);
        }

        public void AddMailContent(SaveMailContentDto mailContent)
        {
            var customTourRequest = _customTourRequestRepository.GetById(new Guid(mailContent.RequestId));
            customTourRequest.MailContent = mailContent.MailContent;
            _customTourRequestRepository.Update(customTourRequest);
            _unitOfWork.saveChanges();
        }
    }
}
