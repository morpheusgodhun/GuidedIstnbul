using Core.StaticValues;
using Dto.ApiPanelDtos.AdditionalServiceDtos;
using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GuideWeb.Controllers
{
    public class TourController : CustomBaseController
    {
        private readonly ICookie _cookie;

        public TourController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
            _cookie = cookie;
        }

        public async Task<IActionResult> Index(string id)
        {

            int languageId = 1;

            string url = _url + "WebTour/GetCategoryTourList/" + id + "/" + languageId;

            CustomResponseDto<GetCategoryTourListDto> getCategoryTourListDto = await _apiHandler.GetAsync<CustomResponseDto<GetCategoryTourListDto>>(url);

            if (getCategoryTourListDto == null)
            {
                return View();
            }
            else
            {
                return View(getCategoryTourListDto.Data);
            }
        }
        public async Task<IActionResult> TourDetail(string id)
        {
            int languageId = 1;
            //HttpContext.Request.Path

            string url = _url + "WebTour/TourDetail/" + id + "/" + languageId;

            CustomResponseDto<WebTourDetailDto> webTourDetailDto = await _apiHandler.GetAsync<CustomResponseDto<WebTourDetailDto>>(url);
            var webTour = webTourDetailDto.Data;


            //Önemli not
            //tur detayında additionalların herbiri için special kişi sayısı özelliği olmalı bu özellik için ayrı bir yapı tutacağız
            //aslında option yapısını kullanarak veri basıp ve alacağım ama özel bir guid ile onları alıp kişi sayısı sutununa basacağım
            /* foreach (var additional in webTour.AdditionalServices)
             {

             }*/

            //AdditionalServiceOptionInputDto SpecialPaticipantCount = new() { InputID = new Guid("cd051d10-5658-4476-afae-6def53ad9a05"), InputName = "Participant Number", InputType = 3, Order = 0 };
            //webTour.AdditionalServices.ForEach(a => a.Options.ForEach(o => o.Inputs.Add(SpecialPaticipantCount)));
            //var optionsinputs = additionalService.Options.SelectMany(option => option.Inputs).Distinct().OrderBy(x => x.Order).ToList();


            //webTour.SuggestedStartTimeID = 1

            #region seçili tarih ve saati ayarlama

            var minSelectableDateTime = DateTime.Now.AddHours(webTour.CutoffHour); // seçilebilecek en küçük günü ayarlıyoruz

            TimeOnly selectedTime = TimeOnly.Parse(webTour.StartTimeSelectList.Select(z => z.Value).FirstOrDefault());
            // tavsiye ile gelen saat değerini çevirelim
            //TODO: eğer SuggestedStartTimeID yoksa uymuyorsa hataya düşmesin default biişi atasın
            try
            {
                if (TimeSpan.TryParseExact(webTour.StartTimeSelectList.Where(x => x.ID == webTour.SuggestedStartTimeID).FirstOrDefault().Value, "hh\\:mm", null, out TimeSpan timeSpan)) { selectedTime = TimeOnly.FromTimeSpan(timeSpan); }
            }
            catch (Exception)
            {
            }

            //seçilen gün ve saat - cuttof hesaplamasından büyük ise onu verelim devam edelim.
            DateTime combinedDateTime = webTour.TourMinPrice.FromDate + selectedTime.ToTimeSpan();

            //if (webTour.TourMinPrice.FromDate > DateTime.Now.AddHours(webTour.CutoffHour))
            if (combinedDateTime >= minSelectableDateTime)//eğer minpricedate > cutofftandate büyükse - minprice gününü getirelim 
            {
                webTour.SelectedDate = webTour.TourMinPrice.FromDate;
                webTour.SelectedTime = selectedTime;
            }
            else
            {

                //saat için minsaatinden büyük olan en küçük saat değerini seçelim
                var minTimeOnly = TimeOnly.FromTimeSpan(minSelectableDateTime.TimeOfDay);

                ////eğer büyük saat varsa onu arayalım yoksa. 
                //günü bir sonraki gün yapıp saati suggestion saatine çevirelim
                webTour.SelectedDate = minSelectableDateTime.AddDays(1).Date;
                webTour.SelectedTime = selectedTime;

                foreach (var option in webTour.StartTimeSelectList)
                {
                    if (TimeSpan.TryParseExact(option.Value, "hh\\:mm", null, out TimeSpan timeSpan2))
                    {
                        var timeOnly = TimeOnly.FromTimeSpan(timeSpan2);
                        if (timeOnly > minTimeOnly)
                        {
                            webTour.SelectedDate = minSelectableDateTime.Date;
                            webTour.SelectedTime = TimeOnly.FromTimeSpan(timeSpan2);
                            break;
                        }
                    }
                }

            }

            #endregion


            if (webTourDetailDto == null)
            {
                return View();
            }
            else
            {
                return View("TourDetailOld", webTour); // eski sayfayı aldım çünkü tasarımı patlamıştı
            }
        }

        public async Task<IActionResult> TourFilter()
        {
            int languageId = 1;
            string url = _url + "WebTour/GetTourFilter/" + languageId;

            CustomResponseDto<GetTourFilterDto> getTourFilterDto = await _apiHandler.GetAsync<CustomResponseDto<GetTourFilterDto>>(url);

            if (getTourFilterDto == null)
            {
                return View();
            }
            else
            {
                return View(getTourFilterDto.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TourFilterList(GetTourFilterFormDto filters)
        {
            int languageId = 1;
            string url = _url + "WebTour/GetFilteredTourList";

            CustomResponseDto<GetTourFilterDto>? filteredList = _apiHandler.PostApi<CustomResponseDto<GetTourFilterDto>>(filters, url);

            #region yedek
            //List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? filter1 = new();
            //if (filters.Categories is not null)
            //{
            //    filter1 = filteredList.Data.Tours.Where(x => x.CategoryIDs.Any(x => filters.Categories.Contains(x))).ToList();
            //}
            //if (filters.Specials is not null)
            //{
            //    var filter2 = filter1.Where(x => x.InclusionIDs.Any(x => filters.Categories.Contains(x))).ToList();
            //}
            //List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? specialFilter = new List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>();
            //List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? categoryFilter = new List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>();
            //List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? destinationFilter = new List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>();
            //List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? TourTypeFilter = new List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>(); 
            #endregion
            List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? specialFilter = filteredList.Data.Tours;
            List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? categoryFilter = filteredList.Data.Tours;
            List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? destinationFilter = filteredList.Data.Tours;
            List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto>? TourTypeFilter = filteredList.Data.Tours;
            if (filters.Specials is not null)
            {
                specialFilter = filteredList.Data.Tours.Where(x => x.InclusionIDs.Any(x => filters.Specials.Contains(x))).ToList();
            }
            if (filters.Categories is not null)
            {
                categoryFilter = filteredList.Data.Tours.Where(x => x.CategoryIDs.Any(x => filters.Categories.Contains(x))).ToList();
            }
            if (filters.Destinations is not null)
            {
                destinationFilter = filteredList.Data.Tours.Where(x => x.DurationIDs.Any(x => filters.Destinations.Contains(x))).ToList();
            }
            if (filters.Types is not null)
            {
                TourTypeFilter = filteredList.Data.Tours.Where(x => filters.Types.Contains(x.TourTypeId)).ToList();
            }


            // Burada specialFilter, categoryFilter, destinationFilter ve tourTypeFilter'e FilterTourDto nesnelerini ekleyin

            // TourId'leri eşleşen öğeleri bulmak için ortak bir TourId listesi oluşturun

            List<Guid> commonTourIds = specialFilter.Select(item => item.TourId)
          .Intersect(categoryFilter.Select(item => item.TourId))
          .Intersect(destinationFilter.Select(item => item.TourId))
          .Intersect(TourTypeFilter.Select(item => item.TourId))
          .ToList();
            #region Yedek

            // Ortak TourId'leri kullanarak yeni bir liste oluşturun
            //List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto> mergedList = (from item1 in specialFilter
            //                                                                   join item2 in categoryFilter on item1.TourId equals item2.TourId
            //                                                                   join item3 in destinationFilter on item1.TourId equals item3.TourId
            //                                                                   join item4 in TourTypeFilter on item1.TourId equals item4.TourId
            //                                                                   where commonTourIds.Contains(item1.TourId)
            //                                                                   select new Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto
            //                                                                   {
            //                                                                       ProductId = item1.ProductId,
            //                                                                       CardImagePath = item1.CardImagePath,
            //                                                                       Price = item1.Price,
            //                                                                       Rate = item1.Rate,
            //                                                                       TourName = item1.TourName,
            //                                                                       IsPerPerson = item1.IsPerPerson,
            //                                                                       CategoryNames = item1.CategoryNames,
            //                                                                       TagNames = item1.TagNames,
            //                                                                       CategoryIDs = item1.CategoryIDs,
            //                                                                       DurationIDs = item1.DurationIDs,
            //                                                                       InclusionIDs = item1.InclusionIDs,
            //                                                                       TourTypeId = item1.TourTypeId,
            //                                                                       TourId = item1.TourId
            //                                                                   }).ToList(); 
            #endregion

            List<Dto.ApiWebDtos.ApiToWebDtos.Tour.FilterTourDto> mergedList = filteredList.Data.Tours.Where(x => commonTourIds.Contains(x.TourId)).ToList();
            filteredList.Data.Tours = mergedList;
            return PartialView("_filterResult", filteredList.Data);

        }

        [HttpPost]
        public IActionResult CustomMadeTourRequest(CustomMadeTourPostDto customTour)
        {
            int languageId = 1;
            string url = _url + "WebTour/CustomMadeTourRequest";

            //member yapıyorsa bu işi membera bağlaylım ama resgele gelen biride yapabilecek
            //member yaparsa custom turlarım diye bir sayfa açalım orada görsün
            //giriş yapmamış biri yaparsa ona mail atacağız oradan tıklayıp görecek
            //TODO: bunu kontrol edelim

            var memberId = _cookie.getMemberId();
            if (!String.IsNullOrWhiteSpace(memberId))
            {
                customTour.MemberId = new Guid(memberId);
            }

            var request = _apiHandler.PostApi<CustomResponseDto<CustomMadeTourPostDto>>(customTour, url);

            //sonuçları takip edebilmesi için buraya yönlendireceğim
            return Redirect("/Reservation/CustomTourRequest/" + request.Data.RequestId);

        }


        [HttpPost]
        public IActionResult CustomMadeTourRequestComfirm(CustomTourOfferCustomerAnswer offerAnswer)
        {
            string url = _url + "WebTour/CustomMadeTourRequestAnswer";
            offerAnswer.Answer = new CustomTourOfferStatus().GetValue((int)CustomTourOfferStatus.OfferStatus.Confirmed);
            _apiHandler.PostApi<CustomResponseNullDto>(offerAnswer, url);
            //tekrar geldiği yere dönsün
            return Redirect("/Reservation/CustomTourRequest/" + offerAnswer.RequestId);
        }

        [HttpPost]
        public IActionResult CustomMadeTourRequestEdit(CustomTourOfferCustomerAnswer offerAnswer)
        {
            string url = _url + "WebTour/CustomMadeTourRequestAnswer";
            offerAnswer.Answer = new CustomTourOfferStatus().GetValue((int)CustomTourOfferStatus.OfferStatus.UpdateRequest);
            _apiHandler.PostApi<CustomResponseNullDto>(offerAnswer, url);
            //tekrar geldiği yere dönsün
            return Redirect("/Reservation/CustomTourRequest/" + offerAnswer.RequestId);
        }
        //

    }
}
