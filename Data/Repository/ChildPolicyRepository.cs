using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiPanelDtos.ChildPolicyDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ChildPolicyRepository : GenericRepository<ChildPolicy>, IChildPolicyRepository
    {
        Context _context;
        DbSet<ChildPolicy> _childPolicy;
        public ChildPolicyRepository(Context context) : base(context)
        {
            _context = context;
            _childPolicy = _context.ChildPolicies;
        }

        public void AddChildPolicy(AddChildPolicyDto addChildPolicyDto)
        {
            ChildPolicy childPolicy = new ChildPolicy()
            {
                Order = addChildPolicyDto.Order,
                FromAge = addChildPolicyDto.FromAge,
                ToAge = addChildPolicyDto.ToAge,
                Percent = addChildPolicyDto.PayPercent
            };

            _childPolicy.Add(childPolicy);
        }

        public void EditChildPolicy(EditChildPolicyDto editChildPolicyDto)
        {
            ChildPolicy childPolicy = _childPolicy.Find(editChildPolicyDto.ChildPolicyID);

            childPolicy.Order = editChildPolicyDto.Order;
            childPolicy.FromAge = editChildPolicyDto.FromAge;
            childPolicy.ToAge = editChildPolicyDto.ToAge;
            childPolicy.Percent = editChildPolicyDto.PayPercent;

            _childPolicy.Update(childPolicy);
        }

        public List<ChildPolicyListDto> GetChildPolicyListDtos()
        {

            List<ChildPolicyListDto> childPolicyListDtos = (from childPolicy in _childPolicy.ToList()
                                                            where childPolicy.IsDeleted == false
                                                            select new ChildPolicyListDto()
                                                            {
                                                                ChildPolicyID = childPolicy.Id,
                                                                Status = childPolicy.Status,
                                                                Order = childPolicy.Order,
                                                                FromAge = childPolicy.FromAge,
                                                                ToAge = childPolicy.ToAge,
                                                                PayPercent = childPolicy.Percent

                                                            }).ToList();

            return childPolicyListDtos;
        }

        public EditChildPolicyDto GetEditChildPolicyDto(Guid id)
        {
            ChildPolicy childPolicy = _childPolicy.Find(id);

            EditChildPolicyDto editChildPolicyDto = new EditChildPolicyDto()
            {
                ChildPolicyID = childPolicy.Id,
                Order = childPolicy.Order,
                FromAge = childPolicy.FromAge,
                ToAge = childPolicy.ToAge,
                PayPercent = childPolicy.Percent
            };

            return editChildPolicyDto;
        }
    }
}
