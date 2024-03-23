using Dto.ApiWebDtos.ApiToWebDtos.About;
using Dto.ApiWebDtos.GeneralDtos;


namespace Dto.ApiWebDtos.ApiToWebDtos.Layout
{
    public class NavbarDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string HomePage { get; set; }
        public string About { get; set; }
        public string Service { get; set; }
        public string Blog { get; set; }
        public string Faq { get; set; }
        public string Contact { get; set; }

        public string AboutSlug { get; set; }
        public string ServiceSlug { get; set; }
        public string BlogSlug { get; set; }
        public string FaqSlug { get; set; }
        public string ContactSlug { get; set; }
        public List<NavbarTourCategoryDto> Categories { get; set; }
        public NavbarTourCategoryDto IstanbulTour { get; set; }
    }
    public class NavbarTourCategoryDto
    {
        public string CategoryName { get; set; }
        public Guid CategoryID { get; set; }
        public string Slug { get; set; }
    }
}
