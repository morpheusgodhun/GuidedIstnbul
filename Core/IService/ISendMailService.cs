using Core.Entities;
using Dto.ApiPanelDtos.SendMailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ISendMailService : IGenericService<SendMail>
    {
        //public void AddSendMail(SendMailDto mail);
        void CancelScheduledUserMails(User user);
    }
}
