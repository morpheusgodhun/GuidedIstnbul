using Core.IService;
using Core.StaticValues;
using Dto;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuideAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace GuideApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : CustomBaseController
    {
        readonly IRouteService _routeService;
        readonly ISeoService _seoService;

        public HomeController(IRouteService routeService, ISeoService seoService)
        {
            _routeService = routeService;
            _seoService = seoService;
        }


        [HttpGet]
        public IActionResult IndexGet()
        {
            Product product = new Product();
            product.Id = "1";
            product.Name = "Name";
            product.Description = "Description";

            return Ok(product);
        }
        [HttpPost]
        public IActionResult IndexPost(Product product)
        {
            product.Id = "1";
            product.Name = "Name";
            product.Description = "Description";

            return Ok(product);
        }
        [HttpPost]
        public CustomResponseDto<RouteTemplateDto> GetRoute(GetRouteTemplateDto slugDto)
        {
            RouteTemplateDto routeTemplate = _routeService.GetRouteTemplateBySlug(slugDto.Slug);

            if (routeTemplate is not null)
            {
                var seoList = _seoService.GetSeoListByRouteId(routeTemplate.Id);
                if (seoList.Count > 0)
                    routeTemplate.SeoListDtos = seoList;

                return CustomResponseDto<RouteTemplateDto>.Success(200, routeTemplate);
            }

            return CustomResponseDto<RouteTemplateDto>.Success(404, null);
        }

        [HttpGet("{sitemapUrl}")]
        public ActionResult<CustomResponseDto<string>> GetSitemap(string sitemapUrl)
        {
            var xmlDocument = _seoService.GenerateSitemap(sitemapUrl);
            //Response.ContentType = "application/xml";
            //Response.ContentType = "text/xml";

            return CustomResponseDto<string>.Success(200, xmlDocument.InnerXml);
        }

    }
}
