using Core.Entities;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ICancellationPolicyService : IGenericService<CancellationPolicy>
    {
        List<CancellationPolicyListDto> GetCancellationPolicyListDtos();
        void AddCancellationPolicy(AddCancellationPolicyDto addCancellationPolicyDto);
        EditCancellationPolicyDto GetEditCancellationPolicyDto(Guid id);
        void EditCancellationPolicy(EditCancellationPolicyDto editCancellationPolicyDto);
        LanguageEditCancellationPolicyDto GetLanguageEditCancellationPolicyDto(Guid id, int languageId);
        void LanguageEditCancellationPolicy(LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto);
        List<SelectListOptionDto> GetCancellationPolicySelectList();
    }
}
