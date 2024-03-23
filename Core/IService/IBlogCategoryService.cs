using Core.Entities;
using Dto.ApiPanelDtos.BlogCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IBlogCategoryService : IGenericService<BlogCategory>
    {
        List<BlogCategoryListDto> GetBlogCategoryListDtos();
        void AddBlogCategory(AddBlogCategoryDto addBlogCategoryDto);
        EditBlogCategoryDto GetEditBlogCategoryDto(Guid id);
        void EditBlogCategory(EditBlogCategoryDto editBlogCategoryDto);
        LanguageEditBlogCategoryDto GetLanguageEditBlogCategoryDto(Guid id, int languageId);
        void LanguageEditBlogCategory(LanguageEditBlogCategoryDto languageEditBlogCategoryDto);
        List<SelectListOptionDto> BlogCategorySelectList();
    }
}
