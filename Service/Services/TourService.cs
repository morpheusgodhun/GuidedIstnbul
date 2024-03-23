using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Dto.ApiPanelDtos.ProductDtos.TourDtos;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;

namespace Service.Services
{
    public class TourService : GenericService<Tour>, ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourPriceService _tourPriceService;
        private readonly IAdditionalServiceOptionPriceService _additionalServiceOptionPriceService;

        public TourService(IGenericRepository<Tour> repository, IUnitOfWork unitOfWork, ITourRepository tourRepository, ITourPriceService tourPriceService, IAdditionalServiceOptionPriceService additionalServiceOptionPriceService) : base(repository, unitOfWork)
        {
            _tourRepository = tourRepository;
            _tourPriceService = tourPriceService;
            _additionalServiceOptionPriceService = additionalServiceOptionPriceService;
        }

        public async Task<List<TourListDto>> BestDealTourListAsync(int languageId)
        {
            var tourlist = await _tourRepository.BestDealTourListAsync(languageId);

            tourlist.Select(c =>
            {
                c.Price = c.AskForPrice ? 0 : _tourPriceService.TourMinPriceForList(c.TourId).Price;
                return c;
            }).
                OrderBy(x => x.Order).ToList();
            return tourlist;
        }

        public void EditTour(EditTourDto editTourDto)
        {
            _tourRepository.EditTour(editTourDto);
            _unitOfWork.saveChanges();
        }

        public async Task<WebTourDetailDto> GetTourDetail(Guid tourId, int languageId)
        {
            //tur detayları geldi
            var tourDetail = await _tourRepository.GetTourDetail(tourId, languageId);

            var tourAdditionals = await _tourRepository.GetTourDetailAdditionals(tourId, languageId);

            // tura iat tüm fiyatları çektim
            var tourAllPriceList = _tourPriceService.TourAllPricesList(tourDetail.TourId);
            tourDetail.TourAllPrices = tourAllPriceList;
            //min fiyatını hesaplayalım
            tourDetail.TourMinPrice = _tourPriceService.TourMinPriceForList(tourAllPriceList); //tekrar fiyat çekmeyelim
            tourDetail.TourPrice = tourDetail.TourMinPrice.Price;


            //tüm option fiyatlarını çekip minpice olarak bireyere yazmam lazım
            tourDetail.AdditionalServices = _additionalServiceOptionPriceService.SetOptionalPricesForTourDetail(tourAdditionals.AdditionalServices);

            return tourDetail;
        }

        public async Task<List<TourListDto>> PrivateTourListAsync(int languageId)
        {
            var tourlist = await _tourRepository.PrivateTourListAsync(languageId);
            tourlist.Select(c => { c.Price = c.AskForPrice ? 0 : _tourPriceService.TourMinPriceForList(c.TourId).Price; return c; }).ToList();
            return tourlist;
        }

        public GetTourFilterDto TourFilters(int languageId)
        {
            return _tourRepository.TourFilters(languageId);
        }

        public GetTourFilterDto TourListFiltered(GetTourFilterFormDto filters, int languageId)
        {
            var tourlist = _tourRepository.TourListFiltered(filters, languageId);
            tourlist.Tours.Select(c => { c.Price = c.AskForPrice ? 0 : _tourPriceService.TourMinPriceForList(c.TourId).Price; return c; }).ToList();
            return tourlist;
        }


        public async Task<List<TourListDto>> TurkeyTourListAsync(int languageId)
        {
            var tourlist = await _tourRepository.TurkeyTourListAsync(languageId);
            tourlist.Select(c => { c.Price = c.AskForPrice ? 0 : _tourPriceService.TourMinPriceForList(c.TourId).Price; return c; }).ToList();
            return tourlist;
        }
    }
}
