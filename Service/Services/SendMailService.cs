using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Dto.ApiPanelDtos.SendMailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SendMailService : GenericService<SendMail>, ISendMailService
    {
        private readonly ISendMailRepository _sendMailRepository;

        public SendMailService(IGenericRepository<SendMail> repository, IUnitOfWork unitOfWork, ISendMailRepository sendMailRepository) : base(repository, unitOfWork)
        {
            _sendMailRepository = sendMailRepository;
        }

        public void CancelScheduledUserMails(User user)
        {
            List<int> reservationTemplateNos = SendMailTemplateName.Status.Where(template => template.Value.Contains("Rezervasyon")).Select(x => x.ID).ToList();


            var scheduledMails = _sendMailRepository.Where(x => x.Email == user.Email && x.SendTime > DateTime.Now && x.Status && reservationTemplateNos.Contains(x.TemplateNo));
            if (scheduledMails.Any())
                scheduledMails.ForEach(mail =>
                {
                    ToggleStatus(mail.Id);
                });
        }

        //public void AddSendMail(SendMailDto mail)
        //{
        //    _sendMailRepository.AddSendMail(mail);
        //    _unitOfWork.saveChanges();
        //}
    }
}
