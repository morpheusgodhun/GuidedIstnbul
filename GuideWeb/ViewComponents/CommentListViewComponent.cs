using Dto.ApiWebDtos.ApiToWebDtos.PageComponets;
using DTO;
using GuideWeb.APIHandler;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents
{
    [ViewComponent(Name = "CommentListViewComponent")]
    public class CommentListViewComponent : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public CommentListViewComponent(IApiHandler apiHandler, IConfiguration configuration)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = this._configuration["BaseUrl"];
        }

        //public async Task<IViewComponentResult> Invoke()
        //{
        //    string url = _url + "WebPageComponent/GetCommentList/1";
        //    CustomResponseDto<GetCommentListDto> getCommentListDto = await _apiHandler.GetAsync<CustomResponseDto<GetCommentListDto>>(url);

        //    if (getCommentListDto is null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(getCommentListDto.Data);
        //    }
        //}
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _url + "WebPageComponent/GetCommentList/1";
            CustomResponseDto<GetCommentListDto> getCommentListDto = await _apiHandler.GetAsync<CustomResponseDto<GetCommentListDto>>(url);

            if (getCommentListDto is null)
            {
                return View();
            }
            else
            {
                return View(getCommentListDto.Data);
            }
        }
    }
}
