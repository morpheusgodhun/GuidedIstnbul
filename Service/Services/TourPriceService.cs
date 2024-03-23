using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Migrations;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TourPriceService : GenericService<TourPrice>, ITourPriceService
    {
        private readonly ITourPriceRepository _tourPriceRepository;
        private readonly ITourPriceItemService _tourPriceItemService;
        //private readonly ITourRepository _tourRepository;


        public TourPriceService(IGenericRepository<TourPrice> repository, IUnitOfWork unitOfWork, ITourPriceRepository tourPriceRepository, ITourPriceItemService tourPriceItemService) : base(repository, unitOfWork)
        {
            _tourPriceRepository = tourPriceRepository;
            _tourPriceItemService = tourPriceItemService;
            //_tourRepository = tourRepository;
        }

        public decimal TourPriceForDays(Guid tourId, int daycount)
        {
            decimal totalPrice = 0;
            /*var listTourPrice = _tourPriceRepository.GetHighPriorityTourPriceWithTourId(tourId, daycount);
            foreach (var item in listTourPrice)
            {
                var tourPriceItem = _tourPriceItemService.Where(x => x.TourPriceID == item.Id).FirstOrDefault();//todo burada personpolicye görede çekmek gerekiyor! - Çağdaş
                totalPrice += tourPriceItem.Price;
            }*/
            return totalPrice;
        }


        //bir turun tüm fiyatlarını çekmek için kullanıyorum
        public List<TourPriceDto> TourAllPricesList(Guid tourId)
        {
            var tourPriceItemList = _tourPriceRepository.GetTourPriceList(tourId); // bugünü içeren tüm fiyatlar çekilir
            return tourPriceItemList;
        }


        //listeleme işlemlerinde o turun min priceını bulacağım
        public TourPriceDto TourMinPriceForList(List<TourPriceDto> tourPriceItemList)
        {
            TourPriceDto bestDate = CalculateBestDate(tourPriceItemList);
            return bestDate;
        }

        public TourPriceDto TourMinPriceForList(Guid tourId)
        {
            var tourPriceItemList = TourAllPricesList(tourId); //yukarıdaki fiyat çekmeyi kullanıyorum
            TourPriceDto bestDate = CalculateBestDate(tourPriceItemList);
            return bestDate;
        }















        //gelen fiyalar arasındaki tüm günleri dolaşıp en düşüğü bulmaya çalışıyorum
        static TourPriceDto CalculateBestDate(List<TourPriceDto> priceList)
        {

            if (priceList.Count == 0)
            {
                return new TourPriceDto();
            }

            decimal minPrice = int.MaxValue;
            DateTime bestDate = DateTime.MinValue;

            var selected = new TourPriceDto();

            var minDate = priceList.Select(z => z.FromDate).Min();
            /*if (minDate < DateTime.Today)  TODO: burayı açacağım -- niye böyle yazmışım hatırlayamadım o sebeple açmadım
                minDate = DateTime.Today;*/
            var maxDate = priceList.Select(z => z.ToDate).Max();

            for (DateTime currentDate = minDate; currentDate <= maxDate; currentDate = currentDate.AddDays(1))
            {
                var currentPrice = GetLowestPrice(priceList, currentDate);
                if (currentPrice is not null && currentPrice.Price < minPrice)
                {
                    selected = currentPrice;
                    minPrice = currentPrice.Price;
                    bestDate = currentDate;
                }
            }

            return selected;
        }

        static TourPriceDto GetLowestPrice(List<TourPriceDto> priceList, DateTime inputDate)
        {
            /* var matchingReservations = priceList
                 .Where(prc => inputDate >= prc.FromDate && inputDate <= prc.ToDate).ToList()
                 .OrderByDescending(prc => prc.Priorty).Take(3).OrderBy(prc => prc.Price); /// TODO:burada take 3 şartı pergroup sayısına göre güncellenmeli mi gibi bir durum var.
            */

            var matchingReservations = priceList
               .Where(prc => inputDate >= prc.FromDate && inputDate <= prc.ToDate).ToList()
               .OrderByDescending(prc => prc.Priorty).ThenBy(prc => prc.Price);


            if (matchingReservations.Any())
            {
                return matchingReservations.First();//.Price;
            }

            return null;
        }
    }
}
