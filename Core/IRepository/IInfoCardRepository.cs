﻿using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IInfoCardRepository : IGenericRepository<InfoCard>
    {
        List<InfoCardDto> GetInfoCardDtoList(int languageId);
    }
}
