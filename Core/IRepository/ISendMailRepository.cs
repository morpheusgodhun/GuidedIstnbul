using Core.Entities;
using Dto.ApiPanelDtos.SendMailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ISendMailRepository : IGenericRepository<SendMail>
    {
        //void AddSendMail(SendMailDto mail);
    }
}
