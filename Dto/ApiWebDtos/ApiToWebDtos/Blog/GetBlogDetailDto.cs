using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Blog
{
    public class GetBlogDetailDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public Guid BlogID { get; set; }
        public string BannerImagePath { get; set; }
        public string CardImagePath { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        //public string ShortDescription { get; set; }
        public string Content1 { get; set; }
        public string Content2   { get; set; }
        public int CommentCount { get; set; }
        public DateTime Date { get; set; }
        public List<TagDto> Tags { get; set; }
        public string Category { get; set; }
        public CategoryDto CategoryDto { get; set; }
        public List<BlogCommentListDto> Comments { get; set; }
        //public List<BlogComment> Comments { get; set; } eskisi
        public List<RecomendedBlog> RecomendedBlogs { get; set; }
        public List<TourDto> RecomendedTours { get; set; }

    }

    public class BlogComment
    {
        public string CommentID { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string? ToCommentID { get; set; }
    }


}
