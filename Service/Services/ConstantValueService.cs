using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.ConstantValueDtos;
using Dto.ApiWebDtos.GeneralDtos;

namespace Service.Services
{
    public class ConstantValueService : GenericService<ConstantValue>, IConstantValueService
    {
        private readonly IConstantValueRepository _constantValueRepository;
        public ConstantValueService(IGenericRepository<ConstantValue> repository, IUnitOfWork unitOfWork, IConstantValueRepository constantValueRepository) : base(repository, unitOfWork)
        {
            _constantValueRepository = constantValueRepository;
        }

        public void AddConstantValue(AddConstantValueDto addConstantValueDto)
        {
            _constantValueRepository.AddConstantValue(addConstantValueDto);
            _unitOfWork.saveChanges();
        }

        public Task<List<ConstantValueDto>> GetConstantValueByPageNameAsync(string pageName, int languageId)
        {
            return _constantValueRepository.GetConstantValueByPageNameAsync(pageName, languageId);
        }

        public async Task<List<ConstantValueDto>> GetAllConstantValue(int languageId)
        {
            return await _constantValueRepository.GetAllConstantValue(languageId);
        }

        public List<ConstantValueListDto> GetConstantValueListDtos()
        {
            return _constantValueRepository.GetConstantValueListDtos();
        }

        public LanguageEditConstantValueDto GetLanguageEditConstantValueDto(Guid id, int languageId)
        {
            return _constantValueRepository.GetLanguageEditConstantValueDto(id, languageId);
        }

        public void LanguageEditConstantValue(LanguageEditConstantValueDto languageEditConstantValueDto)
        {
            _constantValueRepository.LanguageEditConstantValue(languageEditConstantValueDto);
            _unitOfWork.saveChanges();
        }
    }
}
