using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TagService : GenericService<Tag>, ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(IGenericRepository<Tag> repository, IUnitOfWork unitOfWork, ITagRepository tagRepository) : base(repository, unitOfWork)
        {
            _tagRepository = tagRepository;
        }

        public void AddTag(AddTagDto addTagDto)
        {
            _tagRepository.AddTag(addTagDto);
            _unitOfWork.saveChanges();
        }

        public void EditTag(EditTagDto editTagDto)
        {
            _tagRepository.EditTag(editTagDto);
            _unitOfWork.saveChanges();
        }

        public EditTagDto GetEditTagDto(Guid id)
        {
            return _tagRepository.GetEditTagDto(id);
        }

        public LanguageEditTagDto GetLanguageEditTagDto(Guid id, int languageId)
        {
            return _tagRepository.GetLanguageEditTagDto(id, languageId);
        }

        public List<TagListDto> GetTagListDtos()
        {
            return _tagRepository.GetTagListDtos();
        }

        public void LanguageEditTag(LanguageEditTagDto languageEditTagDto)
        {
            _tagRepository.LanguageEditTag(languageEditTagDto);
            _unitOfWork.saveChanges();
        }

        public List<SelectListOptionDto> TagSelectList()
        {
            return _tagRepository.TagSelectList();
        }

        public List<SelectListOptionDto> TagSelectListForBlog()
        {
            return _tagRepository.TagSelectListForBlog();
        }

        public List<SelectListOptionDto> TagSelectListForProduct()
        {
            return _tagRepository.TagSelectListForProduct();
        }
    }
}
