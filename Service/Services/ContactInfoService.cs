using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.ContactInfoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContactInfoService : GenericService<ContactInfo>, IContactInfoService
    {
        IContactInfoRepository _contactInfoRepository;

        public ContactInfoService(IGenericRepository<ContactInfo> repository, IUnitOfWork unitOfWork, IContactInfoRepository contactInfoRepository) : base(repository, unitOfWork)
        {
            _contactInfoRepository = contactInfoRepository;
        }

        public void AddContactInfo(AddContactInfoDto addContactInfoDto)
        {
            _contactInfoRepository.AddContactInfo(addContactInfoDto);
            _unitOfWork.saveChanges();
        }

        public List<ContactInfoListDto> GetContactInfoListDtos()
        {
            return _contactInfoRepository.GetContactInfoListDtos();
        }

        public LanguageEditContactInfoDto GetLanguageEditContactInfoDto(Guid Id, int languageId)
        {
            return _contactInfoRepository.GetLanguageEditContactInfoDto(Id, languageId);
        }

        public void LanguageEditContactInfo(LanguageEditContactInfoDto languageEditContactInfoDto)
        {
            _contactInfoRepository.LanguageEditContactInfo(languageEditContactInfoDto);
            _unitOfWork.saveChanges();
        }
    }
}
