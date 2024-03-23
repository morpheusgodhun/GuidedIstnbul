using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TourCategoryService : GenericService<TourCategory>, ITourCategoryService
    {
        private readonly ITourCategoryRepository _tourCategoryRepository;
        private readonly ITourPriceService _tourPriceService;

        public TourCategoryService(IGenericRepository<TourCategory> repository, IUnitOfWork unitOfWork, ITourCategoryRepository tourCategoryRepository, ITourPriceService tourPriceService) : base(repository, unitOfWork)
        {
            _tourCategoryRepository = tourCategoryRepository;
            _tourPriceService = tourPriceService;
        }

        public List<NavbarTourCategoryDto> GetTourCategoriesForNavbar(int languageId)
        {
            return _tourCategoryRepository.GetTourCategoriesForNavbar(languageId);
        }

        public string GetTourCategoryName(Guid id)
        {
            return _tourCategoryRepository.GetTourCategoryName(id);
        }

        public GetCategoryTourListDto GetTourListByCategory(Guid categoryId, int languageId)
        {
            var tourlist = _tourCategoryRepository.GetTourListByCategory(categoryId, languageId);
            if (tourlist is not null)
                tourlist.Tours.Select(c => { c.Price = _tourPriceService.TourMinPriceForList(c.TourId).Price; return c; }).ToList();
            return tourlist;
        }
    }
}
