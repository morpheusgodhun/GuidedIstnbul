using Core.Entities;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ITagService:IGenericService<Tag>
    {
        List<TagListDto> GetTagListDtos();
        void AddTag(AddTagDto addTagDto);
        EditTagDto GetEditTagDto(Guid id);
        void EditTag(EditTagDto editTagDto);
        LanguageEditTagDto GetLanguageEditTagDto(Guid id, int languageId);
        void LanguageEditTag(LanguageEditTagDto languageEditTagDto);
        List<SelectListOptionDto> TagSelectList();
        List<SelectListOptionDto> TagSelectListForBlog();
        List<SelectListOptionDto> TagSelectListForProduct();
    }
}
