using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PageService : GenericService<Page>, IPageService
    {
        private readonly IPageRepository _pageRepository;

        private readonly IFaqCategoryRepository _faqCategoryRepository;
        private readonly IFaqLanguageRepository _faqLanguageRepository;
        private readonly IFaqCategoryLanguageRepository _faqCategoryLanguageRepository;
        private readonly IFaqRepository _faqRepository;
        public PageService(IGenericRepository<Page> repository, IUnitOfWork unitOfWork, IPageRepository pageRepository, IFaqCategoryRepository faqCategoryRepository, IFaqService faqService, IFaqCategoryLanguageRepository faqCategoryLanguageRepository, IFaqLanguageRepository faqLanguageRepository, IFaqRepository faqRepository) : base(repository, unitOfWork)
        {
            _pageRepository = pageRepository;
            _faqCategoryRepository = faqCategoryRepository;
            _faqCategoryLanguageRepository = faqCategoryLanguageRepository;
            _faqLanguageRepository = faqLanguageRepository;
            _faqRepository = faqRepository;
        }

        public PageDto GetByPageName(string PageName, int languageId)
        {
            return _pageRepository.GetByPageName(PageName, languageId);
        }

        public PageDto GetSlugByPageName(string pageName, int languageId)
        {
            return _pageRepository.GetSlugByPageName(pageName, languageId);
        }
        public string GetDisplayNameByPageName(string pageName, int languageId)
        {
            return _pageRepository.GetDisplayNameByPageName(pageName, languageId);
        }

        public Dictionary<string, string> GetPagesAndUrls(int languageId)
        {
            return _pageRepository.GetPagesAndUrls(languageId);
        }

        public PageDto GetPageById(string id, int languageId)
        {
            Page page = _pageRepository.GetById(new Guid(id));
            PageLanguageItem langItem = page.PageLanguageItems.FirstOrDefault(x => x.LanguageId == languageId);
            PageDto pageDto = new()
            {
                BannerImagePath = page.ImagePath,
                Content = langItem.Content,
                Subtitle = langItem.Subtitle,
                Title = langItem.Title
            };
            if (_faqCategoryRepository.Where(x => x.PageId == page.Id).FirstOrDefault() is not null)
            {
                var data = (from category in _faqCategoryRepository.GetAll(x => x.Status)
                            join categoryLanguage in _faqCategoryLanguageRepository.GetAll(x => x.Status)
                            on category.Id equals categoryLanguage.FaqCategoryID
                            where categoryLanguage.LangaugeID == languageId && category.PageId == page.Id
                            select new FaqCategoryDto()
                            {
                                FaqCategoryName = categoryLanguage.DisplayName,
                                Order = category.Order,
                                Faqs = (from faq in _faqRepository.GetAll()
                                        where faq.FaqCategoryID == category.Id && faq.Status
                                        join faqLanguageItem in _faqLanguageRepository.GetAll(x => x.Status)
                                        on faq.Id equals faqLanguageItem.FaqID
                                        where faqLanguageItem.LangaugeID == languageId
                                        select new FaqDto()
                                        {
                                            Question = faqLanguageItem.Question,
                                            Answer = faqLanguageItem.Answer,
                                            Order = faq.Order

                                        }).ToList()
                            }).ToList();
                pageDto.FaqCategories = data;
            }


            return pageDto;
        }

        public string GetPageSlug(string pageName, int languageId)
        {
            return _pageRepository.GetPageSlug(pageName, languageId);
        }

         
    }
}
