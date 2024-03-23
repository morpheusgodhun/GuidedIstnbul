using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "BlogSidebarViewComponent")]
    public class BlogSidebarViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public BlogSidebarViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        //public async Task<IViewComponentResult> Invoke()
        //{
        //    string url = _url + "WebBlog/GetBlogSidebarPartial/1";
        //    CustomResponseDto<GetBlogSidebarPartialDto> getBlogSidebarPartialDto = await _apiHandler.GetAsync<CustomResponseDto<GetBlogSidebarPartialDto>>(url);

        //    if (getBlogSidebarPartialDto is null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(getBlogSidebarPartialDto.Data);
        //    }
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebBlog/GetBlogSidebarPartial/1";
            CustomResponseDto<GetBlogSidebarPartialDto> getBlogSidebarPartialDto = await _apiHandler.GetAsync<CustomResponseDto<GetBlogSidebarPartialDto>>(url);

            if (getBlogSidebarPartialDto is null)
            {
                return View();
            }
            else
            {
                return View(getBlogSidebarPartialDto.Data);
            }
        }
    }
}
