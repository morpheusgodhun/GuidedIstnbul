using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Data.Migrations;
using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        Context _context;
        DbSet<Page> _pages;
        DbSet<PageLanguageItem> _pageLanguageItems;
        IConstantValueService _constantValueService;
        IConstantValueLanguageService _constantValueLanguageService;
        public PageRepository(Context context, IConstantValueService constantValueService, IConstantValueLanguageService constantValueLanguageService) : base(context)
        {
            _context = context;
            _pages = _context.Pages;
            _pageLanguageItems = _context.PageLanguageItems;
            _constantValueService = constantValueService;
            _constantValueLanguageService = constantValueLanguageService;
        }

        public PageDto GetByPageName(string PageName, int languageId)
        {
            PageDto pageDto = (from page in _pages.ToList()
                               where page.PageName == PageName
                               join pageLanguage in _pageLanguageItems.ToList()
                  on page.Id equals pageLanguage.PageID
                               where pageLanguage.LanguageId == languageId
                               select new PageDto()
                               {
                                   BannerImagePath = page.ImagePath,
                                   Title = pageLanguage.Title,
                                   Subtitle = pageLanguage.Subtitle,
                                   Content = pageLanguage.Content
                               }).FirstOrDefault();
            return pageDto;
        }

        public string GetDisplayNameByPageName(string pageName, int languageId)
        {
            var displayName = (from page in _pages.ToList()
                               where page.PageName == pageName
                               join pageLanguage in _pageLanguageItems.ToList()
                               on page.Id equals pageLanguage.PageID
                               where pageLanguage.LanguageId == languageId
                               select pageLanguage.DisplayName).FirstOrDefault();

            return displayName;

        }
        public Dictionary<string, string> GetPagesAndUrls(int languageId)
        {
            //var displayName = (from page in _pages.ToList()
            //                   join pageLanguage in _pageLanguageItems.ToList()
            //                   on page.Id equals pageLanguage.PageID
            //                   where pageLanguage.LanguageId == languageId
            //                   select pageLanguage.DisplayName pageLanguage.DisplayName).FirstOrDefault();

            return null;
        }


        public override Page GetById(Guid id)
        {
            Page page = _context.Pages.Include(x => x.PageLanguageItems).Where(x => x.Id == id).FirstOrDefault();
            return page;
        }

        public string GetPageSlug(string pageName, int languageId)
        {
            var displayName = (from page in _pages.ToList()
                               where page.PageName == pageName
                               join pageLanguage in _pageLanguageItems.ToList()
                               on page.Id equals pageLanguage.PageID
                               where pageLanguage.LanguageId == languageId
                               select pageLanguage.Slug).FirstOrDefault();

            return displayName;
        }

        public PageDto GetSlugByPageName(string pageName, int languageId)
        {
            PageDto pageDto = (from page in _pages.ToList()
                               where page.PageName == pageName
                               join pageLanguage in _pageLanguageItems.ToList()
                  on page.Id equals pageLanguage.PageID
                               where pageLanguage.LanguageId == languageId
                               select new PageDto()
                               {
                                   Slug = pageLanguage.Slug
                               }).FirstOrDefault();
            return pageDto;
            //ConstantValues = await _constantValueService.GetConstantValueByPageNameAsync("Apply As Guide", languageID),

        }

        public async Task<List<string>> GetPageSlugsForSitemapAsync(int languageId)
        {
            var pageLanguageItems = _pageLanguageItems.Where(x => x.LanguageId == languageId);
            var sitemapPages = _pages.Where(x => x.IncludeSitemap);

            List<string> urls = await (from pageLanguageItem in pageLanguageItems
                                       join page in sitemapPages on
                                       pageLanguageItem.PageID equals page.Id
                                       select pageLanguageItem.Slug
                                 ).ToListAsync();

            return urls;
        }

        //public async Task<List<FooterDto>> FooterPagesInfo(int languageId)
        //{
        //    //var values = await _constantValueService.GetConstantValueByPageNameAsync("Footer", languageId);

        //    //List<FooterDto> footerDto = (from page in _pages.ToList()
        //    //                             join pageLanguage in _pageLanguageItems.ToList()
        //    //                on page.Id equals pageLanguage.PageID
        //    //                             join constantValue in await _constantValueService.GetAllAsync()
        //    //                             on page.Id equals constantValue.PageID
        //    //                             join constantValueLanguage in await _constantValueLanguageService.GetAllAsync()
        //    //                             on constantValue.Id equals constantValueLanguage.ConstantValueID
        //    //                             //  where page
        //    //                             where pageLanguage.LanguageId == languageId
        //    //                             select new FooterDto()
        //    //                             {
        //    //                                 Slug = pageLanguage.Slug,
        //    //                                 ConstantValues = values,

        //    //                             }).ToList();
        //    //return footerDto;


        //    var values2 = (from cv in await _constantValueService.GetAllAsync()
        //                   join p in _pages on cv.PageID equals p.Id
        //                   join pli in _pageLanguageItems on p.Id equals pli.PageID
        //                   join cvli in await _constantValueLanguageService.GetAllAsync() on cv.Id equals cvli.ConstantValueID
        //                   where p.PageName == "Footer" && cvli.LanguageID == languageId
        //                   select new FooterDto()
        //                   {
        //                       Slug = pli.Slug,
        //                       SlugDict = new Dictionary<string, string>
        //           {
        //               { cv.Key, pli.Slug}
        //           },
        //                       ConstantValues = new List<ConstantValueDto>
        //           {
        //               new ConstantValueDto
        //               {
        //                   Key = cv.Key,
        //                   Value = cvli.Value
        //               }
        //           }
        //                   }).ToList();
        //    return values2;

        //}
    }
}
