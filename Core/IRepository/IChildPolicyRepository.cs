using Core.Entities;
using Dto.ApiPanelDtos.ChildPolicyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IChildPolicyRepository : IGenericRepository<ChildPolicy>
    {
        List<ChildPolicyListDto> GetChildPolicyListDtos();

        void AddChildPolicy(AddChildPolicyDto addChildPolicyDto);
        EditChildPolicyDto GetEditChildPolicyDto(Guid id);
        void EditChildPolicy(EditChildPolicyDto editChildPolicyDto);
    }
}
