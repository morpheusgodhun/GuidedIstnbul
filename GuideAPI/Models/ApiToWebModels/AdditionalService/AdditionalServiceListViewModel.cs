namespace GuideAPI.Models.ApiToWebModels.AdditionalService
{
    public class AdditionalServiceListViewModel
    {
        public string BannerImagePath { get; set; } //Fixed Value
        public string BannerTitle { get; set; }  //Fixed Value
        public string BannerSubtitle { get; set; } //Fixed 
        public string AdditionalServiceDetailButton { get; set; } // Fixed Value
        public string AdditionalServiceLabelFromButton { get; set; } // Fixed Value
        public List<_AdditionalServiceListItem> AdditionalServices { get; set; }
    }
}
