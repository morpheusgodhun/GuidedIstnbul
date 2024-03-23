using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Tour
{
    public class GetTourDetailDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerTitle { get; set; }
        public string BannerSubtitle { get; set; }
        public List<TourCategoryDto> Categories { get; set; }
        public List<TourTagDto> Tags { get; set; }
        public TourDetailGeneralInformation InformationContent { get; set; }
        public List<TourDetailPlan> TourPlanContent { get; set; }
        public List<TourDetailGallery> GalleryContent { get; set; }
        public TourDetailReview ReviewContent { get; set; }
        public TourDetailBookTour BookTour { get; set; }
        

    }

    public class TourDetailGeneralInformation
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public bool IsPerPerson { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
       // public int PersonLimit { get; set; }
        //public List<TourCategoryDto> Categories { get; set; }
        public string Description { get; set; }
        public List<string> Destination { get; set; }
        public string StartPoint { get; set; }

        public string DepartureReturnLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime FinishTime { get; set; }
        public List<string> Includes { get; set; }
        public List<string> Excludes { get; set; }


        public TourDetailFaq TourFaq { get; set; }
    }
    public class TourDetailFaq
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<TourDetailFaqItem> TourFaqs { get; set; }
    }
    public class TourDetailFaqItem
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class TourDetailPlan
    {
        public int Day { get; set; }
        public string DayTitle { get; set; }
        public string DayContent { get; set; }
    }
    public class TourDetailGallery
    {
        public string ImagePath { get; set; }
        public int Order { get; set; }
    }
    public class TourDetailReview
    {
        public List<TourComment> Comments { get; set; }
        public TourCommentForm CommentForm { get; set; }
    }
    public class TourComment
    {
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
    public class TourCommentForm
    {
        public List<ConstantValueDto> ConstantValues { get; set; }

    }
    public class TourDetailBookTour
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public List<string> Languages { get; set; }
        public List<SelectListOptionDto> HowFindUsOptions { get; set; }
        public List<SelectListOptionDto> InterestedOptions { get; set; }

    }
}
