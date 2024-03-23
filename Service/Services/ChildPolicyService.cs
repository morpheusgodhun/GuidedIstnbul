using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.ChildPolicyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ChildPolicyService : GenericService<ChildPolicy>, IChildPolicyService
    {
 
        private readonly IChildPolicyRepository _childPolicyRepository;

        public ChildPolicyService(IGenericRepository<ChildPolicy> repository, IUnitOfWork unitOfWork, IChildPolicyRepository childPolicyRepository) : base(repository, unitOfWork)
        {
            _childPolicyRepository = childPolicyRepository;
        }

        public void AddChildPolicy(AddChildPolicyDto addChildPolicyDto)
        {
            _childPolicyRepository.AddChildPolicy(addChildPolicyDto);
            _unitOfWork.saveChanges();
        }

        public void EditChildPolicy(EditChildPolicyDto editChildPolicyDto)
        {
            _childPolicyRepository.EditChildPolicy(editChildPolicyDto);
            _unitOfWork.saveChanges();
        }

        public List<ChildPolicyListDto> GetChildPolicyListDtos()
        {
            return _childPolicyRepository.GetChildPolicyListDtos();
        }

        public EditChildPolicyDto GetEditChildPolicyDto(Guid id)
        {
            return _childPolicyRepository.GetEditChildPolicyDto(id);
        }
    }
}
