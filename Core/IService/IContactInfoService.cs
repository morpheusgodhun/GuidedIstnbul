using Core.Entities;
using Dto.ApiPanelDtos.ContactInfoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IContactInfoService : IGenericService<ContactInfo>
    {
        List<ContactInfoListDto> GetContactInfoListDtos();
        void AddContactInfo(AddContactInfoDto addContactInfoDto);
        LanguageEditContactInfoDto GetLanguageEditContactInfoDto(Guid Id, int languageId);
        void LanguageEditContactInfo(LanguageEditContactInfoDto languageEditContactInfoDto);
    }
}
