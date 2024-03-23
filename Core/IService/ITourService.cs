using Core.Entities;
using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;

namespace Core.IService
{
    public interface ITourService : IGenericService<Tour>
    {
        public Task<WebTourDetailDto> GetTourDetail(Guid tourId, int languageId);
        public Task<List<TourListDto>> BestDealTourListAsync(int languageId);
        public Task<List<TourListDto>> PrivateTourListAsync(int languageId);
        public Task<List<TourListDto>> TurkeyTourListAsync(int languageId);
        public GetTourFilterDto TourFilters(int languageId);
        public GetTourFilterDto TourListFiltered(GetTourFilterFormDto filters, int languageId);
        public void EditTour(EditTourDto editTourDto);
    }
}
