using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;
using Data.Repository;
using Dto.ApiPanelDtos.ReservationPaymentDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Payment;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserExtensionMailService : GenericService<UserExtensionMail>, IUserExtensionMailService
    {
        readonly IUserExtensionMailRepository _userExtensionMailRepository;
        public UserExtensionMailService(IGenericRepository<UserExtensionMail> repository, IUnitOfWork unitOfWork, IUserExtensionMailRepository userExtensionMailRepository) : base(repository, unitOfWork)
        {
            _userExtensionMailRepository = userExtensionMailRepository;
        }
        //ReservationPaymentSendMailDto
        public UserExtensionMailDto GetByUrlCode(string uniqueUrlCode)
        {
            var extensionMail = _repository.Where(x => x.UrlCode == Guid.Parse(uniqueUrlCode)).FirstOrDefault();
            return new()
            {
                UrlCode = extensionMail.UrlCode.ToString(),
                ExpireDate = extensionMail.ExpireDate,
                MailTemplateType = extensionMail.MailTemplateType,
                ResetCode = extensionMail.ResetCode,
                UserId = extensionMail.UserId.ToString()
            };
        }
        public ReservationPaymentSendMailDto UrlCodeByPayment(string urlCode)
        {
            var extensionMail = _repository.Where(x => x.UrlCode == Guid.Parse(urlCode)).FirstOrDefault();
            return new ReservationPaymentSendMailDto()
            {
                Amount = extensionMail.Amount,
                Email = extensionMail.Email,
                ReservationId = extensionMail.ReservationId ?? Guid.Empty,
                ExpireDate = extensionMail.ExpireDate,
            };
        }

        public bool IsUrlCodeValid(string urlCode, SendMailTemplateName.No templateType)
        {
            var mail = _userExtensionMailRepository.GetByUrlCode(urlCode);

            if (mail is null)
                return false;
            if (mail.ExpireDate < DateTime.Now)
                return false;
            if (!mail.Status)
                return false;
            if (!mail.MailTemplateType.Equals((int)templateType))
                return false;

            return true;
        }


    }
}
