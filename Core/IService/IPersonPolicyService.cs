using Core.Entities;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IPersonPolicyService : IGenericService<PersonPolicy>
    {
        List<PersonPolicyListDto> GetPersonPolicyListDto();
        void AddPersonPolicy(AddPersonPolicyDto addPersonPolicyDto);
        EditPersonPolicyDto GetEditPersonPolicyDto(Guid id);
        void EditPersonPolicy(EditPersonPolicyDto editPersonPolicyDto);
        List<PersonPolicyDto> PersonPolicyDtoList();
    }
}
