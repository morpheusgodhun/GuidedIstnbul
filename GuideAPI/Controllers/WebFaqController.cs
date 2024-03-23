using Core.IService;
using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebFaqController : ControllerBase
    {
        private readonly IFaqCategoryLanguageService _faqCategoryLanguageService;
        private readonly IFaqCategoryService _faqCategoryService;
        private readonly IFaqLanguageService _faqLanguageService;
        private readonly IFaqService _faqService;
        private readonly IConstantValueService _constantValueService;
        private readonly IConstantValueLanguageService _constantValueLanguageService;
        private readonly IPageService _pageService;
        private readonly IBlogService _blogService;

        public WebFaqController(IFaqCategoryLanguageService faqCategoryLanguageService, IFaqCategoryService faqCategoryService, IFaqLanguageService faqLanguageService, IFaqService faqService, IConstantValueService constantValueService, IConstantValueLanguageService constantValueLanguageService, IPageService pageService, IBlogService blogService)
        {
            _faqCategoryLanguageService = faqCategoryLanguageService;
            _faqCategoryService = faqCategoryService;
            _faqLanguageService = faqLanguageService;
            _faqService = faqService;
            _constantValueService = constantValueService;
            _constantValueLanguageService = constantValueLanguageService;
            _pageService = pageService;
            _blogService = blogService;
        }

        [HttpGet("{LanguageID}")]
        public async Task<CustomResponseDto<GetFaqDto>> GetFaq(int LanguageID)
        {
            var cats = _faqCategoryService.GetAll(x => x.Status);
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Faq", LanguageID);

            GetFaqDto getFaqDto = new GetFaqDto()
            {
                ConstantValues = constantValues,
                BannerImagePath = _pageService.GetById(new Guid("b3a959ba-0c9a-4510-bd95-042e68001172")).ImagePath,
                FaqCategories = (from category in _faqCategoryService.GetAll(x => x.Status)
                                 join categoryLanguage in _faqCategoryLanguageService.GetAll(x => x.Status)
                                 on category.Id equals categoryLanguage.FaqCategoryID
                                 where categoryLanguage.LangaugeID == LanguageID && category.PageId == new Guid("b3a959ba-0c9a-4510-bd95-042e68001172")
                                 select new FaqCategoryDto()
                                 {
                                     FaqCategoryName = categoryLanguage.DisplayName,
                                     Order = category.Order,
                                     Faqs = (from faq in _faqService.GetAll()
                                             where faq.FaqCategoryID == category.Id && faq.Status
                                             join faqLanguageItem in _faqLanguageService.GetAll(x => x.Status)
                                             on faq.Id equals faqLanguageItem.FaqID
                                             where faqLanguageItem.LangaugeID == LanguageID
                                             select new FaqDto()
                                             {
                                                 Question = faqLanguageItem.Question,
                                                 Answer = faqLanguageItem.Answer,
                                                 Order = faq.Order

                                             }).ToList()
                                 }).ToList(),
                Blogs = _blogService.FaqBlogList(LanguageID)
            };

            return CustomResponseDto<GetFaqDto>.Success(200, getFaqDto);
        }



    }
}
