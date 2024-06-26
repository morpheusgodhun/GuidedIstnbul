﻿using Core.Entities;
using Dto.ApiPanelDtos.ContactMessageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IContactMessageRepository : IGenericRepository<ContactMessage>
    {
        List<ContactMessageListDto> GetContactMessageListDtos();
        ContactMessageDetailDto ContactMessageDetail(Guid id);
    }
}
