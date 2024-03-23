using Core.Entities;
using Core.StaticValues;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IUserExtensionMailService : IGenericService<UserExtensionMail>
    {
        UserExtensionMailDto GetByUrlCode(string uniqueUrlCode);
        bool IsUrlCodeValid(string urlCode, SendMailTemplateName.No templateType);
        ReservationPaymentSendMailDto UrlCodeByPayment(string urlCode);

    }
}
