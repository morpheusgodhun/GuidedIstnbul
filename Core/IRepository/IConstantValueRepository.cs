using Core.Entities;
using Dto.ApiPanelDtos.ConstantValueDtos;
using Dto.ApiWebDtos.GeneralDtos;

namespace Core.IRepository
{
    public interface IConstantValueRepository : IGenericRepository<ConstantValue>
    {
        Task<List<ConstantValueDto>> GetConstantValueByPageNameAsync(string pageName, int languageId);
        List<ConstantValueListDto> GetConstantValueListDtos();
        Task<List<ConstantValueDto>> GetAllConstantValue(int languageId);
        void AddConstantValue(AddConstantValueDto addConstantValueDto);
        LanguageEditConstantValueDto GetLanguageEditConstantValueDto(Guid id, int languageId);
        void LanguageEditConstantValue(LanguageEditConstantValueDto languageEditConstantValueDto);
    }
}
