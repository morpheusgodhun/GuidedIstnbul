using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.Repository;
using Dto.ApiWebDtos.ApiToWebDtos.AdditionalService;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AdditionalServiceOptionPriceService : GenericService<AdditionalServiceOptionPrice>, IAdditionalServiceOptionPriceService
    {
        private readonly IAdditionalServiceOptionPriceRepository _additionalServiceOptionPriceRepository;

        public AdditionalServiceOptionPriceService(IGenericRepository<AdditionalServiceOptionPrice> repository, IUnitOfWork unitOfWork, IAdditionalServiceOptionPriceRepository additionalServiceOptionPriceRepository) : base(repository, unitOfWork)
        {
            _additionalServiceOptionPriceRepository = additionalServiceOptionPriceRepository;
        }

        public List<AdditionalServicePriceDto> OptionAllPriceList(Guid optionId)
        {
            var additionalOptionPriceItemList = _additionalServiceOptionPriceRepository.GetOptionPriceList(optionId); // bugünü içeren tüm fiyatlar çekilir
            return additionalOptionPriceItemList;
        }

        /* burasını lazım olursa açarım
        public AdditionalServicePriceDto OptionMinPriceForList(Guid optionId)
        {
            var optionAllPriceList = OptionAllPriceList(optionId); //yukarıdaki fiyat çekmeyi kullanıyorum
            AdditionalServicePriceDto bestDate = CalculateBestDate(optionAllPriceList);
            return bestDate;
        }*/

        public AdditionalServicePriceDto OptionMinPriceForList(List<AdditionalServicePriceDto> priceList)
        {
            AdditionalServicePriceDto bestDate = CalculateBestDate(priceList);
            return bestDate;
        }

       
        // tur detayı çekilirken ek servislerin -> optionların tüm priceleini çekmem lazım çünkü fiyat ona göre değişiyor.
        //tüm servisleri gönderiyorum, servisleri güncelleyip ger dönecepşm
        public List<AdditionalServiceDto> SetOptionalPricesForTourDetail(List<AdditionalServiceDto> additionalServices)
        {
            foreach (var service in additionalServices)
            {
                var options = service.Options;
                foreach (var option in options)
                {
                    var allPrices = OptionAllPriceList(option.OptionID);

                    option.OptionAllPrices = allPrices;
                    option.OptionPrice = OptionMinPriceForList(allPrices).Price;
                }
            }

            return additionalServices;

        }



        //gelen fiyalar arasındaki tüm günleri dolaşıp en düşüğü bulmaya çalışıyorum
        static AdditionalServicePriceDto CalculateBestDate(List<AdditionalServicePriceDto> priceList)
        {

            if (priceList.Count == 0)
            {
                return new AdditionalServicePriceDto();
            }

            decimal minPrice = int.MaxValue;
            DateTime bestDate = DateTime.MinValue;


            var selected = new AdditionalServicePriceDto();

            var minDate = priceList.Select(z => z.FromDate).Min();
            if (minDate < DateTime.Today)
                minDate = DateTime.Today;
            var maxDate = priceList.Select(z => z.ToDate).Max();

            for (DateTime currentDate = minDate; currentDate <= maxDate; currentDate = currentDate.AddDays(1))
            {
                var currentPrice = GetLowestPrice(priceList, currentDate);
                if (currentPrice.Price < minPrice)
                {
                    selected = currentPrice;
                    minPrice = currentPrice.Price;
                    bestDate = currentDate;
                }
            }

            return selected;
        }

        static AdditionalServicePriceDto GetLowestPrice(List<AdditionalServicePriceDto> priceList, DateTime inputDate)
        {
            /*
            var matchingReservations = priceList
                .Where(prc => inputDate >= prc.FromDate && inputDate <= prc.ToDate).ToList()
                .OrderByDescending(prc => prc.Priorty).Take(3).OrderBy(prc => prc.Price);
            */
            //aldığım 3 tane, priortye göre sıralayınca gruplu olanlarda işe yarıyor, çünkü kendi arasındaki en ucuzu getiriyor ama 
            //grupsuz olanlada işe yaramaz, priortynin bir önemi kalmıyor bu sebeple alttakine geçelim

            var matchingReservations = priceList
                .Where(prc => inputDate >= prc.FromDate && inputDate <= prc.ToDate).ToList()
                .OrderByDescending(prc => prc.Priorty).ThenBy(prc => prc.Price); //.Take(3).OrderBy(prc => prc.Price);


            if (matchingReservations.Any())
            {
                return matchingReservations.First();//.Price;
            }

            return null;
        }

    }
}
