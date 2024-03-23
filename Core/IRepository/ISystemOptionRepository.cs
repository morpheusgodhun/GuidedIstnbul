using Core.Entities;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ISystemOptionRepository : IGenericRepository<SystemOption>
    {
        List<SelectListOptionDto> GetSystemOptionByCategoryId(int systemOptionCategoryId, int languageId);
        SystemOption GetSystemOptionById(Guid systemOptionId, int languageId);
        SelectListOptionDto GetCityById(int systemOptionCategoryId, int languageId, Guid cityId);
    }
}
