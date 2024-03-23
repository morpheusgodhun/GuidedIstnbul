using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "FooterViewComponent")]
    public class FooterViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public FooterViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = _configuration["BaseUrl"];
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int languageId = 1;
            //string url = _url + "WebBlog/GetBlogSidebarPartial/1";
            string url1 = _url + $"WebBlog/GetBlogsWithNameAndId/{languageId}";
            string url2 = _url + $"WebTour/BestDealerTours/{languageId}";
            string url3 = _url + $"WebCustomPage/CustomPageInfo/{languageId}";

           
            CustomResponseDto<List<BlogsWithNameAndIdDto>> blogs = await _apiHandler.GetAsync<CustomResponseDto<List<BlogsWithNameAndIdDto>>>(url1);
            CustomResponseDto<List<TourListDto>> tours = await _apiHandler.GetAsync<CustomResponseDto<List<TourListDto>>>(url2);
            CustomResponseDto<CustomPageConstantValueDto> customPageDto = await _apiHandler.GetAsync<CustomResponseDto<CustomPageConstantValueDto>>(url3);

            //CustomResponseDto<>
            var pageModel = (blogs.Data, tours.Data, customPageDto.Data);
            if (blogs.Data is null)
            {
                return View();
            }
            else
            {
                return View(pageModel);
            }
        }
    }
}
