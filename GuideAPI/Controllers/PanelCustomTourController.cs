using Azure.Core;
using Core.IService;
using Dto.ApiPanelDtos.CustomTourDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelCustomTourController : ControllerBase
    {
        private readonly ICustomTourRequestService _customTourRequestService;

        public PanelCustomTourController(ICustomTourRequestService customTourRequestService)
        {
            _customTourRequestService = customTourRequestService;
        }

        [HttpGet]
        public CustomResponseDto<List<CustomTourRequestListItemDto>> CustomTourRequestList()
        {

            var result = _customTourRequestService.CustomTourRequestList();
            return CustomResponseDto<List<CustomTourRequestListItemDto>>.Success(200, result);

        }

        [HttpGet("{RequestId}")]
        public CustomResponseDto<List<OfferListDto>> OfferListByRequestId(Guid RequestId)
        {
            var result = _customTourRequestService.OfferListByRequestId(RequestId);
            return CustomResponseDto<List<OfferListDto>>.Success(200, result);

        }

        [HttpPost]
        public CustomResponseNullDto AddOffer(AddOfferDto addOfferDto)
        {
            _customTourRequestService.AddOffer(addOfferDto);
            return CustomResponseNullDto.Success(204);

        }

        public CustomResponseNullDto SaveMailContent(SaveMailContentDto saveMailContentDto)
        {
            _customTourRequestService.AddMailContent(saveMailContentDto);
            return CustomResponseNullDto.Success(0200);
        }

    }
}
