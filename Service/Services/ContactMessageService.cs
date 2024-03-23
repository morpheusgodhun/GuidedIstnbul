using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ContactMessageDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using Service.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContactMessageService : GenericService<ContactMessage>, IContactMessageService
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly ISendMailTemplateService _mailTemplateService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IUserService _userService;

        public ContactMessageService(IGenericRepository<ContactMessage> repository, IUnitOfWork unitOfWork, IContactMessageRepository contactMessageRepository, ISendMailTemplateService mailTemplateService, IMailSenderService mailSenderService, IUserService userService) : base(repository, unitOfWork)
        {
            _contactMessageRepository = contactMessageRepository;
            _mailTemplateService = mailTemplateService;
            _mailSenderService = mailSenderService;
            _userService = userService;
        }

        public ContactMessageDetailDto ContactMessageDetail(Guid id)
        {
            return _contactMessageRepository.ContactMessageDetail(id);
        }

        public List<ContactMessageListDto> GetContactMessageListDtos()
        {
            return _contactMessageRepository.GetContactMessageListDtos();
        }

        public void SendContactMails(ContactPostDto contactPostDto)
        {
            //todo link göm
            //link panele gidiyor olacak. burada db ye eklenen contact messagein id sini öğrenmem lazım sanırım
            #region OperationMail
            var operationTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.BizeUlasinOperation);
            MailPlaceholderUtil.ReplaceMailContent(operationTemplate, new(name: contactPostDto.Name, email: contactPostDto.Email, message: contactPostDto.Message, subject: contactPostDto.Subject, link: ""));

            _mailSenderService.ScheduleMailForSent(new()
            {
                Content = operationTemplate.Content,
                Subject = operationTemplate.Subject,
                Email = SendMailAddressConstants.OperationMail,
                SendTime = DateTime.Now.AddMinutes(1),
                TemplateNo = operationTemplate.Template,
                IsSended = false
            });
            #endregion

            var managerTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.BizeUlasinManager);
            MailPlaceholderUtil.ReplaceMailContent(managerTemplate, new(name: contactPostDto.Name, email: contactPostDto.Email, message: contactPostDto.Message, subject: contactPostDto.Subject, link: ""));
            SendMail managerMail = new()
            {
                Content = managerTemplate.Content,
                Subject = managerTemplate.Subject,
                Email = SendMailAddressConstants.ManagerMail,
                SendTime = DateTime.Now.AddMinutes(1),
                TemplateNo = managerTemplate.Template,
                IsSended = false
            };
            _mailSenderService.ScheduleMailForSent(managerMail);

            bool isEmailExist = _userService.IsEmailExist(contactPostDto.Email);
            if (!isEmailExist)
            {
                var customerTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.BizeUlasinCustomer);
                MailPlaceholderUtil.ReplaceMailContent(customerTemplate, new(name: contactPostDto.Name, email: contactPostDto.Email, message: contactPostDto.Message, subject: contactPostDto.Subject));

                _mailSenderService.ScheduleMailForSent(new()
                {
                    Content = customerTemplate.Content,
                    Subject = customerTemplate.Subject,
                    Email = contactPostDto.Email,
                    SendTime = DateTime.Now.AddMinutes(1),
                    TemplateNo = customerTemplate.Template,
                    IsSended = false
                });
            }
            else
            {
                var memberTemplate = _mailTemplateService.GetTemplateByNo((int)SendMailTemplateName.No.BizeUlasinMember);
                MailPlaceholderUtil.ReplaceMailContent(memberTemplate, new(name: contactPostDto.Name, email: contactPostDto.Email, message: contactPostDto.Message, subject: contactPostDto.Subject));

                _mailSenderService.ScheduleMailForSent(new()
                {
                    Content = memberTemplate.Content,
                    Subject = memberTemplate.Subject,
                    Email = contactPostDto.Email,
                    SendTime = DateTime.Now.AddMinutes(1),
                    TemplateNo = memberTemplate.Template,
                    IsSended = false
                });
            }
        }
    }
}
