using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PersonPolicyService : GenericService<PersonPolicy>, IPersonPolicyService
    {
        private readonly IPersonPolicyRepository _personPolicyRepository;
        public PersonPolicyService(IGenericRepository<PersonPolicy> repository, IUnitOfWork unitOfWork, IPersonPolicyRepository personPolicyRepository) : base(repository, unitOfWork)
        {
            _personPolicyRepository = personPolicyRepository;
        }

        public void AddPersonPolicy(AddPersonPolicyDto addPersonPolicyDto)
        {
            _personPolicyRepository.AddPersonPolicy(addPersonPolicyDto);
            _unitOfWork.saveChanges();
        }

        public void EditPersonPolicy(EditPersonPolicyDto editPersonPolicyDto)
        {
            _personPolicyRepository.EditPersonPolicy(editPersonPolicyDto);
            _unitOfWork.saveChanges();
        }

        public EditPersonPolicyDto GetEditPersonPolicyDto(Guid id)
        {
            return _personPolicyRepository.GetEditPersonPolicyDto(id);
        }

        public List<PersonPolicyListDto> GetPersonPolicyListDto()
        {
            return _personPolicyRepository.GetPersonPolicyListDto();
        }

        public List<PersonPolicyDto> PersonPolicyDtoList()
        {
            return _personPolicyRepository.PersonPolicyDtoList();
        }
    }
}
