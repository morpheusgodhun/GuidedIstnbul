using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Blog
{
    public class BlogListItemDto
    {
        public Guid BlogID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int CommentCount { get; set; }
        public DateTime Date { get; set; }
        public List<TagDto> Tags { get; set; }
        public string Category { get; set; }
        public CategoryDto CategoryDto { get; set; }
        public string Slug { get; set; }

     
    }
}
