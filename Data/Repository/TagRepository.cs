using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        Context _context;
        DbSet<Tag> _tags;
        DbSet<TagLanguageItem> _tagLanguageItems;
        public TagRepository(Context context) : base(context)
        {
            _context = context;
            _tags = _context.Tags;
            _tagLanguageItems = _context.TagLanguageItems;
        }

        public void AddTag(AddTagDto addTagDto)
        {
            Tag tag = new Tag()
            {
                IconPath = addTagDto.IconPath,
                TagCategories = JsonSerializer.Serialize(addTagDto.TagCategoryIDs)
            };
            _tags.Add(tag);

            foreach (var language in LanguageList.AllLanguages)
            {
                TagLanguageItem tagLanguageItem = new TagLanguageItem()
                {
                    TagID = tag.Id,
                    DisplayName = addTagDto.DisplayName,
                    LangaugeID = language.Id
                };

                _tagLanguageItems.Add(tagLanguageItem);
            }
        }

        public void EditTag(EditTagDto editTagDto)
        {
            Tag tag = _tags.Find(editTagDto.TagID);


            tag.IconPath = editTagDto.IconPath;
            tag.TagCategories = JsonSerializer.Serialize(editTagDto.TagCategoryIDs);

            _tags.Update(tag);
        }

        public EditTagDto GetEditTagDto(Guid id)
        {
            Tag tag = _tags.Find(id);

            EditTagDto editTagDto = new EditTagDto()
            {
                TagID = tag.Id,
                IconPath = tag.IconPath,
                TagCategoryIDs = JsonSerializer.Deserialize<List<int>>(tag.TagCategories),
            };
            return editTagDto;
        }

        public LanguageEditTagDto GetLanguageEditTagDto(Guid id, int languageId)
        {
            TagLanguageItem tagLanguageItem = _tagLanguageItems.Where(x => x.LangaugeID == languageId && x.TagID == id).FirstOrDefault();

            LanguageEditTagDto languageEditTagDto = new LanguageEditTagDto()
            {
                TagLanguageItemID = tagLanguageItem.Id,
                DisplayName = tagLanguageItem.DisplayName,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Slug = tagLanguageItem.Slug
            };
            return languageEditTagDto;
        }

        public List<TagListDto> GetTagListDtos()
        {
            List<TagListDto> tagListDtos = (from tag in _tags.ToList()
                                            where tag.IsDeleted == false
                                            join languageItem in _tagLanguageItems.ToList()
                                            on tag.Id equals languageItem.TagID
                                            where languageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                            select new TagListDto
                                            {
                                                TagID = tag.Id,
                                                Status = tag.Status,
                                                IconPath = tag.IconPath,
                                                TagName = languageItem.DisplayName,
                                                TagCategories = (from category in new TagCategory().Categories
                                                                 where JsonSerializer.Deserialize<List<int>>(tag.TagCategories).Contains(category.ID)
                                                                 select category.Value
                                                                 ).ToList()
                                            }).ToList();
            return tagListDtos;
        }

        public void LanguageEditTag(LanguageEditTagDto languageEditTagDto)
        {
            TagLanguageItem tagLanguageItem = _tagLanguageItems.Find(languageEditTagDto.TagLanguageItemID);


            tagLanguageItem.DisplayName = languageEditTagDto.DisplayName;
            _tagLanguageItems.Update(tagLanguageItem);
        }

        public List<SelectListOptionDto> TagSelectList()
        {
            List<SelectListOptionDto> tagSelectList = (from tag in _tags.ToList()
                                                       where tag.IsDeleted == false && tag.Status
                                                       join languageItem in _tagLanguageItems.ToList()
                                                       on tag.Id equals languageItem.TagID
                                                       where languageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                       select new SelectListOptionDto()
                                                       {
                                                           OptionID = tag.Id,
                                                           Option = languageItem.DisplayName
                                                       }).ToList();
            return tagSelectList;
        }

        public List<SelectListOptionDto> TagSelectListForBlog()
        {

            List<SelectListOptionDto> tagSelectList = (from tag in _tags.ToList()
                                                       where tag.IsDeleted == false && tag.Status
                                                       where JsonSerializer.Deserialize<List<int>>(tag.TagCategories).Contains(new TagCategory().Categories.FirstOrDefault(x => x.Value == "Blog").ID)
                                                       join languageItem in _tagLanguageItems.ToList()
                                                       on tag.Id equals languageItem.TagID
                                                       where languageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                       select new SelectListOptionDto()
                                                       {
                                                           OptionID = tag.Id,
                                                           Option = languageItem.DisplayName
                                                       }).ToList();
            return tagSelectList;
        }

        public List<SelectListOptionDto> TagSelectListForProduct()
        {
            List<SelectListOptionDto> tagSelectList = (from tag in _tags.ToList()
                                                       where tag.IsDeleted == false && tag.Status
                                                       where JsonSerializer.Deserialize<List<int>>(tag.TagCategories).Contains(new TagCategory().Categories.FirstOrDefault(x => x.Value == "Product").ID)
                                                       join languageItem in _tagLanguageItems.ToList()
                                                       on tag.Id equals languageItem.TagID
                                                       where languageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                                       select new SelectListOptionDto()
                                                       {
                                                           OptionID = tag.Id,
                                                           Option = languageItem.DisplayName
                                                       }).ToList();
            return tagSelectList;
        }
    }
}
