using Dto.ApiPanelDtos.SubscriberDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelSubscriberController : ControllerBase
    {
        [HttpGet]
        public CustomResponseDto<List<SubscriberListDto>> SubscriberList()
        {
            List<SubscriberListDto> subscriberListDtos = new List<SubscriberListDto>()
            {
                new SubscriberListDto()
                {
                    SubscriberID = "1",
                    Email = "ecmel@gmail.com",
                    Status = true,
                    MembershipDate = DateTime.Now.AddDays(-3),
                },
                new SubscriberListDto()
                {
                    SubscriberID = "2",
                    Email = "metin@gmail.com",
                    Status = false,
                    MembershipDate = DateTime.Now.AddDays(-6),
                },
                new SubscriberListDto()
                {
                    SubscriberID = "3",
                    Email = "alinda@gmail.com",
                    Status = true,
                    MembershipDate = DateTime.Now.AddDays(-9),
                },
                new SubscriberListDto()
                {
                    SubscriberID = "4",
                    Email = "sevinç@gmail.com",
                    Status = false,
                    MembershipDate = DateTime.Now.AddDays(-10),
                },
            };

            return CustomResponseDto<List<SubscriberListDto>>.Success(200, subscriberListDtos);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleSubscriberStatus(string id)
        {

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet]
        public CustomResponseDto<List<SubscriberListDto>> GetActiveSubscriberList()
        {
            List<SubscriberListDto> subscriberListDtos = new List<SubscriberListDto>()
            {
                new SubscriberListDto()
                {
                    SubscriberID = "1",
                    Email = "ecmel@gmail.com",
                    Status = true,
                    MembershipDate = DateTime.Now.AddDays(-3),
                },
                new SubscriberListDto()
                {
                    SubscriberID = "3",
                    Email = "alinda@gmail.com",
                    Status = true,
                    MembershipDate = DateTime.Now.AddDays(-9),
                },
            };

            return CustomResponseDto<List<SubscriberListDto>>.Success(200, subscriberListDtos);
        }
    }
}
