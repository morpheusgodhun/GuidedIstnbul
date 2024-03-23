using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Core.StaticValues.Route;
using Data.Migrations;



//using Data.Migrations;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Service.Services;
using System.Text.Json;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelBlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IBlogLanguageService _blogLanguageService;
        private readonly IBlogCategoryLanguageService _blogCategoryLanguageService;
        private readonly IManyBlogTagService _manyBlogTagService;
        private readonly IMany_Blog_RecomendedTourService _many_Blog_RecomendedTourService;
        private readonly IMany_Blog_BlogCategoryService _blog_BlogCategoryService;
        private readonly IProductService _productService;
        private readonly IRouteService _routeService;

        public PanelBlogController(IBlogService blogService, IBlogLanguageService blogLanguageService, IBlogCategoryLanguageService blogCategoryLanguageService, IManyBlogTagService manyBlogTagService, IMany_Blog_RecomendedTourService many_Blog_RecomendedTourService, IProductService productService, IRouteService routeService, IMany_Blog_BlogCategoryService blog_BlogCategoryService)
        {
            _blogService = blogService;
            _blogLanguageService = blogLanguageService;
            _blogCategoryLanguageService = blogCategoryLanguageService;
            _manyBlogTagService = manyBlogTagService;
            _many_Blog_RecomendedTourService = many_Blog_RecomendedTourService;
            _productService = productService;
            _routeService = routeService;
            _blog_BlogCategoryService = blog_BlogCategoryService;
        }

        [HttpGet]
        public CustomResponseDto<List<BlogListDto>> BlogList()
        {
            List<BlogListDto> blogListDtos = (from blog in _blogService.GetAll(x => !x.IsDeleted)
                                              join blogLanguage in _blogLanguageService.GetAll(x => !x.IsDeleted)
                                              on blog.Id equals blogLanguage.BlogID
                                              where blogLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                              select new BlogListDto()
                                              {
                                                  BlogID = blog.Id,
                                                  Status = blog.Status,
                                                  ShowOnFaq = blog.ShowOnFAQ,
                                                  ShowOnHomepage = blog.ShowOnHomepage,
                                                  BlogTitle = blogLanguage.BlogTitle,
                                                  Date = blog.Date,
                                                  BlogCategory = _blogCategoryLanguageService.Where(x => x.LanguageID == LanguageList.BaseLanguage.Id && x.BlogCategoryID == blog.BlogCategoryID).FirstOrDefault().CategoryName
                                              }).ToList();


            return CustomResponseDto<List<BlogListDto>>.Success(200, blogListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddBlog(AddBlogDto addBlogDto)
        {
            Guid blogCategoryId = addBlogDto.BlogCategoryIDs.FirstOrDefault();
            Blog blog = new()
            {
                BlogCategoryID = blogCategoryId,
                Date = addBlogDto.Date,
                BannerImagePath = addBlogDto.BannerImagePath,
                CardImagePath = addBlogDto.CardImagePath,
                ShowOnHomepage = false,
                ShowOnFAQ = false
            };

            _blogService.Add(blog);

            foreach (var language in LanguageList.AllLanguages)
            {
                BlogLanguageItem blogLanguageItem = new()
                {
                    BlogID = blog.Id,
                    LanguageID = language.Id,
                    BlogTitle = addBlogDto.BlogTitle,
                    Slug = addBlogDto.Slug,
                    ShortDescription = addBlogDto.ShortDescription,
                    Content1 = addBlogDto.Content1,
                    Content2 = string.IsNullOrEmpty(addBlogDto.Content2) ? "" : addBlogDto.Content2,
                    SitemapInclude = addBlogDto.SitemapInclude,
                };

                _blogLanguageService.Add(blogLanguageItem);
            }

            foreach (var tagId in addBlogDto.TagIDs)
            {
                Many_Blog_Tag tag = new()
                {
                    BlogID = blog.Id,
                    TagID = tagId,

                };
                _manyBlogTagService.Add(tag);
            }

            foreach (var productID in addBlogDto.RecommendedTourIDs)
            {
                Many_Blog_RecomendedTour many = new()
                {
                    BlogID = blog.Id,
                    ProductID = productID,
                };
                _many_Blog_RecomendedTourService.Add(many);
            }
            foreach (var blogCategory in addBlogDto.BlogCategoryIDs)
            {
                Many_Blog_BlogCategory manyBlogCategory = new()
                {
                    BlogCategoryId = blogCategory,
                    BlogId = blog.Id,
                };
                _blog_BlogCategoryService.Add(manyBlogCategory);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditBlogDto> EditBlog(Guid id)
        {
            Blog blog = _blogService.GetById(id);
            BlogLanguageItem blogLanguageItem = _blogLanguageService.Where(x => x.LanguageID == 1 && x.BlogID == id).FirstOrDefault();


            EditBlogDto editBlogDto = new EditBlogDto()
            {
                BlogID = blog.Id,
                BlogCategoryID = blog.BlogCategoryID,
                Date = blog.Date,
                CardImagePath = blog.CardImagePath,
                BannerImagePath = blog.BannerImagePath,
                TagIDs = (from tag in _manyBlogTagService.GetAll(x => !x.IsDeleted)
                          where tag.BlogID == blog.Id
                          select tag.TagID).ToList(),
                RecommendedTourIDs = (from many in _many_Blog_RecomendedTourService.GetAll(x => x.BlogID == id)
                                      join product in _productService.GetAll()
                                      on many.ProductID equals product.Id
                                      select product.Id).ToList(),
                BlogTitle = blogLanguageItem.BlogTitle
            };

            return CustomResponseDto<EditBlogDto>.Success(200, editBlogDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditBlog(EditBlogDto editBlogDto)
        {

            Blog blog = _blogService.GetById(editBlogDto.BlogID);


            blog.BlogCategoryID = editBlogDto.BlogCategoryID;
            blog.Date = editBlogDto.Date;
            blog.CardImagePath = editBlogDto.CardImagePath;
            blog.BannerImagePath = editBlogDto.BannerImagePath;


            var oldTags = _manyBlogTagService.Where(x => x.BlogID == editBlogDto.BlogID).ToList();

            foreach (var item in oldTags)
            {
                _manyBlogTagService.Remove(item);
            }

            foreach (var item in editBlogDto.TagIDs)
            {
                Many_Blog_Tag blogTag = new()
                {
                    BlogID = editBlogDto.BlogID,
                    TagID = item
                };

                _manyBlogTagService.Add(blogTag);
            }

            var oldTours = _many_Blog_RecomendedTourService.Where(x => x.BlogID == editBlogDto.BlogID).ToList();


            foreach (var tour in oldTours)
            {
                _many_Blog_RecomendedTourService.Remove(tour);
            }

            foreach (var tourID in editBlogDto.RecommendedTourIDs)
            {
                Many_Blog_RecomendedTour many = new()
                {
                    ProductID = tourID,
                    BlogID = blog.Id
                };
                _many_Blog_RecomendedTourService.Add(many);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditBlogDto> LanguageEditBlog(Guid id, int languageId)
        {
            BlogLanguageItem blogLanguageItem = _blogLanguageService.Where(x => x.LanguageID == languageId && x.BlogID == id).FirstOrDefault();
            Core.Entities.Route? route = _routeService.Where(x => x.EntityId == id).FirstOrDefault();
            LanguageEditBlogDto languageEditBlogDto = new LanguageEditBlogDto()
            {
                BlogLanguageID = blogLanguageItem.Id,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                BlogTitle = blogLanguageItem.BlogTitle,
                Slug = blogLanguageItem.Slug,
                ShortDescription = blogLanguageItem.ShortDescription,
                Content1 = blogLanguageItem.Content1,
                Content2 = blogLanguageItem.Content2,
                SitemapInclude = route is not null ? route.SitemapInclude : false


            };


            return CustomResponseDto<LanguageEditBlogDto>.Success(204, languageEditBlogDto);
        }



        [HttpPost]
        public CustomResponseNullDto LanguageEditBlog(LanguageEditBlogDto languageEditBlogDto)
        {
            BlogLanguageItem blogLanguageItem = _blogLanguageService.GetById(languageEditBlogDto.BlogLanguageID);

            blogLanguageItem.BlogTitle = languageEditBlogDto.BlogTitle;

            blogLanguageItem.Slug = languageEditBlogDto.Slug;
            blogLanguageItem.ShortDescription = !String.IsNullOrEmpty(languageEditBlogDto.ShortDescription) ? languageEditBlogDto.ShortDescription : string.Empty;
            blogLanguageItem.Content1 = !String.IsNullOrEmpty(languageEditBlogDto.Content1) ? languageEditBlogDto.Content1 : string.Empty;
            blogLanguageItem.Content2 = !String.IsNullOrEmpty(languageEditBlogDto.Content2) ? languageEditBlogDto.Content2 : string.Empty;
            blogLanguageItem.LanguageID = LanguageList.AllLanguages.FirstOrDefault(x => x.Name == languageEditBlogDto.LanguageName).Id;
            blogLanguageItem.SitemapInclude = languageEditBlogDto.SitemapInclude;
            _blogLanguageService.Update(blogLanguageItem);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleBlogStatus(Guid id)
        {

            _blogService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);
        }


        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleShowOnFaq(Guid id)
        {
            Blog blog = _blogService.GetById(id);
            blog.ShowOnFAQ = !blog.ShowOnFAQ;
            _blogService.Update(blog);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleShowOnHomepage(Guid id)
        {
            Blog blog = _blogService.GetById(id);
            blog.ShowOnHomepage = !blog.ShowOnHomepage;
            _blogService.Update(blog);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<List<BlogCommentListDto>> BlogCommentList(string id)
        {
            List<BlogCommentListDto> blogCommentListDtos = new List<BlogCommentListDto>();

            if (id == "1")
            {
                blogCommentListDtos = new List<BlogCommentListDto>()
                {
                    new BlogCommentListDto()
                    {
                        BlogID = id,
                        BlogCommentID = "1",
                        SendDate = DateTime.Now.AddDays(-30),
                        Status = true,
                        SenderName = "Ecmel SADIKOĞLU",
                        SenderEmail = "ecmel@gmail.com",
                        Message = "Ramazan bayramının hissettirdiği eşsiz hisler için bile Türkiye'ye gitmeye değer.",
                        ToAnswer = null
                    },
                    new BlogCommentListDto()
                    {
                        BlogID = id,
                        BlogCommentID = "2",
                        SendDate = DateTime.Now.AddDays(-20),
                        Status = true,
                        SenderName = "Melek ÖZTÜRK",
                        SenderEmail = "melek@gmail.com",
                        Message = "Bence de. Çok haklısınız",
                        ToAnswer = "Ecmel SADIKOĞLU"
                    }
                };
            }
            if (id == "2")
            {
                blogCommentListDtos = new List<BlogCommentListDto>()
                {
                    new BlogCommentListDto()
                    {
                        BlogID = id,
                        BlogCommentID = "3",
                        SendDate = DateTime.Now.AddDays(-53),
                        Status = false,
                        SenderName = "Ahmet DURMUŞ",
                        SenderEmail = "ahmet@gmail.com",
                        Message = "Daha önce Sümelaya iki defa gittim ama böyle sırları olduğunu ilk kez öğreniyorum. Bilgilendirme için teşekkürler.",
                        ToAnswer = null
                    },
                    new BlogCommentListDto()
                    {
                        BlogID = id,
                        BlogCommentID = "4",
                        SendDate = DateTime.Now.AddDays(-12),
                        Status = true,
                        SenderName = "Hasan TÜREMİŞ",
                        SenderEmail = "hasan@gmail.com",
                        Message = "Sümela manastırı kesinlikle görülmeye değer bir yer. Herkese tavsiye ediyorum.",
                        ToAnswer = null
                    }
                };
            }
            if (id == "3")
            {
                blogCommentListDtos = new List<BlogCommentListDto>()
                {
                    new BlogCommentListDto()
                    {
                        BlogID = id,
                        BlogCommentID = "5",
                        SendDate = DateTime.Now.AddDays(-27),
                        Status = true,
                        SenderName = "Hayat GÜLÜMSE",
                        SenderEmail = "hayat@gmail.com",
                        Message = "Türkler bu konuda çok hassaslar. Dini değerlerine saygı duymak gerekiyor.",
                        ToAnswer = null
                    },
                    new BlogCommentListDto()
                    {
                        BlogID = id,
                        BlogCommentID = "6",
                        SendDate = DateTime.Now.AddDays(-3),
                        Status = false,
                        SenderName = "Duru Su SESSİZ",
                        SenderEmail = "duru@gmail.com",
                        Message = "İsminiz gibi düşünceleriniz de çok nazik. Mutlu bir hayat geçirmeniz dileğiyle...",
                        ToAnswer = "Hayat GÜLÜMSE"
                    }
                };
            }

            return CustomResponseDto<List<BlogCommentListDto>>.Success(200, blogCommentListDtos);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleBlogCommentStatus(string id)
        {
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> BlogSelectList()
        {

            List<SelectListOptionDto> blogSelectList = (from blog in _blogService.GetAll(x => !x.IsDeleted)
                                                        join languageItem in _blogLanguageService.GetAll()
                                                        on blog.Id equals languageItem.BlogID
                                                        where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                        select new SelectListOptionDto()
                                                        {
                                                            OptionID = blog.Id,
                                                            Option = languageItem.BlogTitle
                                                        }).ToList();



            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, blogSelectList);
        }
    }
}
