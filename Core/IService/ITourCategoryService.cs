using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ITourCategoryService : IGenericService<TourCategory>
    {
        public List<NavbarTourCategoryDto> GetTourCategoriesForNavbar(int languageId);
        public GetCategoryTourListDto GetTourListByCategory(Guid categoryId, int languageId);
        public string GetTourCategoryName(Guid id);
    }
}
