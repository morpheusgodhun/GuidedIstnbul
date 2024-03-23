using Core.Entities;
using Dto.ApiPanelDtos.ContactMessageDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IContactMessageService : IGenericService<ContactMessage>
    {
        List<ContactMessageListDto> GetContactMessageListDtos();
        ContactMessageDetailDto ContactMessageDetail(Guid id);
        void SendContactMails(ContactPostDto contactPostDto);
    }
}
