
using Core.IService;
using Dto.ApiPanelDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelTourGuideController : ControllerBase
    {
        private readonly IGuideService _guideService;

        public PanelTourGuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }
        [HttpGet]
        public CustomResponseDto<List<GetGuideDto>> GetAllGuides()
        {
            var result = _guideService.GetAllGuides();
            return result;
        }
        [HttpGet("{Id}")]
        public CustomResponseNullDto ApproveGuide(Guid Id)
        {
           return _guideService.ApproveGuide(Id);
        }
        [HttpGet("{Id}")]
        public CustomResponseNullDto RejectGuide(Guid Id)
        {
            return _guideService.RejectGuide(Id);
        }
        [HttpGet("{Id}")]
        public CustomResponseNullDto ChangeGuideStatus(Guid Id)
        {
            return _guideService.ChangeGuideStatus(Id);
        }
    }
}
