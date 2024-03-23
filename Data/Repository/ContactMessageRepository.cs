using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.ContactMessageDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ContactMessageRepository : GenericRepository<ContactMessage>, IContactMessageRepository
    {
        Context _context;
        DbSet<ContactMessage> _contactMessages;
        DbSet<SystemOptionLanguageItem> _systemOptionLanguageItems;
        public ContactMessageRepository(Context context) : base(context)
        {
            _context = context;
            _contactMessages = _context.ContactMessages;
            _systemOptionLanguageItems = _context.SystemOptionLanguageItems;

        }

        public ContactMessageDetailDto ContactMessageDetail(Guid id)
        {

            ContactMessage contactMessage = _contactMessages.Find(id);

            ContactMessageDetailDto contactMessageDetailDto = new ContactMessageDetailDto()
            {
                ContactMessageID = contactMessage.Id,
                Country = CountryList.Countries.FirstOrDefault(x => x.ID == contactMessage.CountryID).Value,
                Message = contactMessage.Message,
                Subject = contactMessage.Subject,
                SenderName = contactMessage.SenderName,
                SendingDate = contactMessage.SendingDate,
                Phone = contactMessage.SenderPhoneNumber,
                HowFindUs = _systemOptionLanguageItems.Where(x => x.SystemOptionId == contactMessage.SystemOptionID && x.LanguageID == LanguageList.BaseLanguage.Id).FirstOrDefault().Name,
            };
            return contactMessageDetailDto;
        }

        public List<ContactMessageListDto> GetContactMessageListDtos()
        {
            List<ContactMessageListDto> contactMessageListDtos = (from message in _contactMessages.ToList()
                                                                  where message.IsDeleted == false
                                                                  select new ContactMessageListDto()
                                                                  {
                                                                      ContactMessageID = message.Id,
                                                                      SenderName = message.SenderName,
                                                                      SenderPhone = message.SenderPhoneNumber,
                                                                      Subject = message.Subject,
                                                                      SendingDate = message.SendingDate,
                                                                  }).ToList();
            return contactMessageListDtos;
        }
    }
}
