using Core.Entities;
using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;

namespace Core.IRepository
{
    public interface ITourRepository : IGenericRepository<Tour>
    {
        Task<WebTourDetailDto> GetTourDetail(Guid tourId, int languageId);
        Task<List<TourListDto>> BestDealTourListAsync(int languageId);
        Task<List<TourListDto>> PrivateTourListAsync(int languageId);
        Task<List<TourListDto>> TurkeyTourListAsync(int languageId);
        GetTourFilterDto TourFilters(int languageId);
        GetTourFilterDto TourListFiltered(GetTourFilterFormDto filters, int languageId);
        void EditTour(EditTourDto editTourDto);
        // void UpdateStartTimes();
        Task<WebTourDetailDto> GetTourDetailAdditionals(Guid tourId, int languageId);
    }
}
