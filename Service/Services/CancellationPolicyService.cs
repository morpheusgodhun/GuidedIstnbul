using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CancellationPolicyService : GenericService<CancellationPolicy>, ICancellationPolicyService
    {
        ICancellationPolicyRepository _cancellationPolicyRepository;

        public CancellationPolicyService(IGenericRepository<CancellationPolicy> repository, IUnitOfWork unitOfWork, ICancellationPolicyRepository cancellationPolicyRepository) : base(repository, unitOfWork)
        {
            _cancellationPolicyRepository = cancellationPolicyRepository;
        }

        public void AddCancellationPolicy(AddCancellationPolicyDto addCancellationPolicyDto)
        {
            _cancellationPolicyRepository.AddCancellationPolicy(addCancellationPolicyDto);
            _unitOfWork.saveChanges();
        }

        public void EditCancellationPolicy(EditCancellationPolicyDto editCancellationPolicyDto)
        {
            _cancellationPolicyRepository.EditCancellationPolicy(editCancellationPolicyDto);
            _unitOfWork.saveChanges();
        }

        public List<CancellationPolicyListDto> GetCancellationPolicyListDtos()
        {
            return _cancellationPolicyRepository.GetCancellationPolicyListDtos();
        }

        public List<SelectListOptionDto> GetCancellationPolicySelectList()
        {
            return _cancellationPolicyRepository.GetCancellationPolicySelectList();
        }

        public EditCancellationPolicyDto GetEditCancellationPolicyDto(Guid id)
        {
            return _cancellationPolicyRepository.GetEditCancellationPolicyDto(id);
        }

        public LanguageEditCancellationPolicyDto GetLanguageEditCancellationPolicyDto(Guid id, int languageId)
        {
            return _cancellationPolicyRepository.GetLanguageEditCancellationPolicyDto(id, languageId);
        }

        public void LanguageEditCancellationPolicy(LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto)
        {
            _cancellationPolicyRepository.LanguageEditCancellationPolicy(languageEditCancellationPolicyDto);
            _unitOfWork.saveChanges();
        }
    }
}
