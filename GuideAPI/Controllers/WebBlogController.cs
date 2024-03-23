
using Core.Entities;
using Core.IService;
using Core.StaticValues;
using Data.Migrations;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebBlogController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly ITagService _tagService;
        private readonly IConstantValueService _constantValueService;
        private readonly IConstantValueLanguageService _constantValueLanguageService;
        private readonly IBlogService _blogService;
        private readonly IBlogLanguageService _blogLanguageService;
        private readonly IManyBlogTagService _manyBlogTagService;
        private readonly ITagLanguageService _tagLanguageService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IBlogCategoryLanguageService _blogCategoryLanguageService;
        private readonly IProductLanguageService _productLanguageService;
        private readonly IMany_Blog_RecomendedTourService _many_Blog_RecomendedTourService;
        private readonly IProductService _productService;
        private readonly ITourService _tourService;
        private readonly ITourLanguageService _tourLanguageService;
        private readonly ITourPriceService _tourPriceService;
        private readonly ITourPriceItemService _tourPriceItemService;
        private readonly ISystemOptionService _systemOptionService;
        private readonly ISystemOptionLanguageService _systemOptionLanguageService;
        private readonly IMany_Blog_BlogCategoryService _many_Blog_BlogCategoryService;
        private readonly IBlogCommentService _blogCommentService;

        public WebBlogController(IPageService pageService, IConstantValueService constantValueService, IConstantValueLanguageService constantValueLanguageService, IBlogService blogService, IBlogLanguageService blogLanguageService, IManyBlogTagService manyBlogTagService, ITagLanguageService tagLanguageService, IBlogCategoryService blogCategoryService, IBlogCategoryLanguageService blogCategoryLanguageService, IProductLanguageService productLanguageService, IMany_Blog_RecomendedTourService many_Blog_RecomendedTourService, IProductService productService, ITourService tourService, ITourLanguageService tourLanguageService, ITourPriceService tourPriceService, ITourPriceItemService tourPriceItemService, ISystemOptionService systemOptionService, ISystemOptionLanguageService systemOptionLanguageService, IMany_Blog_BlogCategoryService many_Blog_BlogCategoryService, ITagService tagService, IBlogCommentService blogCommentService)
        {
            _pageService = pageService;
            _constantValueService = constantValueService;
            _constantValueLanguageService = constantValueLanguageService;
            _blogService = blogService;
            _blogLanguageService = blogLanguageService;
            _manyBlogTagService = manyBlogTagService;
            _tagLanguageService = tagLanguageService;
            _blogCategoryService = blogCategoryService;
            _blogCategoryLanguageService = blogCategoryLanguageService;
            _productLanguageService = productLanguageService;
            _many_Blog_RecomendedTourService = many_Blog_RecomendedTourService;
            _productService = productService;
            _tourService = tourService;
            _tourLanguageService = tourLanguageService;
            _tourPriceService = tourPriceService;
            _tourPriceItemService = tourPriceItemService;
            _systemOptionService = systemOptionService;
            _systemOptionLanguageService = systemOptionLanguageService;
            _many_Blog_BlogCategoryService = many_Blog_BlogCategoryService;
            _tagService = tagService;
            _blogCommentService = blogCommentService;
        }

        [HttpGet("{currentPage}/{pageSize}/{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<GetBlogListDto>> GetBlogList(int currentPage, int pageSize, int LanguageID)
        {

            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Blog List", LanguageID);

            pageSize = pageSize > 20 ? 20 : pageSize;

            //var pagedBlogs = _blogService.GetBlogListPage(currentPage, pageSize, 1);


            var blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
                         join blogLanguage in _blogLanguageService.GetAll()
                         on blog.Id equals blogLanguage.BlogID
                         where blogLanguage.LanguageID == LanguageID
                         select new BlogListItemDto()
                         {
                             BlogID = blog.Id,
                             Title = blogLanguage.BlogTitle,
                             ImagePath = blog.CardImagePath,
                             Date = blog.Date,
                             CommentCount = _blogCommentService.Where(x => x.BlogId == blog.Id).Count,
                             ShortDescription = blogLanguage.ShortDescription,
                             Slug = blogLanguage.Slug,
                             CategoryDto = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                            join blogCategory in _blogCategoryService.GetAll()
                                            on categoryLanguage.BlogCategoryID equals blogCategory.Id
                                            where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
                                            select new CategoryDto
                                            {
                                                ID = categoryLanguage.Id,
                                                Name = categoryLanguage.CategoryName,
                                                Slug = categoryLanguage.Slug
                                            }).FirstOrDefault(),

                             Tags = (from many in _manyBlogTagService.GetAll(x => x.BlogID == blog.Id)
                                     join tagLanguage in _tagLanguageService.GetAll()
                                     on many.TagID equals tagLanguage.TagID
                                     where tagLanguage.LangaugeID == LanguageID
                                     select new TagDto()
                                     {
                                         ID = many.TagID,
                                         Name = tagLanguage.DisplayName,
                                         Slug = tagLanguage.Slug
                                     }).ToList(),

                             Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID && x.BlogCategoryID == blog.BlogCategoryID)
                                         select categoryLanguage.CategoryName).FirstOrDefault()
                         }).OrderByDescending(x => x.Date).ToList();

            var totalCount = blogs.Count;


            var getBlogListDto = new GetBlogListDto()
            {
                BlogListPage = new PageDto()
                {
                    Title = "Blog List",
                    BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
                },
                ConstantValues = constantValues,
                Blogs = blogs.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                Paginate = new()
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = totalCount//pagedBlogs.Count //todo : totaol al 
                }

            };

            #region eski kod
            //            var getBlogListDto = new GetBlogListDto()
            //            {

            //                BlogListPage = new PageDto()
            //                {
            //                    Title = "Blog List",
            //                    BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
            //                },
            //                ConstantValues = constantValues,
            //                Blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
            //                         join blogLanguage in _blogLanguageService.GetAll()
            //                         on blog.Id equals blogLanguage.BlogID
            //                         where blogLanguage.LanguageID == LanguageID

            //                         select new BlogListItemDto()
            //                         {
            //                             BlogID = blog.Id,
            //                             Title = blogLanguage.BlogTitle,
            //                             ImagePath = blog.CardImagePath,
            //                             Date = blog.Date,
            //                             CommentCount = 5,
            //                             ShortDescription = blogLanguage.ShortDescription,
            //                             Slug = blogLanguage.Slug,
            //                             CategoryDto = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                                            join blogCategory in _blogCategoryService.GetAll()
            //                                            on categoryLanguage.BlogCategoryID equals blogCategory.Id
            //                                            where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
            //                                            select new CategoryDto
            //                                            {
            //                                                ID = categoryLanguage.Id,
            //                                                Name = categoryLanguage.CategoryName,
            //                                                Slug = categoryLanguage.Slug
            //                                            }).FirstOrDefault(),

            //                             Tags = (from many in _manyBlogTagService.GetAll()
            //                                     join tagLanguage in _tagLanguageService.GetAll()
            //                                     on many.TagID equals tagLanguage.TagID
            //                                     where tagLanguage.LangaugeID == LanguageID
            //                                     select new TagDto()
            //                                     {
            //                                         ID = many.TagID,
            //                                         Name = tagLanguage.DisplayName,
            //                                         Slug = tagLanguage.Slug
            //                                     }).ToList(),

            //                             Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID && x.BlogCategoryID == blog.BlogCategoryID)
            //                                         select categoryLanguage.CategoryName).FirstOrDefault()



            //                             //Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                             //                         select categoryLanguage.CategoryName).FirstOrDefault()
            //                         }).ToList(),

            //CurrentPage = currentPage,
            //                PageSize = pageSize,
            //                TotalCount =Blogs.Count,
            //            };

            #endregion




            return CustomResponseDto<GetBlogListDto>.Success(200, getBlogListDto);
        }


        [HttpGet("{categoryId}/{currentPage}/{pageSize}/{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<GetBlogListDto>> GetBlogListByCategoryId(Guid categoryId, int currentPage, int pageSize, int LanguageID)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Blog List", LanguageID);
            pageSize = pageSize > 20 ? 20 : pageSize;

            var manyBlogs = await _many_Blog_BlogCategoryService.GetAllAsync(x => x.BlogCategoryId == categoryId);

            var blogLanguageItems = await _blogLanguageService.GetAllAsync();
            var blogCategories = await _blogCategoryService.GetAllAsync();
            var blogCategoryLanguageItems = await _blogCategoryLanguageService.GetAllAsync(x => x.LanguageID == LanguageID);
            var tagLanguageItems = await _tagLanguageService.GetAllAsync(x => x.LangaugeID == LanguageID);


            var blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
                         join blogLanguage in blogLanguageItems
                         on blog.Id equals blogLanguage.BlogID
                         join manyBlogItem in manyBlogs
                         on blog.Id equals manyBlogItem.BlogId
                         where blogLanguage.LanguageID == LanguageID
                         select new BlogListItemDto()
                         {
                             BlogID = blog.Id,
                             Title = blogLanguage.BlogTitle,
                             ImagePath = blog.CardImagePath,
                             Date = blog.Date,
                             CommentCount = _blogCommentService.Where(x => x.BlogId == blog.Id).Count,
                             ShortDescription = blogLanguage.ShortDescription,
                             Slug = blogLanguage.Slug,
                             CategoryDto = (from categoryLanguage in blogCategoryLanguageItems
                                            join blogCategory in blogCategories
                                            on categoryLanguage.BlogCategoryID equals blogCategory.Id
                                            where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
                                            select new CategoryDto
                                            {
                                                ID = categoryLanguage.Id,
                                                Name = categoryLanguage.CategoryName,
                                                Slug = categoryLanguage.Slug
                                            }).FirstOrDefault(),

                             Tags = (from many in _manyBlogTagService.GetAll(x => x.BlogID == blog.Id)
                                     join tagLanguage in tagLanguageItems
                                     on many.TagID equals tagLanguage.TagID
                                     where tagLanguage.LangaugeID == LanguageID
                                     select new TagDto()
                                     {
                                         ID = many.TagID,
                                         Name = tagLanguage.DisplayName,
                                         Slug = tagLanguage.Slug
                                     }).ToList(),

                             Category = (from categoryLanguage in
                                         blogCategoryLanguageItems.Where(x => x.BlogCategoryID == blog.BlogCategoryID)
                                         select categoryLanguage.CategoryName).FirstOrDefault(),

                             //Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
                             //                         select categoryLanguage.CategoryName).FirstOrDefault()
                         }).OrderByDescending(x => x.Date).ToList();

            var totalCount = blogs.Count;
            string title = _blogCategoryLanguageService.Where(x => x.LanguageID == LanguageID && x.BlogCategoryID == categoryId).FirstOrDefault()?.CategoryName;

            var getBlogListDto = new GetBlogListDto()
            {
                BlogListPage = new PageDto()
                {
                    Title = string.IsNullOrEmpty(title) ? "Blog List" : title,
                    BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
                },
                ConstantValues = constantValues,
                Blogs = blogs.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                Paginate = new()
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = totalCount
                }
            };

            #region ........

            //var getBlogListDto = new GetBlogListDto()
            //{

            //    BlogListPage = new PageDto()
            //    {
            //        Title = "Blog List",
            //        BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
            //    },
            //    ConstantValues = constantValues,
            //    Blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
            //             join blogLanguage in _blogLanguageService.GetAll()
            //             on blog.Id equals blogLanguage.BlogID
            //             where blogLanguage.LanguageID == LanguageID

            //             select new BlogListItemDto()
            //             {
            //                 BlogID = blog.Id,
            //                 Title = blogLanguage.BlogTitle,
            //                 ImagePath = blog.CardImagePath,
            //                 Date = blog.Date,
            //                 CommentCount = 5,
            //                 ShortDescription = blogLanguage.ShortDescription,
            //                 Slug = blogLanguage.Slug,
            //                 CategoryDto = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                                join blogCategory in _blogCategoryService.GetAll()
            //                                on categoryLanguage.BlogCategoryID equals blogCategory.Id
            //                                where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
            //                                select new CategoryDto
            //                                {
            //                                    ID = categoryLanguage.Id,
            //                                    Name = categoryLanguage.CategoryName,
            //                                    Slug = categoryLanguage.Slug
            //                                }).FirstOrDefault(),


            //                 Tags = (from many in _manyBlogTagService.GetAll(x => x.BlogID == blog.Id)
            //                         join tagLanguage in _tagLanguageService.GetAll()
            //                         on many.TagID equals tagLanguage.TagID
            //                         where tagLanguage.LangaugeID == LanguageID
            //                         select new TagDto()
            //                         {
            //                             ID = many.TagID,
            //                             Name = tagLanguage.DisplayName,
            //                             Slug = tagLanguage.Slug
            //                         }).ToList(),



            //                 Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID && x.BlogCategoryID == blog.BlogCategoryID)
            //                             select categoryLanguage.CategoryName).FirstOrDefault(),



            //                 //Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                 //                         select categoryLanguage.CategoryName).FirstOrDefault()
            //             }).ToList();
            #endregion

            #region eski kod 
            //var getBlogListDto = new GetBlogListDto()
            //{

            //    BlogListPage = new PageDto()
            //    {
            //        Title = "Blog List",
            //        BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
            //    },
            //    ConstantValues = constantValues,
            //    Blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
            //             join blogLanguage in _blogLanguageService.GetAll()
            //             on blog.Id equals blogLanguage.BlogID
            //             where blogLanguage.LanguageID == LanguageID && blog.BlogCategoryID == categoryId
            //             select new BlogListItemDto()
            //             {
            //                 BlogID = blog.Id,
            //                 Title = blogLanguage.BlogTitle,
            //                 ImagePath = blog.CardImagePath,
            //                 Date = blog.Date,
            //                 CommentCount = 5,
            //                 ShortDescription = blogLanguage.ShortDescription,
            //                 Slug = blogLanguage.Slug,

            //                 Tags = (from many in _manyBlogTagService.GetAll()
            //                         join tagLanguage in _tagLanguageService.GetAll()
            //                         on many.TagID equals tagLanguage.TagID
            //                         where tagLanguage.LangaugeID == LanguageID
            //                         select new TagDto()
            //                         {
            //                             ID = many.TagID,
            //                             Name = tagLanguage.DisplayName,
            //                             Slug = tagLanguage.Slug
            //                         }).ToList(),

            //                 Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID && x.BlogCategoryID == blog.BlogCategoryID)
            //                             select categoryLanguage.CategoryName).FirstOrDefault(),



            //                 //Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                 //                         select categoryLanguage.CategoryName).FirstOrDefault()
            //             }).ToList()

            //};
            #endregion


            return CustomResponseDto<GetBlogListDto>.Success(200, getBlogListDto);
        }


        [HttpGet("{tagId}/{currentPage}/{pageSize}/{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<GetBlogListDto>> GetBlogListByTagId(Guid tagId, int currentPage, int pageSize, int LanguageID)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Blog List", LanguageID);
            pageSize = pageSize > 20 ? 20 : pageSize;
            var blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
                         join blogLanguage in _blogLanguageService.GetAll()
                         on blog.Id equals blogLanguage.BlogID
                         join manyBlogTag in _manyBlogTagService.GetAll(x => x.TagID == tagId)
                         on blog.Id equals manyBlogTag.BlogID
                         where (blogLanguage.LanguageID == LanguageID)
                         select new BlogListItemDto()
                         {
                             BlogID = blog.Id,
                             Title = blogLanguage.BlogTitle,
                             ImagePath = blog.CardImagePath,
                             Date = blog.Date,
                             CommentCount = _blogCommentService.Where(x => x.BlogId == blog.Id).Count,
                             ShortDescription = blogLanguage.ShortDescription,
                             Slug = blogLanguage.Slug,
                             CategoryDto = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                            join blogCategory in _blogCategoryService.GetAll()
                                            on categoryLanguage.BlogCategoryID equals blogCategory.Id
                                            where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
                                            select new CategoryDto
                                            {
                                                ID = categoryLanguage.Id,
                                                Name = categoryLanguage.CategoryName,
                                                Slug = categoryLanguage.Slug
                                            }).FirstOrDefault(),
                             Tags = (from many in _manyBlogTagService.GetAll(x => x.BlogID == blog.Id)
                                     join tagLanguage in _tagLanguageService.GetAll()
                                     on many.TagID equals tagLanguage.TagID
                                     where tagLanguage.LangaugeID == LanguageID
                                     select new TagDto()
                                     {
                                         ID = many.TagID,
                                         Name = tagLanguage.DisplayName,
                                         Slug = tagLanguage.Slug
                                     }).ToList(),

                             Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID && x.BlogCategoryID == blog.BlogCategoryID)
                                         select categoryLanguage.CategoryName).FirstOrDefault(),



                             //Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
                             //                         select categoryLanguage.CategoryName).FirstOrDefault()
                         }).OrderByDescending(x => x.Date).ToList();

            var totalCount = blogs.Count();
            var getBlogListDto = new GetBlogListDto()
            {
                BlogListPage = new PageDto()
                {
                    Title = "Blog List",
                    BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
                },
                ConstantValues = constantValues,
                Blogs = blogs.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                Paginate = new PaginateDto
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = totalCount
                }
            };

            #region eski kod 
            //var getBlogListDto = new GetBlogListDto()
            //{

            //    BlogListPage = new PageDto()
            //    {
            //        Title = "Blog List",
            //        BannerImagePath = _pageService.GetAll(x => x.PageName == "Blog List").FirstOrDefault().ImagePath
            //    },
            //    ConstantValues = constantValues,
            //    Blogs = (from blog in _blogService.GetAll(x => !x.IsDeleted)
            //             join blogLanguage in _blogLanguageService.GetAll()
            //             on blog.Id equals blogLanguage.BlogID
            //             join manyBlogTag in _manyBlogTagService.GetAll(x => x.TagID == tagId)
            //             on blog.Id equals manyBlogTag.BlogID
            //             where (blogLanguage.LanguageID == LanguageID)
            //             select new BlogListItemDto()
            //             {
            //                 BlogID = blog.Id,
            //                 Title = blogLanguage.BlogTitle,
            //                 ImagePath = blog.CardImagePath,
            //                 Date = blog.Date,
            //                 CommentCount = 5,
            //                 ShortDescription = blogLanguage.ShortDescription,
            //                 Slug = blogLanguage.Slug,
            //                 CategoryDto = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                                join blogCategory in _blogCategoryService.GetAll()
            //                                on categoryLanguage.BlogCategoryID equals blogCategory.Id
            //                                where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
            //                                select new CategoryDto
            //                                {
            //                                    ID = categoryLanguage.Id,
            //                                    Name = categoryLanguage.CategoryName,
            //                                    Slug = categoryLanguage.Slug
            //                                }).FirstOrDefault(),
            //                 Tags = (from many in _manyBlogTagService.GetAll(x => x.BlogID == blog.Id)
            //                         join tagLanguage in _tagLanguageService.GetAll()
            //                         on many.TagID equals tagLanguage.TagID
            //                         where tagLanguage.LangaugeID == LanguageID
            //                         select new TagDto()
            //                         {
            //                             ID = many.TagID,
            //                             Name = tagLanguage.DisplayName,
            //                             Slug = tagLanguage.Slug
            //                         }).ToList(),

            //                 Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID && x.BlogCategoryID == blog.BlogCategoryID)
            //                             select categoryLanguage.CategoryName).FirstOrDefault(),



            //                 //Category = (from categoryLanguage in _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID)
            //                 //                         select categoryLanguage.CategoryName).FirstOrDefault()
            //             }).ToList()

            //};
            #endregion



            return CustomResponseDto<GetBlogListDto>.Success(200, getBlogListDto);
        }






        [HttpGet("{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<GetBlogSidebarPartialDto>> GetBlogSidebarPartial(int LanguageID)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Blog SideBar", LanguageID);
            var getBlogSidebarPartialDto = new GetBlogSidebarPartialDto()
            {

                ConstantValues = constantValues,
                BlogCategories = (from category in _blogCategoryService.GetAll(x => !x.IsDeleted && x.Status)
                                  join categoryLanguage in _blogCategoryLanguageService.GetAll()
                                  on category.Id equals categoryLanguage.BlogCategoryID
                                  where categoryLanguage.LanguageID == LanguageID
                                  select new CategoryDto()
                                  {
                                      ID = category.Id,
                                      Name = categoryLanguage.CategoryName,
                                      Order = category.Order,
                                      Slug = $"category/{categoryLanguage.Slug}",
                                  }).ToList(),
                BlogTags = (from tag in _tagService.GetAll()
                            join tagLanguage in _tagLanguageService.GetAll()
                            on tag.Id equals tagLanguage.TagID
                            where tagLanguage.LangaugeID == LanguageID
                            select new TagDto()
                            {
                                ID = tag.Id,
                                Name = tagLanguage.DisplayName,
                                Slug = tagLanguage.Slug
                            }).ToList(),
                TrendBlogs = (from blog in _blogService.GetAll(x => !x.IsDeleted && x.Status).Take(20)
                              join blogLanguage in _blogLanguageService.GetAll(x => x.LanguageID == LanguageID)
                              on blog.Id equals blogLanguage.BlogID
                              select new TrendBlogDto()
                              {
                                  BlogID = blog.Id,
                                  Title = blogLanguage.BlogTitle,
                                  ImagePath = blog.CardImagePath,
                                  Date = blog.Date,
                                  Slug = blogLanguage.Slug,
                              }).ToList()

            };

            return CustomResponseDto<GetBlogSidebarPartialDto>.Success(200, getBlogSidebarPartialDto);
        }

        [HttpGet("{blogID}/{LanguageID}")]
        [ResponseCache(CacheProfileName = AppConstants.Cache60)]
        public async Task<CustomResponseDto<GetBlogDetailDto>> GetBlogDetail(Guid blogID, int LanguageID)
        {
            var constantValues = await _constantValueService.GetConstantValueByPageNameAsync("Blog Detail", LanguageID);
            var blogLanguages = _blogLanguageService.GetAll(x => x.LanguageID == LanguageID);
            var blogCatogryLanguages = _blogCategoryLanguageService.GetAll(x => x.LanguageID == LanguageID);
            var blogCategoryList = _blogCategoryService.GetAll();
            var tagLanguages = _tagLanguageService.GetAll(x => x.LangaugeID == LanguageID);
            var productLangauges = _productLanguageService.GetAll(x => x.LanguageID == LanguageID);
            var blogCommentList = _blogCommentService.GetAll(x => x.BlogId == blogID);
            var tours = _tourService.GetAll();
            try
            {
                GetBlogDetailDto getBlogDetailDto = (from blog in _blogService.GetAll(x => x.Id == blogID)
                                                     join blogLanguage in blogLanguages
                                                     on blog.Id equals blogLanguage.BlogID
                                                     select new GetBlogDetailDto()
                                                     {
                                                         ConstantValues = constantValues,
                                                         BannerImagePath = blog.BannerImagePath,
                                                         CardImagePath = blog.CardImagePath,
                                                         BlogID = blog.Id,
                                                         CategoryDto = (from categoryLanguage in blogCatogryLanguages
                                                                        join blogCategory in blogCategoryList
                                                                        on categoryLanguage.BlogCategoryID equals blogCategory.Id
                                                                        where blog.BlogCategoryID == categoryLanguage.BlogCategoryID
                                                                        select new CategoryDto
                                                                        {
                                                                            ID = categoryLanguage.Id,
                                                                            Name = categoryLanguage.CategoryName,
                                                                            Slug = categoryLanguage.Slug
                                                                        }).FirstOrDefault(),
                                                         Category = _blogCategoryLanguageService.GetAll(x => x.BlogCategoryID == blog.BlogCategoryID && x.LanguageID == LanguageID).FirstOrDefault().CategoryName,
                                                         Tags = (from many in _manyBlogTagService.GetAll(x => x.BlogID == blog.Id)
                                                                 join tagLanguage in tagLanguages
                                                                 on many.TagID equals tagLanguage.TagID
                                                                 select new TagDto()
                                                                 {
                                                                     ID = tagLanguage.TagID,
                                                                     Name = tagLanguage.DisplayName,
                                                                     Slug = tagLanguage.Slug
                                                                 }).ToList(),
                                                         CommentCount = _blogCommentService.Where(x => x.BlogId == blog.Id).Count,
                                                         Date = blog.Date,
                                                         Content1 = blogLanguage.Content1,
                                                         Content2 = blogLanguage.Content2,
                                                         Title = blogLanguage.BlogTitle,
                                                         Slug = blogLanguage.Slug,
                                                         Comments = (from comments in _blogCommentService.GetAll(x => x.BlogId == blogID)
                                                                     select new BlogCommentListDto
                                                                     {
                                                                         BlogID = blogID.ToString(),
                                                                         SenderName = comments.NameSurname,
                                                                         Message = comments.CommentContent,
                                                                         BlogCommentID = comments.Id.ToString(),
                                                                         SenderEmail = comments.Email,
                                                                         Status = comments.Status,
                                                                         ToAnswer = comments.AnsweredBlogCommentId.ToString(),
                                                                         SendDate = comments.ReviewDate,
                                                                         ProfilePhotoPath = comments.ProfilePhotoPath
                                                                     }).ToList(),

                                                         RecomendedTours = (from many in _many_Blog_RecomendedTourService.GetAll(x => x.BlogID == blogID)
                                                                            join product in _productService.GetAll()
                                                                            on many.ProductID equals product.Id
                                                                            join productLanguage in productLangauges
                                                                            on product.Id equals productLanguage.ProductID
                                                                            join tour in tours
                                                                            on product.Id equals tour.ProductID

                                                                            join systemOptionLanguageItem in _systemOptionLanguageService.GetAll() on tour.TourTypeID equals systemOptionLanguageItem.SystemOptionId

                                                                            group new { many, product, productLanguage, tour, systemOptionLanguageItem } by new { many.ProductID } into grouped
                                                                            select new TourDto()
                                                                            {
                                                                                TourImagePath = grouped.FirstOrDefault().product.CardImagePath,
                                                                                ProductID = grouped.Key.ProductID,
                                                                                TourRate = 5,
                                                                                TourName = grouped.FirstOrDefault().productLanguage.DisplayName,
                                                                                TourSegment = new TourTypeDto()
                                                                                {
                                                                                    TypeName = grouped.FirstOrDefault().systemOptionLanguageItem.Name
                                                                                }
                                                                            }).ToList(),


                                                         RecomendedBlogs = (from rBlog in _blogService.GetAll(x => x.BlogCategoryID == blog.BlogCategoryID).Take(2)
                                                                            join rBlogLanguage in _blogLanguageService.GetAll(x => x.LanguageID == LanguageID)
                                                                            on rBlog.Id equals rBlogLanguage.BlogID
                                                                            select new RecomendedBlog()
                                                                            {
                                                                                BlogID = rBlog.Id,
                                                                                BlogImagePath = rBlog.CardImagePath,
                                                                                BlogTitle = rBlogLanguage.BlogTitle,
                                                                                BlogShortDescription = rBlogLanguage.ShortDescription
                                                                            }).ToList()
                                                     }).FirstOrDefault();

                getBlogDetailDto.CommentCount = getBlogDetailDto.Comments.Count;
                return CustomResponseDto<GetBlogDetailDto>.Success(200, getBlogDetailDto);
            }
            catch (Exception ex)
            {
                return CustomResponseDto<GetBlogDetailDto>.Success(200, null);

            }
        }

        [HttpPost]
        public CustomResponseNullDto AddBlogComment(BlogCommentPostDto blogCommentPostDto)
        {

            _blogCommentService.AddBlogComment(blogCommentPostDto);
            //Ekleme işlemi
            return CustomResponseNullDto.Success(204);
        }
        [HttpGet("{languageId}")]
        public CustomResponseDto<List<BlogsWithNameAndIdDto>> GetBlogsWithNameAndId(int languageId)
        {
            var result = _blogService.GetBlogsWithNameAndId(languageId);
            return result;
        }
    }
}
