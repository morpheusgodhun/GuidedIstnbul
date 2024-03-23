using Core.Entities;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ITourPriceService : IGenericService<TourPrice>
    {
        public List<TourPriceDto> TourAllPricesList(Guid tourId); // tüm fiyatları listeleyelim
        public TourPriceDto TourMinPriceForList(Guid tourId); ////turların listelendiği yerlerde bunu kullanarak fiyat çekelim
        public TourPriceDto TourMinPriceForList(List<TourPriceDto> tourPriceItemList);



        // public TourPriceDto TourAllPricesForDetail(Guid tourId); ////turların detayındaki tüm fiyatları çeken json

        public decimal TourPriceForDays(Guid tourId, int daycount);

    }
}
