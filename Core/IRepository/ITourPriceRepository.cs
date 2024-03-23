using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ITourPriceRepository : IGenericRepository<TourPrice>
    {
        public List<TourPriceDto> GetTourPriceList(Guid tourId); // bu min price için hepsini getiriyor
        public TourPricesDto GetTourPriceListJson(Guid tourId); // buda detail için json formatına uyacak şekilde getiriyor

        List<TourPrice> GetHighPriorityTourPriceWithTourId(Guid id, int daycount);
    }
   

}
