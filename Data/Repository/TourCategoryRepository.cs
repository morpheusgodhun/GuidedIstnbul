using Core.Entities;
using Core.IRepository;
using Data.Migrations;
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TourCategoryRepository : GenericRepository<TourCategory>, ITourCategoryRepository
    {
        Context _context;
        DbSet<TourCategory> _categories;
        DbSet<TourCategoryLanguageItem> _categoryLanguageItems;
        DbSet<Tour> _tours;
        DbSet<TourLanguageItem> _toursLanguageItems;
        DbSet<Many_Tour_TourCategory> _many_TourCategories;
        DbSet<Product> _product;
        DbSet<ProductLanguageItem> _productLanguageItems;
        DbSet<SystemOptionLanguageItem> _systemOptionLanguageItems;

        DbSet<ConstantValue> _constantValues;
        DbSet<ConstantValueLanguageItem> _constantValueLanguageItems;
        DbSet<Page> _pages;



        public TourCategoryRepository(Context context) : base(context)
        {
            _context = context;
            _categories = _context.TourCategories;
            _categoryLanguageItems = _context.TourCategoryLanguageItems;
            _tours = _context.Tours;
            _product = _context.Products;
            _productLanguageItems = _context.ProductLanguageItems;
            _toursLanguageItems = _context.TourLanguageItems;
            _many_TourCategories = _context.Many_Tour_TourCategories;
            _systemOptionLanguageItems = _context.SystemOptionLanguageItems;


            _constantValues = _context.ConstantValues;
            _constantValueLanguageItems = _context.ConstantValueLanguageItems;
            _pages = _context.Pages;

        }

        public List<NavbarTourCategoryDto> GetTourCategoriesForNavbar(int languageId)
        {
            var value = (from category in _categories.ToList()
                         where category.Status
                         join categoryLangauge in _categoryLanguageItems.ToList()
                         on category.Id equals categoryLangauge.TourCategoryID
                         where categoryLangauge.LanguageID == languageId
                         select new NavbarTourCategoryDto()
                         {
                             CategoryID = category.Id,
                             CategoryName = categoryLangauge.CategoryName,
                             Slug = categoryLangauge.Slug
                         }).ToList();
            return value;
        }



        public string GetTourCategoryName(Guid id)
        {
            var tourCategory = _context.TourCategories.Where(x => x.Id == id).Include(x => x.TourCategoryLanguageItems).FirstOrDefault();
            return tourCategory.TourCategoryLanguageItems.FirstOrDefault(x => x.LanguageID == 1).CategoryName;
        }
        public override async Task<TourCategory> GetByIdAsync(Guid id)
        {
            return await _context.TourCategories.Include(x => x.TourCategoryLanguageItems).FirstOrDefaultAsync(x => x.Id == id);
        }
        public GetCategoryTourListDto GetTourListByCategory(Guid categoryId, int languageId)
        {
            GetCategoryTourListDto value = (from category in _categories.ToList()
                                            where category.Id == categoryId
                                            join categoryLanguage in _categoryLanguageItems.ToList()
                                            on category.Id equals categoryLanguage.TourCategoryID
                                            where categoryLanguage.LanguageID == languageId
                                            select new GetCategoryTourListDto()
                                            {
                                                TourCategoryBannerImagePath = category.BannerImagePath,
                                                TourCategoryDescription = categoryLanguage.Content,
                                                TourCategoryName = categoryLanguage.CategoryName,
                                                ConstantValues = (from item in _constantValues.ToList()
                                                                  join page in _pages.Where(x => x.PageName == "Tour List").ToList()
                                                                  on item.PageID equals page.Id
                                                                  join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                                                                  on item.Id equals languageItem.ConstantValueID
                                                                  select new ConstantValueDto()
                                                                  {
                                                                      Key = item.Key,
                                                                      Value = languageItem.Value
                                                                  }).ToList(),
                                                Tours = (from many in _many_TourCategories.ToList()
                                                         where many.TourCategoryID == categoryId
                                                         join tour in _tours.ToList()
                                                         on many.TourID equals tour.Id
                                                         join product in _product.ToList()
                                                         on tour.ProductID equals product.Id
                                                         where !product.IsDeleted && product.IsTour && product.Status
                                                         join productLanguage in _productLanguageItems.ToList()
                                                         on product.Id equals productLanguage.ProductID
                                                         where productLanguage.LanguageID == languageId && tour.CustomTourRequestId == null
                                                         orderby product.Order
                                                         select new TourListDto()
                                                         {
                                                             ProductID = product.Id,
                                                             Order = product.Order,
                                                             Slug = productLanguage.Slug,
                                                             CardImagePath = product.CardImagePath,
                                                             Name = productLanguage.DisplayName,
                                                             Price = 0, //Sonradan hesaplayacağım için 0 çekiyorum
                                                             Rate = 5,
                                                             TourType = (from typeLanguage in _systemOptionLanguageItems.ToList()
                                                                         where typeLanguage.SystemOptionId == tour.TourTypeID && typeLanguage.LanguageID == languageId
                                                                         select new TourTypeDto()
                                                                         {
                                                                             TypeName = typeLanguage.Name
                                                                         }).FirstOrDefault(),
                                                             TourId = tour.Id
                                                         }).ToList()
                                            }).FirstOrDefault();

            return value;
        }
    }
}
