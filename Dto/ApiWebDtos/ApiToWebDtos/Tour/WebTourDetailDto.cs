using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class WebTourDetailDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public Guid ProductID { get; set; }
        public Guid TourId { get; set; }
        public string BannerImagePath { get; set; }
        public string TourTitle { get; set; }
        public List<TourCategoryDto> Categories { get; set; }
        public List<TourTagDto> Tags { get; set; }
        public decimal TourPrice { get; set; }
        public List<TourPriceDto> TourAllPrices { get; set; } //turla alakalı bütün fiyatlar gelsin
        public TourPriceDto TourMinPrice { get; set; } // mininum parayıda hesapladım direk doldurmak için
        public bool IsPerPerson { get; set; }
        public bool IsPerDay { get; set; }
        public string Duration { get; set; }
        public int DurationDay { get; set; }
        public int? PersonLimit { get; set; }
        public bool IsPersonLimited { get; set; }
        public string Content { get; set; }
        public List<string> Destination { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public List<string> Includes { get; set; }
        public List<string> Excludes { get; set; }
        public List<string> SightsToSee { get; set; }
        public string CancellationPolicy { get; set; }
        public List<WebTourDetailFaqDto> Faqs { get; set; }
        public List<WebTourDetailProgramDto> Programs { get; set; }
        public List<WebTourDetailImageDto> Images { get; set; }
        public List<WebTourDetailTripadvisorCommenDto> TripadvisorComments { get; set; }
        public List<WebTourDetailBlogDto> RelatedBlogs { get; set; }
        public List<AdditionalServiceDto> AdditionalServices { get; set; }
        public List<SelectListOption> StartTimeSelectList { get; set; }
        public int SuggestedStartTimeID { get; set; }
        public List<SelectListOptionDto> HowFindUsSelectList { get; set; }
        public List<SelectListOptionDto> AlsoInterestedSelectList { get; set; }
        public List<SelectListOption> DurationSelectList { get; set; }
        public int CutoffHour { get; set; }
        public DateTime SelectedDate { get; set; }
        public TimeOnly SelectedTime { get; set; }
        public string Segment { get; set; }
        public string Type { get; set; }
        public bool AskForPrice { get; set; }
    }
    public class WebTourDetailImageDto
    {
        public string ImagePath { get; set; }
        public int Order { get; set; }
    }
    public class WebTourDetailFaqDto
    {
        public int Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class WebTourDetailProgramDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Day { get; set; }
    }

    public class WebTourDetailTripadvisorCommenDto
    {
        public string Sender { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
    public class WebTourDetailBlogDto
    {
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string CardImagePath { get; set; }
    }
}
