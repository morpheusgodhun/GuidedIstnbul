using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mail
{
    public class RecurringEmailJob : IJob
    {
        private readonly IMailSenderService _mailSenderService;

        public RecurringEmailJob(IMailSenderService mailSenderService)
        {
            _mailSenderService = mailSenderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mailSenderService.SendScheduledMails();
        }
    }
}
