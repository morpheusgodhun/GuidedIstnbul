using Core.Entities;
using DTO;

namespace Service.Mail
{
    public interface IMailSenderService
    {
        Task<CustomResponseNullDto> MailSend(SendMail mail);
        Task<CustomResponseNullDto> SendInstantMail(SendMail mail);
        Task<CustomResponseNullDto> SendScheduledMails();
        Task ScheduleMailForSent(SendMail mail);
    }
}