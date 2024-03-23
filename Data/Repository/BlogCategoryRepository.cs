using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.BlogCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository
    {
        Context _context;
        DbSet<BlogCategory> _blogCategories;
        DbSet<BlogCategoryLanguageItem> _blogCategoryLanguageItems;
        public BlogCategoryRepository(Context context) : base(context)
        {
            _context = context;
            _blogCategories = _context.BlogCategories;
            _blogCategoryLanguageItems = _context.BlogCategoryLanguageItems;
        }

        public void AddBlogCategory(AddBlogCategoryDto addBlogCategoryDto)
        {
            BlogCategory blogCategory = new BlogCategory()
            {
                Order = addBlogCategoryDto.Order
            };

            _blogCategories.Add(blogCategory);

            foreach (var language in LanguageList.AllLanguages)
            {
                BlogCategoryLanguageItem blogCategoryLanguageItem = new BlogCategoryLanguageItem()
                {
                    BlogCategoryID = blogCategory.Id,
                    LanguageID = language.Id,
                    CategoryName = addBlogCategoryDto.BlogCategoryName,
                    Slug = addBlogCategoryDto.Slug
                };

                _blogCategoryLanguageItems.Add(blogCategoryLanguageItem);
            }
        }

        public List<SelectListOptionDto> BlogCategorySelectList()
        {
            List<SelectListOptionDto> blogCategory = (from category in _blogCategories.ToList()
                                                      where !category.IsDeleted
                                                      join languageItem in _blogCategoryLanguageItems.ToList()
                                                      on category.Id equals languageItem.BlogCategoryID
                                                      where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                      select new SelectListOptionDto()
                                                      {
                                                          OptionID = category.Id,
                                                          Option = languageItem.CategoryName
                                                      }).ToList();
            return blogCategory;
        }

        public void EditBlogCategory(EditBlogCategoryDto editBlogCategoryDto)
        {
            BlogCategory blogCategory = _blogCategories.Find(editBlogCategoryDto.BlogCategoryID);

            blogCategory.Order = editBlogCategoryDto.Order;
            _blogCategories.Update(blogCategory);
        }

        public List<BlogCategoryListDto> GetBlogCategoryListDtos()
        {
            List<BlogCategoryListDto> blogCategoryListDtos = (from category in _blogCategories.ToList()
                                                              where !category.IsDeleted
                                                              join languageItem in _blogCategoryLanguageItems.ToList()
                                                              on category.Id equals languageItem.BlogCategoryID
                                                              where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                              select new BlogCategoryListDto
                                                              {
                                                                  BlogCategoryID = category.Id,
                                                                  BlogCategoryName = languageItem.CategoryName,
                                                                  Order = category.Order,
                                                                  Status = category.Status
                                                                 
                                                              }).ToList();
            return blogCategoryListDtos;
        }

        public EditBlogCategoryDto GetEditBlogCategoryDto(Guid id)
        {
            BlogCategory blogCategory = _blogCategories.Find(id);
            var blogCategoryName = _blogCategoryLanguageItems.Where(x => x.BlogCategoryID == id && x.LanguageID == 1).FirstOrDefault().CategoryName;
            EditBlogCategoryDto editBlogCategoryDto = new EditBlogCategoryDto()
            {
                BlogCategoryID = blogCategory.Id,
                Order = blogCategory.Order,
                BlogCategoryName = blogCategoryName
            };
            return editBlogCategoryDto;
        }

        public LanguageEditBlogCategoryDto GetLanguageEditBlogCategoryDto(Guid id, int languageId)
        {
            BlogCategoryLanguageItem blogCategoryLanguageItem = _blogCategoryLanguageItems.Where(x => x.LanguageID == languageId && x.BlogCategoryID == id).FirstOrDefault();

            LanguageEditBlogCategoryDto languageEditBlogCategoryDto = new LanguageEditBlogCategoryDto()
            {
                BlogCategoryLanguageItemID = blogCategoryLanguageItem.Id,
                BlogCategoryName = blogCategoryLanguageItem.CategoryName,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Slug=blogCategoryLanguageItem.Slug,
            };
            return languageEditBlogCategoryDto;
        }

        public void LanguageEditBlogCategory(LanguageEditBlogCategoryDto languageEditBlogCategoryDto)
        {
            BlogCategoryLanguageItem blogCategoryLanguageItem = _blogCategoryLanguageItems.Find(languageEditBlogCategoryDto.BlogCategoryLanguageItemID);

            blogCategoryLanguageItem.CategoryName = languageEditBlogCategoryDto.BlogCategoryName;
            _blogCategoryLanguageItems.Update(blogCategoryLanguageItem);
        }
    }
}
