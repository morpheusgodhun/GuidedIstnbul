using Core.Entities;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ISystemOptionService : IGenericService<SystemOption>
    {
        List<SelectListOptionDto> GetSystemOptionByCategoryId(int systemOptionCategoryId, int languageId);
        SelectListOptionDto GetCityById(int systemOptionCategoryId, int languageId, Guid cityId);
        SystemOption GetSystemOptionById(Guid id, int languageId);
    }
}
