using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.BlogCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogCategoryService : GenericService<BlogCategory>, IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryService(IGenericRepository<BlogCategory> repository, IUnitOfWork unitOfWork, IBlogCategoryRepository blogCategoryRepository) : base(repository, unitOfWork)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public void AddBlogCategory(AddBlogCategoryDto addBlogCategoryDto)
        {
            _blogCategoryRepository.AddBlogCategory(addBlogCategoryDto);
            _unitOfWork.saveChanges();
        }

        public List<SelectListOptionDto> BlogCategorySelectList()
        {
            return _blogCategoryRepository.BlogCategorySelectList();
        }

        public void EditBlogCategory(EditBlogCategoryDto editBlogCategoryDto)
        {
            _blogCategoryRepository.EditBlogCategory(editBlogCategoryDto);
            _unitOfWork.saveChanges();
        }

        public List<BlogCategoryListDto> GetBlogCategoryListDtos()
        {
            return _blogCategoryRepository.GetBlogCategoryListDtos();
        }

        public EditBlogCategoryDto GetEditBlogCategoryDto(Guid id)
        {
            return _blogCategoryRepository.GetEditBlogCategoryDto(id);
        }

        public LanguageEditBlogCategoryDto GetLanguageEditBlogCategoryDto(Guid id, int languageId)
        {
            return _blogCategoryRepository.GetLanguageEditBlogCategoryDto(id, languageId);
        }

        public void LanguageEditBlogCategory(LanguageEditBlogCategoryDto languageEditBlogCategoryDto)
        {
            _blogCategoryRepository.LanguageEditBlogCategory(languageEditBlogCategoryDto);
            _unitOfWork.saveChanges();
        }
    }
}
