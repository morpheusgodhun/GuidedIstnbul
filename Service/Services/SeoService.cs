using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Core.StaticValues;
using Core.StaticValues.Route;
using Dto.ApiPanelDtos.SeoDtos;
using Dto.ApiPanelDtos.UserDtos;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service.Services
{
    public class SeoService : GenericService<Seo>, ISeoService
    {
        ISeoRepository _seoRepository;
        IPageRepository _pageRepository;
        IRouteRepository _routeRepository;
        IConfiguration _configuration;
        IBlogLanguageRepository _blogLanguageRepository;
        ITourLanguageRepository _tourLanguageRepository;
        IBlogCategoryLanguageRepository _blogCategoryLanguageRepository;
        public SeoService(IGenericRepository<Seo> repository, IUnitOfWork unitOfWork, ISeoRepository seoRepository, IRouteRepository routeRepository, IConfiguration configuration, IBlogLanguageRepository blogLanguageRepository, ITourLanguageRepository tourLanguageRepository, IBlogCategoryLanguageRepository blogCategoryLanguageRepository, IPageRepository pageRepository) : base(repository, unitOfWork)
        {
            _seoRepository = seoRepository;
            _routeRepository = routeRepository;
            _configuration = configuration;
            _blogLanguageRepository = blogLanguageRepository;
            _tourLanguageRepository = tourLanguageRepository;
            _blogCategoryLanguageRepository = blogCategoryLanguageRepository;
            _pageRepository = pageRepository;
        }

        public void AddSeo(AddSeoDto addSeoDto)
        {
            _seoRepository.AddSeo(addSeoDto);
            _unitOfWork.saveChanges();
        }

        public void EditSeo(EditSeoDto editSeoDto)
        {
            _seoRepository.EditSeo(editSeoDto);
            _unitOfWork.saveChanges();
        }

        public EditSeoDto GetEditSeoDto(Guid id)
        {
            return _seoRepository.GetEditSeoDto(id);
        }

        public List<SeoListByRouteIdDto> GetSeoListByRouteId(Guid routeId)
        {
            return _seoRepository.GetSeoListByRouteId(routeId);
        }

        public List<SeoListDto> GetSeoListDto()
        {
            return _seoRepository.GetSeoListDto();
        }

        public XmlDocument GenerateSitemap(string location)
        {
            if (location.EndsWith(".xml"))
                location = location.Replace(".xml", string.Empty);

            XmlDocument xmlDoc;
            SitemapInfo sitemapInfo = SitemapConstants.GetByLocation(location);
            if (sitemapInfo is null)
                return null;

            xmlDoc = sitemapInfo.IsMainLocation ? GetIndexSitemapXml() : GetSitemapXml(sitemapInfo);
            return xmlDoc;
        }
        XmlDocument GetIndexSitemapXml()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            //XmlProcessingInstruction styleSheet = xmlDoc.CreateProcessingInstruction("xml-stylesheet", $"type=\"text/xsl\" href=\"//{_configuration["ClientBaseURL"]}main-sitemap.xsl\"");
            //xmlDoc.AppendChild(styleSheet);

            XmlElement rootElement = xmlDoc.CreateElement("sitemapindex");
            rootElement.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");

            xmlDoc.AppendChild(rootElement);

            foreach (SitemapInfo sitemap in SitemapConstants.SitemapInfo)
            {
                if (sitemap.IsMainLocation)
                    continue;
                XmlElement sitemapElement = xmlDoc.CreateElement("sitemap");
                XmlElement locElement = xmlDoc.CreateElement("loc");

                locElement.InnerText = $"{_configuration["ClientBaseURL"]}{sitemap.Location}.xml";
                sitemapElement.AppendChild(locElement);

                XmlElement lastmodElement = xmlDoc.CreateElement("lastmod");
                lastmodElement.InnerText = GetLastModifiedDate(sitemap); // ilk çalıştığında hata veriyor. debug et. düzeldi !
                sitemapElement.AppendChild(lastmodElement);

                rootElement.AppendChild(sitemapElement);
            }
            return xmlDoc;
        } //sitemap mainPage --> sitemap_index
        XmlDocument GetSitemapXml(SitemapInfo sitemapInfo) //inside sitemap_index
        {
            XmlDocument xmlDoc = new();

            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            XmlElement rootElement = xmlDoc.CreateElement("urlset");

            rootElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            rootElement.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            rootElement.SetAttribute("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9  http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

            xmlDoc.AppendChild(rootElement);

            XmlElement firstUrlElement = xmlDoc.CreateElement("url");
            rootElement.AppendChild(firstUrlElement);

            XmlElement firstUrlLocElement = xmlDoc.CreateElement("loc");
            string firstLocationStr = string.Empty;

            string clientBaseUrl = _configuration["ClientBaseURL"];
            List<Route> routes = new();

            switch (GetRouteTemplateEnum(sitemapInfo))
            {
                case RouteTemplateConstants.No.Blog:
                    //todo blog slug , page eklenince
                    firstLocationStr = $"{clientBaseUrl}{LanguageList.BaseLanguage.UrlPrefix}/blogs";
                    firstUrlLocElement.InnerText = firstLocationStr;
                    firstUrlElement.AppendChild(firstUrlLocElement);
                    routes = GetRoutes(sitemapInfo.RouteTemplateInfo);
                    break;
                case RouteTemplateConstants.No.BlogCategory:
                    firstLocationStr = $"{clientBaseUrl}{LanguageList.BaseLanguage.UrlPrefix}/category";
                    routes = GetRoutes(sitemapInfo.RouteTemplateInfo);

                    break;
                case RouteTemplateConstants.No.Tour:
                    /*var slug = _pageRepository.GetPageSlug("Tour Filter", LanguageList.BaseLanguage.Id)*/
                    firstLocationStr = $"{clientBaseUrl}{LanguageList.BaseLanguage.UrlPrefix}/tour";
                    firstUrlLocElement.InnerText = firstLocationStr;
                    firstUrlElement.AppendChild(firstUrlLocElement);
                    routes = GetRoutes(sitemapInfo.RouteTemplateInfo);

                    break;
                case RouteTemplateConstants.No.CustomPage:
                    firstUrlLocElement.InnerText = clientBaseUrl;
                    firstUrlElement.AppendChild(firstUrlLocElement);
                    routes = GetPageSitemapRoutes();
                    break;
                default:
                    routes = new();
                    break;
            }

            if (routes.Count > 0)
            {
                XmlElement firstLastmodElement = xmlDoc.CreateElement("lastmod");

                //177 düzeltecem -mehmet ali
                firstLastmodElement.InnerText = routes.OrderByDescending(x => x.UpdateDate).FirstOrDefault().UpdateDate is not null ? routes.OrderByDescending(x => x.UpdateDate).FirstOrDefault().UpdateDate.ToString() : routes.OrderByDescending(x => x.CreateDate).FirstOrDefault().CreateDate.ToString();

                firstUrlElement.AppendChild(firstLastmodElement);

                foreach (var route in routes.OrderBy(x => x.Url))
                {
                    XmlElement urlElement = xmlDoc.CreateElement("url");
                    rootElement.AppendChild(urlElement);

                    XmlElement locElement = xmlDoc.CreateElement("loc");
                    locElement.InnerText = $"{clientBaseUrl}{route.Url}";

                    urlElement.AppendChild(locElement);

                    XmlElement lastmodElement = xmlDoc.CreateElement("lastmod");
                    lastmodElement.InnerText = route.UpdateDate is null ? route.CreateDate.ToString() : route.UpdateDate.ToString();
                    urlElement.AppendChild(lastmodElement);

                    rootElement.AppendChild(urlElement);
                }
            }
            return xmlDoc;
        }
        List<Route> GetRoutes(RouteTemplate routeTemplate)
        {
            var routes = _routeRepository.Where(x => x.Controller == routeTemplate.Controller && x.Action == routeTemplate.Action && x.LanguageId == LanguageList.BaseLanguage.Id && x.SitemapInclude);
            return routes;
        }
        List<Route> GetPageSitemapRoutes()
        {
            List<string> pageUrls = _pageRepository.GetPageSlugsForSitemapAsync(1).Result.ConvertAll(slug => $"{LanguageList.BaseLanguage.UrlPrefix}/{slug}");

            var routes = _routeRepository.GetMatchingRoutesAsync(pageUrls, LanguageList.BaseLanguage.Id).Result;

            var tourCategoryTemplate = RouteTemplateConstants.GetRouteTemplate(RouteTemplateConstants.No.TourCategory);
            var tourCategoryRoutes = _routeRepository.GetSitemapRoutesByRouteTemplateAsync(tourCategoryTemplate, LanguageList.BaseLanguage.Id).Result;
            //page sitemape tourCategory dahil etmek için yazdım -mehmet ali

            routes.AddRange(tourCategoryRoutes);
            return routes;
        }
        string GetLastModifiedDate(SitemapInfo sitemap)
        {
            if (!sitemap.IsMainLocation || sitemap.RouteTemplateInfo is not null)
            {
                var route = _routeRepository.GetAll(x => x.Controller == sitemap.RouteTemplateInfo.Controller && x.Action == sitemap.RouteTemplateInfo.Action).OrderByDescending(x => x.UpdateDate).ThenByDescending(x => x.CreateDate).FirstOrDefault();

                if (route is not null)
                    return route.UpdateDate is null ? route.CreateDate.ToString() : route.UpdateDate.Value.ToString();
            }
            return DateTime.Now.ToString();
        }

        RouteTemplateConstants.No GetRouteTemplateEnum(SitemapInfo sitemap)
        {
            return RouteTemplateConstants.Templates.Where(x => x.Value.Action == sitemap.RouteTemplateInfo.Action && x.Value.Controller == sitemap.RouteTemplateInfo.Controller).FirstOrDefault().Key;
        }
    }
}
