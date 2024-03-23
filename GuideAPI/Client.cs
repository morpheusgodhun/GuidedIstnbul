using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Data.Migrations;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GuideAPI
{
    public class Client
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly HttpClient _httpClient;
        readonly IBlogCategoryService _blogCategoryService;
        readonly IBlogCategoryLanguageService _blogCategoryLanguageService;
        readonly ITagService _tagService;
        readonly ITagLanguageService _tagLanguageService;
        readonly IBlogService _blogService;
        readonly IBlogLanguageService _blogLanguageService;
        readonly IMany_Blog_BlogCategoryService _blog_BlogCategoryService;
        readonly IManyBlogTagService _many_BlogTagService;
        public Client(IHttpClientFactory httpClientFactory, IBlogCategoryLanguageService blogCategoryLanguageService, IBlogCategoryService blogCategoryService, ITagLanguageService tagLanguageService, ITagService tagService, IBlogService blogService, IBlogLanguageService blogLanguageService, IMany_Blog_BlogCategoryService blog_BlogCategoryService, IManyBlogTagService many_BlogTagService)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            _blogCategoryLanguageService = blogCategoryLanguageService;
            _tagService = tagService;
            _blogService = blogService;

            _blogCategoryService = blogCategoryService;
            _tagLanguageService = tagLanguageService;
            _blogLanguageService = blogLanguageService;
            _blog_BlogCategoryService = blog_BlogCategoryService;
            _many_BlogTagService = many_BlogTagService;
        }


        public async Task X()
        {
            List<GuidedIstanbulCategory>? guidedIstanbulCategories = await GetGuidedIstanbulCategoriesAsync();
            List<GuidedIstanbulTag>? guidedIstanbulTags = await GetGuidedIstanbulTagsAsync();

            List<BlogCategoryLanguageItem> newCategoryLangItems = await SaveCategoriesAsync(guidedIstanbulCategories);
            List<TagLanguageItem> newTagsLangItems = await SaveTagsAsync(guidedIstanbulTags);
            List<NewBlogObj> blogs = new();

            for (int i = 1; i < 4; i++)
            {
                var blogsa = await GetBlogsAsync(50, i);
                blogs.AddRange(blogsa);
            }
            foreach (NewBlogObj blog in blogs)
            {
                List<string> blogCategoryNames = guidedIstanbulCategories.Where(x => blog.Categories.Contains(x.Id)).Select(x => x.Name).ToList();

                List<Guid> blogCategoryGuids = new();
                foreach (string category in blogCategoryNames)
                {
                    var blogCategoryGuid = newCategoryLangItems.Where(x => x.CategoryName == category).Select(x => x.BlogCategoryID).FirstOrDefault();
                    blogCategoryGuids.Add(blogCategoryGuid);
                }

                List<string> blogTagNames = guidedIstanbulTags.Where(x => blog.Tags.Contains(x.Id)).Select(x => x.Name).ToList();
                List<Guid> blogTagGuids = new();

                foreach (string tag in blogTagNames)
                {
                    Guid blogTagGuid = newTagsLangItems.Where(x => x.DisplayName == tag).Select(x => x.TagID).FirstOrDefault();
                    blogTagGuids.Add(blogTagGuid);
                }

                Blog newBlog = await _blogService.AddAsync(new()
                {
                    BannerImagePath = blog.BannerImage,
                    CardImagePath = blog.CardImage,
                    ShowOnFAQ = false,
                    ShowOnHomepage = false,
                    BlogCategoryID = blogCategoryGuids.First(),
                    Date = Convert.ToDateTime(blog.Date),
                });
                foreach (Guid categoryId in blogCategoryGuids)
                {
                    Many_Blog_BlogCategory manyBlogCategory = new()
                    {
                        BlogCategoryId = categoryId,
                        BlogId = newBlog.Id
                    };
                    await _blog_BlogCategoryService.AddAsync(manyBlogCategory);
                }
                foreach (Guid tagId in blogTagGuids)
                {
                    Many_Blog_Tag manyBlogTag = new()
                    {
                        TagID = tagId,
                        BlogID = newBlog.Id
                    };
                    await _many_BlogTagService.AddAsync(manyBlogTag);
                }

                foreach (var language in LanguageList.AllLanguages)
                {
                    BlogLanguageItem languageItem = new()
                    {
                        BlogID = newBlog.Id,
                        Content1 = blog.Content,
                        Content2 = "",
                        Slug = blog.Slug,
                        BlogTitle = blog.Title,
                        ShortDescription = "",
                        LanguageID = language.Id,
                    };
                    _blogLanguageService.Add(languageItem);
                }

            }
        }
        public async Task<List<NewBlogObj>> GetBlogsAsync(int size, int page)
        {
            var response = await _httpClient.GetAsync($"https://www.guidedistanbultours.com/wp-json/wp/v2/posts?per_page={size}&page={page}");
            var responseString = await response.Content.ReadAsStringAsync();

            var guidedIstanbulPosts = JsonConvert.DeserializeObject<List<GuidedIstanbulBlogPost>>(responseString);

            List<NewBlogObj> blogList = new();
            foreach (GuidedIstanbulBlogPost post in guidedIstanbulPosts)
            {
                try
                {
                    var imageUrl = post._links["wp:featuredmedia"][0]["href"];
                    var imgResponse = await _httpClient.GetAsync(imageUrl.ToString());
                    var imgResponseString = await imgResponse.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(imgResponseString);
                    var postImgUrl = responseObject["guid"]["rendered"].ToString();

                    string cardImgEndStr = "-400x267.jpg";
                    string bannerImg = postImgUrl.ToString().Replace("https://www.guidedistanbultours.com/media/", string.Empty);
                    string cardImg = bannerImg.ToString().Replace(".jpg", cardImgEndStr);

                    var blog = new NewBlogObj
                    {
                        Title = post.Title?.Rendered,
                        Content = post.Content?.Rendered,
                        Slug = post.Slug,
                        Categories = post.Categories,
                        Tags = post.Tags,
                        BannerImage = bannerImg,
                        CardImage = cardImg,
                        Date = Convert.ToDateTime(post.Date)
                    };

                    blog.Content = RemoveTextBetweenTags(blog.Content, "[vc_zigzag]");
                    blog.Content = RemoveSquareBracketStrings(blog.Content);
                    blogList.Add(blog);
                }
                catch (Exception ex)
                {

                }

            }
            return blogList;
        }
        async Task<List<GuidedIstanbulCategory>> GetGuidedIstanbulCategoriesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://www.guidedistanbultours.com/wp-json/wp/v2/categories");
            string responseString = await response.Content.ReadAsStringAsync();
            var guidedIstanbulCategories = JsonConvert.DeserializeObject<List<GuidedIstanbulCategory>>(responseString);
            return guidedIstanbulCategories;
        }
        async Task<List<GuidedIstanbulTag>> GetGuidedIstanbulTagsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://www.guidedistanbultours.com/wp-json/wp/v2/tags");
            string responseString = await response.Content.ReadAsStringAsync();
            var guidedIstanbulTags = JsonConvert.DeserializeObject<List<GuidedIstanbulTag>>(responseString);
            return guidedIstanbulTags;

        }
        private async Task<List<BlogCategoryLanguageItem>> SaveCategoriesAsync(List<GuidedIstanbulCategory> guidedIstanbulCategories)
        {

            List<BlogCategoryLanguageItem> newlyAddedCategoryList = new();

            foreach (GuidedIstanbulCategory category in guidedIstanbulCategories)
            {
                BlogCategory addedCategory = await _blogCategoryService.AddAsync(new Core.Entities.BlogCategory()
                {
                    Order = new Random().Next(1, 20),
                });

                foreach (Language language in LanguageList.AllLanguages)
                {
                    var newLangItem = _blogCategoryLanguageService.Add(new()
                    {
                        BlogCategoryID = addedCategory.Id,
                        CategoryName = category.Name,
                        Slug = category.Slug,
                        LanguageID = language.Id,
                    });
                    newlyAddedCategoryList.Add(newLangItem);
                }
            }
            return newlyAddedCategoryList;

        }
        private async Task<List<TagLanguageItem>> SaveTagsAsync(List<GuidedIstanbulTag> guidedIstanbulTags)
        {
            List<TagLanguageItem> newlyAddedTagsList = new();

            foreach (GuidedIstanbulTag tag in guidedIstanbulTags)
            {
                Tag newTag = await _tagService.AddAsync(new Core.Entities.Tag()
                {
                    IconPath = " ",
                    TagCategories = "[1]"
                });

                foreach (Language language in LanguageList.AllLanguages)
                {
                    var newLangItem = await _tagLanguageService.AddAsync(new()
                    {
                        DisplayName = tag.Name,
                        Slug = tag.Slug,
                        TagID = newTag.Id,
                        LangaugeID = language.Id
                    });
                    newlyAddedTagsList.Add(newLangItem);
                }
            }

            return newlyAddedTagsList;
        }
        string RemoveTextBetweenTags(string input, string tag)
        {
            string pattern = $@"{Regex.Escape(tag)}(.*?){Regex.Escape(tag)}";
            string result = Regex.Replace(input, pattern, "", RegexOptions.Singleline);
            return result;
        }
        string RemoveSquareBracketStrings(string input)
        {
            string pattern = @"\[.*?\]";
            string result = Regex.Replace(input, pattern, "");
            return result;
        }
    }

    public class NewBlogObj
    {
        public string BannerImage { get; set; }
        public string CardImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public List<int> Categories { get; set; }
        public List<int> Tags { get; set; }
    }

    public class TitleData
    {
        public string Rendered { get; set; }
    }

    public class ContentData
    {
        public string Rendered { get; set; }
    }

    public class GuidedIstanbulBlogPost
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Modified { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public TitleData Title { get; set; }
        public ContentData Content { get; set; }
        public List<int> Categories { get; set; }
        public List<int> Tags { get; set; }
        public dynamic _links { get; set; }
    }
    public class GuidedIstanbulCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
    public class GuidedIstanbulTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
