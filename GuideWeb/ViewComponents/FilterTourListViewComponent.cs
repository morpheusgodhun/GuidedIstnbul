using Dto.ApiWebDtos.ApiToWebDtos.Blog;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "FilterTourListViewComponent")]
    public class FilterTourListViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public FilterTourListViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        public IViewComponentResult Invoke(FilterTourDto dto)
        {
            return View();
            string url = _url + "WebBlog/GetBlogSidebarPartial/1";
            CustomResponseDto<GetBlogSidebarPartialDto> getBlogSidebarPartialDto = _apiHandler.GetApi<CustomResponseDto<GetBlogSidebarPartialDto>>(url);

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
