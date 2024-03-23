using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SystemOptionService : GenericService<SystemOption>, ISystemOptionService
    {
        private readonly ISystemOptionRepository _systemOptionRepository;
        public SystemOptionService(IGenericRepository<SystemOption> repository, IUnitOfWork unitOfWork, ISystemOptionRepository systemOptionRepository) : base(repository, unitOfWork)
        {
            _systemOptionRepository = systemOptionRepository;
        }

        public List<SelectListOptionDto> GetSystemOptionByCategoryId(int systemOptionCategoryId, int languageId)
        {
            return _systemOptionRepository.GetSystemOptionByCategoryId(systemOptionCategoryId, languageId);
        }
        public SelectListOptionDto GetCityById(int systemOptionCategoryId, int languageId, Guid cityId)
        {
            return _systemOptionRepository.GetCityById(systemOptionCategoryId, languageId, cityId);
        }

        public SystemOption GetSystemOptionById(Guid id, int languageId)
        {
            return _systemOptionRepository.GetSystemOptionById(id, languageId);
        }
    }
}
