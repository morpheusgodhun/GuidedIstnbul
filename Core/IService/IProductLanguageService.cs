using Core.Entities;
using Dto.ApiWebDtos.WebToApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IProductLanguageService : IGenericService<ProductLanguageItem>
    {
        ProductNameDto GetProductNameForReservation(Guid reservationId, int languageId);
        
    }
}
