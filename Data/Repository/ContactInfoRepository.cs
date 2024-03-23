using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ContactInfoDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ContactInfoRepository : GenericRepository<ContactInfo>, IContactInfoRepository
    {
        Context _context;
        DbSet<ContactInfo> _contactInfos;
        DbSet<ContactInfoLanguageItem> _contactInfoLanguageItems;
        public ContactInfoRepository(Context context) : base(context)
        {
            _context = context;
            _contactInfos = _context.ContactInfos;
            _contactInfoLanguageItems = _context.ContactInfoLanguageItems;
        }

        public void AddContactInfo(AddContactInfoDto addContactInfoDto)
        {
            ContactInfo contactInfo = new ContactInfo()
            {
                Type = addContactInfoDto.TypeID,
            };

            _contactInfos.Add(contactInfo);

            foreach (var language in LanguageList.AllLanguages)
            {
                ContactInfoLanguageItem contactInfoLanguageItem = new ContactInfoLanguageItem()
                {
                    ContactInfoID = contactInfo.Id,
                    LanguageID = language.Id,
                    Value = addContactInfoDto.Value
                };

                _contactInfoLanguageItems.Add(contactInfoLanguageItem);
            }
        }

        public List<ContactInfoListDto> GetContactInfoListDtos()
        {
            List<ContactInfoListDto> contactInfoListDtos = (from info in _contactInfos.ToList()
                                                            where info.IsDeleted == false
                                                            join languageItem in _contactInfoLanguageItems.ToList()
                                                            on info.Id equals languageItem.ContactInfoID
                                                            where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                            select new ContactInfoListDto()
                                                            {
                                                                ContactInfoID = info.Id,
                                                                Status = info.Status,
                                                                Type = new ContactInfoType().Types.FirstOrDefault(x => x.ID == info.Type).Value,
                                                                Value = languageItem.Value
                                                            }).ToList();
            return contactInfoListDtos;
        }

        public LanguageEditContactInfoDto GetLanguageEditContactInfoDto(Guid Id, int languageId)
        {
            ContactInfoLanguageItem contactInfoLanguageItem = _contactInfoLanguageItems.Where(x => x.ContactInfoID == Id && x.LanguageID == languageId).FirstOrDefault();

            LanguageEditContactInfoDto languageEditContactInfoDto = new LanguageEditContactInfoDto()
            {
                Value = contactInfoLanguageItem.Value,
                ContactInfoLanguageItemID = contactInfoLanguageItem.Id,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name
            };
            return languageEditContactInfoDto;
        }

        public void LanguageEditContactInfo(LanguageEditContactInfoDto languageEditContactInfoDto)
        {
            ContactInfoLanguageItem contactInfoLanguageItem = _contactInfoLanguageItems.Find(languageEditContactInfoDto.ContactInfoLanguageItemID);

            contactInfoLanguageItem.Value = languageEditContactInfoDto.Value;
            _contactInfoLanguageItems.Update(contactInfoLanguageItem);
        }
    }
}
