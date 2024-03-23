using Core.Entities;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ICancellationPolicyRepository : IGenericRepository<CancellationPolicy>
    {
        List<CancellationPolicyListDto> GetCancellationPolicyListDtos();
        void AddCancellationPolicy(AddCancellationPolicyDto addCancellationPolicyDto);
        EditCancellationPolicyDto GetEditCancellationPolicyDto(Guid id);
        LanguageEditCancellationPolicyDto GetLanguageEditCancellationPolicyDto(Guid id, int languageId);
        void EditCancellationPolicy(EditCancellationPolicyDto editCancellationPolicyDto);
        void LanguageEditCancellationPolicy(LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto);
        List<SelectListOptionDto> GetCancellationPolicySelectList();


    }
}
