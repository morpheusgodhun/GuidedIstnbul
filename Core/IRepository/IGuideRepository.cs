﻿using Core.Entities;
using Dto.ApiPanelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IGuideRepository:IGenericRepository<Guide>
    {
        List<GetGuideDto> GetAllGuides();
        
    }
}
