using Core.Entities;
using Core.StaticClass;
using Dto;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuideWeb.APIHandler;
using Humanizer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace GuideWeb.Helpers.Routing
{
    public class RouteHelper : IRouteHelper
    {
        //301 urls --> all from guidedistanbultours.com/sitemap_index.xml
        readonly List<string> RedirectUrls = new List<string>
        {
           #region tours
            //tours
            "tour/best-of-istanbul-in-1-2-or-3-day-private-guided-istanbul-tour",
            "tour/private-istanbul-tour",
            "tour/istanbul-tour-small-group",
            "tour/private-4-days-turkey-tour-from-istanbul-to-cappadocia-ephesus-pamukkale",
            "tour/best-of-cappadocia-1-2-or-3-day-private-guided-cappadocia-tour",
            "tour/private-2-days-cappadocia-tour-from-istanbul-optional-hot-air-balloon",
            "tour/private-biblical-seven-churches-of-turkey-tour-in-5-days",
            "tour/istanbul-layover-tour",
            "tour/istanbul-shore-excursion-1-or-2-day-private-guided-tour-with-private-van",
            "tour/private-ephesus-kusadasi-shore-excursion-tour",
            "tour/1-day-private-gobeklitepe-tour",
            "tour/istanbul-luxury-yacht-bosphorus-cruise",
            "tour/daily-istanbul-tour",
            "tour/private-mt-nemrut-gobeklitepe-tour-1-day-1-night",
            "tour/half-day-istanbul-private-food-tour-culinary-experience",
            "tour/istanbul-bosphorus-tour",
            "tour/6-days-private-turkey-tour-istanbul-cappadocia",
            "tour/istanbul-private-helicopter-tour",
            "tour/private-daily-cappadocia-tour-from-istanbul",
            "tour/10-days-private-turkey-tour-package",
            "tour/2-days-private-ephesus-and-pamukkale-tour",
            "tour/1-day-private-konya-tour",
            "tour/7-day-private-turkey-tour",
            "tour/full-day-private-gallipoli-tour-from-istanbul",
            "tour/full-day-private-tour-of-troy-from-istanbul",
            "tour/shore-excursion-1-or-2-day-private-tour-from-kusadasi-port-for-ephesus-pergamon",
            "tour/21-days-highlights-of-turkey-tour-private-package",
            "tour/1-day-private-pamukkale-tour",
            "tour/4-days-private-turkey-tour-gallipoli-troy-pergamon-ephesus-and-pamukkale",
            "tour/3-days-private-antalya-tour",
            "tour/9-days-private-turkey-tour-package",
            "tour/5-days-private-turkey-tour-istanbul-ephesus-cappadocia",
            "tour/6-days-private-turkey-tour-istanbul-ephesus-and-pamukkale",
            "tour/14-days-best-of-turkey-private-tour",
            "tour/a-5-hour-private-luxury-yacht-cruise-on-the-bosphorus-strait-of-istanbul",
            "tour/private-daily-ephesus-tour-from-istanbul",
            "tour/2-days-private-gallipoli-and-troy-tour",
            "tour/12-days-best-of-turkey-private-tour",
            "tour/20-days-private-western-turkey-tour-package",
            "tour/cappadocia-classics-you-cant-miss",
            "tour/3-days-private-turkey-tour-gallipoli-troy-pergamon-and-ephesus",
            "tour/15-days-turkey-excursion-package-on-private-basis",
            "tour/31-days-private-turkey-israel-greece-tour-package",
            "tour/ephesus-classics-you-cant-miss",
#endregion
            #region categories
            //categories
            "category/covid/",
            "category/events/",
            "category/places-to-visit/",
            "category/tips-for-travellers/",
            "category/top-tours/",
            "category/turkish-cuisine/",
            "category/turkish-culture/",
            "category/uncategorized/",
#endregion
            #region posts
            "does-turkey-require-pcr-test",
            "how-many-people-live-in-istanbul",
            "what-is-the-weather-like-in-istanbul",
            "where-are-the-best-places-to-stay-in-istanbul",
            "how-to-make-turkish-tea",
            "hot-air-balloon-flights-resume-in-turkey-2020",
            "will-istanbul-host-formula-1-in-2020",
            "sultanahmet-from-istanbul-airport",
            "iconic-hagia-sophia-turns-into-a-mosque",
            "how-to-get-from-sabiha-gokcen-airport-to-istanbul-airport",
            "how-to-spend-3-days-in-istanbul",
            "what-to-see-in-the-asian-side-of-istanbul",
            "sultanahmet",
            "biblical-history-of-turkey",
            "hagia-sophia-mosque-again",
            "istanbul-earthquake-1999",
            "where-to-buy-turkish-delight",
            "what-to-eat-in-istanbul",
            "shopping-in-istanbul-where-and-what-to-buy",
            "when-did-constantinople-became-istanbul",
            "istanbul-museums",
            "how-can-i-go-from-istanbul-to-cappadocia",
            "what-to-do-in-istanbul-in-3-days",
            "how-to-get-to-asian-side-of-istanbul",
            "are-istanbul-airport-taxis-safe",
            "does-istanbul-airport-have-free-wifi",
            "how-was-the-conquest-of-istanbul",
            "does-istanbul-airportiga-have-a-hotel",
            "how-to-attend-a-football-match-in-istanbul",
            "what-is-istanbul-museum-pass",
            "most-famous-street-foods-in-istanbul",
            "do-i-need-a-tour-guide-in-istanbul",
            "what-to-do-in-istanbul-at-night",
            "are-constantinople-and-istanbul-the-same-place",
            "epiphany-ceremony-in-balat",
            "how-to-travel-from-istanbul-airport-to-city",
            "how-much-money-to-take-on-holiday-to-istanbul",
            "how-safe-is-it-travel-to-istanbul",
            "why-travel-to-istanbul",
            "what-is-bosphorus-in-istanbul",
            "public-transportation-in-istanbul",
            "delicious-traditional-turkish-desserts",
            "turkish-delight",
            "how-to-visit-mosques-in-istanbul-and-turkey",
            "istanbuls-7-fascinating-fountains",
            "the-best-diving-sites-in-turkey",
            "what-is-the-turkish-hat",
            "10-things-know-before-go-to-istanbul",
            "2023-uefa-champions-league-final-istanbul",
            "4-days-in-turkey-the-best-itinerary-for-4-days-turkey-trip",
            "5-reasons-to-visit-turkey",
            "aqueducts-to-visit-in-istanbul",
            "are-restaurants-in-istanbul-open-during-ramadan-2021",
            "backstreets-of-istanbul",
            "benefits-of-private-tour-in-istanbul",
            "best-parks-in-istanbul-turkey",
            "best-place-for-exchange-in-istanbul",
            "best-places-in-turkey-for-instagrammers",
            "best-turkish-baths-hammams-in-istanbul",
            "budget-economic-tours-in-istanbul",
            "cable-car-teleferik-fees-turkey-2022",
            "covid-restrictions-during-ramadan",
            "covid-restrictions-for-turkey-in-april",
            "distance-between-ephesus-and-kusadasi",
            "does-uber-works-in-istanbul",
            "easter-in-istanbul",
            "entrance-form-to-turkey-march-2021",
            "excellence-journey-istanbul-tours",
            "galataport-istanbul-cruise-port",
            "get-access-to-free-wifi-in-istanbul",
            "henna-in-istanbul",
            "hidden-gems-in-istanbul-for-tourists",
            "honeymoon-in-cappadocia-turkey",
            "how-do-i-get-from-istanbul-to-ephesus",
            "how-to-buy-prepaid-turkish-sim-card",
            "how-to-get-from-istanbul-to-antalya",
            "how-to-get-istanbul-kart",
            "how-to-get-to-bursa-from-istanbul",
            "how-to-go-from-istanbul-to-pamukkale",
            "how-to-go-to-troy-from-istanbul",
            "how-to-make-turkish-coffee-at-home",
            "how-to-spend-a-10-days-holiday-in-turkey",
            "how-to-spend-a-week-in-turkey",
            "how-to-use-high-speed-rail-fast-train-during-my-turkey-tour",
            "how-to-visit-blue-mosque",
            "how-to-visit-gobekli-tepe-turkey",
            "information-of-istanbul-museums",
            "is-a-helicopter-tour-available-in-istanbul",
            "is-basilica-cistern-open-updated-2022",
            "is-istanbul-gay-friendly",
            "is-it-safe-to-rent-a-car-in-turkey",
            "is-it-suggested-to-have-a-tour-guide-in-istanbul",
            "is-st-george-from-cappadocia",
            "istanbul-layover-activities-and-tours",
            "istanbul-shore-excursions",
            "kids-family-activities-in-istanbul",
            "kusadasi-cruise-port-shore-excursions",
            "last-coronavirus-news-to-travel-turkey",
            "lycian-way-hiking-trail-turkey",
            "most-important-archaeological-sites-in-turkey",
            "new-covid-19-travel-restrictions-for-turkey",
            "no-lockdown-in-turkey-from-july-1st",
            "places-for-the-best-view-of-istanbul",
            "princes-islands",
            "recent-covid-19-status-in-turkey",
            "recent-covid-restrictions-in-turkey",
            "recent-covid-restrictions-in-turkey-2",
            "taxis-in-istanbul",
            "the-best-restaurants-of-istanbul",
            "the-best-turkish-tv-series-on-netflix",
            "the-biggest-shopping-mall-in-istanbul",
            "the-cruises-on-the-golden-horn-halic-2",
            "the-current-status-of-chora-church",
            "tourist-traps-in-istanbul",
            "turkey-applies-nationwide-lockdown-till-may-17",
            "turkey-covid-restrictions-for-travelers",
            "turkey-eases-covid-restrictions-by-june",
            "turkey-tends-to-normalize-by-march",
            "turkey-wonders-exploration",
            "visiting-istanbul-in-lockdown",
            "visiting-suleymaniye-mosque-istanbul",
            "visiting-topkapi-palace-museum",
            "what-is-must-not-do-while-visiting-istanbul",
            "what-is-turkish-breakfast",
            "what-to-see-in-antalya",
            "what-to-see-in-bursa",
            "what-to-see-in-istanbul",
            "what-to-see-in-istiklal-street",
            "what-to-see-in-safranbolu-turkey",
            "what-to-wear-in-turkey-visit",
            "when-is-ramadan-eid-in-turkey-2023",
            "when-is-sacrifice-eid-in-turkey-2023",
            "when-is-the-tulip-festival-in-istanbul",
            "where-does-turkish-airlines-fly-to",
            "where-to-go-europe-for-christmas-2023",
            "where-to-ski-in-turkey",
            "which-is-best-travel-agency-for-istanbul-tours",
            "which-palaces-can-i-visit-in-istanbul",
            "which-souvenirs-to-buy-from-turkey",
            "why-do-you-need-a-tour-guide-in-ephesus",
            "why-rent-private-yacht-for-bosphorus",
            "will-cruises-be-sailing-in-2021",
            "will-i-need-a-tour-guide-in-cappadocia",
            //pages 
            "blog/",
            "frequently-asked-questions",
            "contact-us/",
            "gallipoli-troy-tour/",
            "turkey-tours",
            "pamukkale-tours",
            "istanbul-tour/",
            "ephesus-tours",
            "cappadocia-tours",
            "about-us",
            "cancellation-policy",
            "privacy-policy",
            "terms-and-conditions",
#endregion
            "istanbul-tours",
            "pamukkale-tours",
            "ephesus-tours",
            "cappadocia-tours",
            "turkey-tours",
            "gallipoli-troy-tours",
            "contact-us",
            "about-us",
            "cancellation-policy",
            "privacy-policy",
            "terms-and-conditions",

        };

        readonly IConfiguration _configuration;
        public RouteHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<RouteTemplateDto> GetRouteTemplateAsync(string path) //path "/" ile başlıyor
        {
            if (path.StartsWith('/') && path.Length > 1)
            {
                path = path[1..];
            }

            HttpClient client = new();
            string url = $"{_configuration["BaseUrl"]}Home/GetRoute";
            GetRouteTemplateDto slugDto = new(path);

            HttpResponseMessage response = await client.PostAsJsonAsync(url, slugDto);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            CustomResponseDto<RouteTemplateDto> deserializedResponse = JsonConvert.DeserializeObject<CustomResponseDto<RouteTemplateDto>>(responseString);
            return deserializedResponse.Data;
        }

        public RouteTemplateDto ConvertPathToRouteTemplate(string path) //controller/action/gud id
        {
            string[] pathArray = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string controller = pathArray[0];
            string action = pathArray[1];

            if (Guid.TryParse(pathArray[2], out Guid entityId))
                return new(controller, action, entityId);

            return null;
        }

        public bool IsTourRedirectUrlsContains(string url)
        {
            return RedirectUrls.Select(rdrUrl => TrimUrl(rdrUrl)).Contains(TrimUrl(url));
        }

        static string TrimUrl(string url)
        {
            if (url.StartsWith('/'))
                url = url[1..];

            if (url.EndsWith("/"))
                url = url.TrimEnd('/');

            return url;
        }
    }
    public interface IRouteHelper
    {
        Task<RouteTemplateDto> GetRouteTemplateAsync(string slugUrl);
        bool IsTourRedirectUrlsContains(string url);
    }
}
