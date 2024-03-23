using Core.Entities;
using Core.IRepository;
using Core.IService;
using Data.Migrations;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        Context _context;
        DbSet<Blog> _blogs;
        DbSet<BlogLanguageItem> _blogLanguages;
        DbSet<BlogCategory> _categories;
        DbSet<BlogCategoryLanguageItem> _categoryLanguages;
        DbSet<Many_Blog_Tag> _blogTags;
        DbSet<TagLanguageItem> _tagLanguages;
        //DbSet<User> _users;
        DbSet<Core.Entities.BlogComment> _blogComments;


        public BlogRepository(Context context) : base(context)
        {
            _context = context;
            _blogs = _context.Blogs;
            _blogLanguages = _context.BlogLanguageItems;
            _blogTags = _context.Many_Blog_Tags;
            _tagLanguages = _context.TagLanguageItems;
            _categories = _context.BlogCategories;
            _categoryLanguages = _context.BlogCategoryLanguageItems;
            _blogComments = _context.BlogComments;
        }

        public List<TrendBlogDto> FaqBlogList(int languageId)
        {
            List<TrendBlogDto> trendBlogs = (from blog in _blogs.ToList()
                                             where blog.ShowOnFAQ && blog.Status
                                             join blogLanguage in _blogLanguages.ToList()
                                             on blog.Id equals blogLanguage.BlogID
                                             where blogLanguage.LanguageID == languageId
                                             select new TrendBlogDto()
                                             {
                                                 BlogID = blog.Id,
                                                 Title = blogLanguage.BlogTitle,
                                                 ImagePath = blog.CardImagePath,
                                                 Date = blog.Date
                                             }).ToList();
            return trendBlogs;

        }

        public async Task<List<BlogListItemDto>> HomeBlogList(int languageId)
        {
            List<BlogListItemDto> blogList = await (from blog in _blogs
                                                    where !blog.IsDeleted && blog.Status && blog.ShowOnHomepage
                                                    join blogLanguage in _blogLanguages
                                                    on blog.Id equals blogLanguage.BlogID
                                                    where blogLanguage.LanguageID == languageId
                                                    join category in _categories
                                                    on blog.BlogCategoryID equals category.Id
                                                    join categoryLanguage in _categoryLanguages
                                                    on category.Id equals categoryLanguage.BlogCategoryID
                                                    where categoryLanguage.LanguageID == languageId
                                                    select new BlogListItemDto()
                                                    {
                                                        BlogID = blog.Id,
                                                        ImagePath = blog.CardImagePath,
                                                        Title = blogLanguage.BlogTitle,
                                                        ShortDescription = blogLanguage.ShortDescription,
                                                        Category = categoryLanguage.CategoryName,
                                                        Tags = (from many in _blogTags
                                                                where many.BlogID == blog.Id
                                                                join tagLanguage in _tagLanguages
                                                                on many.TagID equals tagLanguage.TagID
                                                                where tagLanguage.LangaugeID == languageId
                                                                select new TagDto()
                                                                {
                                                                    ID = tagLanguage.TagID,
                                                                    Name = tagLanguage.DisplayName,
                                                                    Slug = tagLanguage.Slug
                                                                }).ToList(),
                                                        Date = blog.Date,
                                                        CommentCount = 0
                                                    }).ToListAsync();
            return blogList;
        }

        public List<BlogsWithNameAndIdDto> GetBlogsWithNameAndId(int languageId)
        {
            var blogList = (from blog in _context.Blogs
                            join blogLanguage in _context.BlogLanguageItems
                            on blog.Id equals blogLanguage.BlogID
                            where blogLanguage.LanguageID == languageId && blog.Status == true && blog.IsDeleted == false
                            orderby blog.CreateDate descending
                            select new BlogsWithNameAndIdDto()
                            {
                                BlogTitle = blogLanguage.BlogTitle,
                                BlogId = blog.Id,
                                Slug = blogLanguage.Slug,
                            }).Take(5).ToList();
            return blogList;
        }


        public List<BlogListItemDto> GetBlogList(int languageId)
        {
            /*
             from blog in _blogs.ToList()
                                              where !blog.IsDeleted && blog.Status
                                              join blogLanguage in _blogLanguages.ToList()
                                              on blog.Id equals blogLanguage.BlogID
                                              where blogLanguage.LanguageID == languageId
                                              join category in _categories.ToList()
                                              on blog.BlogCategoryID equals category.Id
                                              join categoryLanguage in _categoryLanguages.ToList()
                                              on category.Id equals categoryLanguage.BlogCategoryID
                                              where categoryLanguage.LanguageID == languageId
             */


            var blogList = (from blog in _blogs
                            where !blog.IsDeleted && blog.Status
                            join blogLanguage in _blogLanguages
                                on blog.Id equals blogLanguage.BlogID into blogLangJoin
                            from blogLanguage in blogLangJoin.Where(bl => bl.LanguageID == languageId).DefaultIfEmpty()
                            join category in _categories
                                on blog.BlogCategoryID equals category.Id
                            join categoryLanguage in _categoryLanguages
                                on category.Id equals categoryLanguage.BlogCategoryID
                            where categoryLanguage.LanguageID == languageId
                            select new BlogListItemDto()
                            {
                                BlogID = blog.Id,
                                Title = blogLanguage.BlogTitle,
                                ImagePath = blog.CardImagePath,
                                Date = blog.Date,
                                CommentCount = 0,
                                ShortDescription = blogLanguage.ShortDescription,
                                Slug = blogLanguage.Slug,
                                Category = categoryLanguage.CategoryName,

                                CategoryDto = new CategoryDto
                                {
                                    ID = categoryLanguage.Id, // new Guid("24fc16df-5017-4e5c-a408-7b7d6258608f"),
                                    Name = categoryLanguage.CategoryName, //"Test",
                                    Slug = categoryLanguage.Slug
                                    // Order = null
                                },
                                Tags = (from many in _blogTags
                                        where many.BlogID == blog.Id
                                        join tagLanguage in _tagLanguages
                                            on many.TagID equals tagLanguage.TagID
                                        where tagLanguage.LangaugeID == languageId
                                        select new TagDto()
                                        {
                                            ID = tagLanguage.TagID,
                                            Name = tagLanguage.DisplayName
                                        }).ToList(),

                            }).ToList();

            return blogList;
        }




        public List<BlogListItemDto> GetBlogListPage(int currentPage, int pageSize, int languageId)
        {

            var blogList = (from blog in _blogs
                            where !blog.IsDeleted && blog.Status
                            join blogLanguage in _blogLanguages
                                on blog.Id equals blogLanguage.BlogID into blogLangJoin
                            from blogLanguage in blogLangJoin.Where(bl => bl.LanguageID == languageId).DefaultIfEmpty()
                            join category in _categories
                                on blog.BlogCategoryID equals category.Id
                            join categoryLanguage in _categoryLanguages
                                on category.Id equals categoryLanguage.BlogCategoryID
                            where categoryLanguage.LanguageID == languageId
                            select new BlogListItemDto()
                            {
                                BlogID = blog.Id,
                                Title = blogLanguage.BlogTitle,
                                ImagePath = blog.CardImagePath,
                                Date = blog.Date,
                                CommentCount = 0,
                                ShortDescription = blogLanguage.ShortDescription,
                                Slug = blogLanguage.Slug,
                                Category = categoryLanguage.CategoryName,

                                CategoryDto = new CategoryDto
                                {
                                    ID = categoryLanguage.Id,
                                    Name = categoryLanguage.CategoryName,
                                    Slug = categoryLanguage.Slug
                                    // Order = null
                                },
                                Tags = (from many in _blogTags
                                        where many.BlogID == blog.Id
                                        join tagLanguage in _tagLanguages
                                            on many.TagID equals tagLanguage.TagID
                                        where tagLanguage.LangaugeID == languageId
                                        select new TagDto()
                                        {
                                            ID = tagLanguage.TagID,
                                            Name = tagLanguage.DisplayName
                                        }).ToList(),

                            }).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return blogList;
        }

        public List<BlogCommentListDto> GetBlogCommentList(Guid blogId, int languageId)
        {
            List<BlogCommentListDto> blogComments = (from comments in _blogComments.Where(x => x.BlogId == blogId).ToList()
                                                     select new BlogCommentListDto
                                                     {
                                                         BlogID = blogId.ToString(),
                                                         SenderName = comments.NameSurname,
                                                         Message = comments.CommentContent,
                                                         BlogCommentID = comments.AnsweredBlogCommentId.ToString(),
                                                         SenderEmail = comments.Email,
                                                         Status = comments.Status,
                                                         ToAnswer = "",
                                                         SendDate = comments.CreateDate,
                                                         ProfilePhotoPath = comments.ProfilePhotoPath
                                                     }).ToList();

            return blogComments;
        }

        //public async Task AddBlogComment(BlogCommentPostDto blogCommentPostDto)
        //{
        //    //Todo : Blog ekleme yapılacak
        //    var user = await _users.SingleOrDefaultAsync(x => x.Id == blogCommentPostDto.UserID);
        //    var blog = await _blogs.SingleOrDefaultAsync(x => x.Id == blogCommentPostDto.BlogID);

        //    Core.Entities.BlogComment blogComment = new()
        //    {
        //        CommentContent = blogCommentPostDto.CommentContent,
        //        AnsweredBlogCommentId = blogCommentPostDto.AnsweredBlogCommentId,
        //        BlogId = blog.Id,

        //    };


        //    throw new NotImplementedException();
        //}
    }
}
