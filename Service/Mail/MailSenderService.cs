using Core.Entities;
using Core.IRepository;
using Core.IUnitOfWork;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Service.Mail
{
    public class MailSenderService : IMailSenderService
    {
        private readonly ISendMailRepository _sendMailRepository;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public MailSenderService(ISendMailRepository sendMailRepository, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _sendMailRepository = sendMailRepository;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseNullDto> MailSend(SendMail mail)
        {
            try
            {
                MailMessage mailMessage = new();
                mailMessage.From = new MailAddress(_configuration["Mail"]);
                mailMessage.To.Add(mail.Email);
                mailMessage.Subject = mail.Subject;
                mailMessage.Body = mail.Content;
                mailMessage.IsBodyHtml = true;

                //scoped ekleyerek de dene
                using SmtpClient smtpClient = new()
                {
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(_configuration["Mail"], _configuration["password"]),
                };
                //_smtpClient.Send(mailMessage);
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch
            {
                return CustomResponseNullDto.Fail(new List<string>() { "Mail Gönderilemedi" }, 404);
            }
            return CustomResponseNullDto.Success(200);
        }
        public async Task ScheduleMailForSent(SendMail mail)
        {
            mail.IsSended = false;
            mail.Status = true;
            mail.IsDeleted = false;
            await _sendMailRepository.AddAsync(mail);
            _unitOfWork.saveChanges();
        }

        public async Task<CustomResponseNullDto> SendInstantMail(SendMail mail)
        {
            var sendResponse = await MailSend(mail);

            mail.Status = sendResponse.StatusCode == 200;
            mail.IsSended = sendResponse.StatusCode == 200;
            _sendMailRepository.Add(mail);
            return sendResponse.StatusCode == 200 ? CustomResponseNullDto.Success(200) : CustomResponseNullDto.Fail(new List<string>() { "Mail Gönderilemedi" }, 404);
        }

        public async Task<CustomResponseNullDto> SendScheduledMails()
        {
            var scheduledMails = _sendMailRepository.Where(x => x.Status && !x.IsSended && x.SendTime < DateTime.Now).OrderByDescending(x => x.SendTime).Take(10).AsEnumerable();

            foreach (var mail in scheduledMails)
            {
                var mailResponse = await MailSend(mail);
                if (mailResponse.StatusCode == 200)
                {
                    mail.Status = false;
                    mail.IsSended = true;
                    _sendMailRepository.Update(mail);
                }
            }

            _unitOfWork.saveChanges();
            return CustomResponseNullDto.Success(200); //, true, "Zamanlı mailler gönderildi"
        }


    }
}
