using Core.IService;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelPageController : CustomBaseController
    {
        private readonly IPageService _pageService;

        public PanelPageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> PageSelectList()
        {
            List<SelectListOptionDto> selectList = (from page in _pageService.GetAll(x => x.IsAllActive)
                                                    select new SelectListOptionDto()
                                                    {
                                                        OptionID = page.Id,
                                                        Option = page.PageName
                                                    }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, selectList);
        }

    }
}
