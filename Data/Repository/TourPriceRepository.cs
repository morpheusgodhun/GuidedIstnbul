using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TourPriceRepository : GenericRepository<TourPrice>, ITourPriceRepository
    {
        Context _context;
        public TourPriceRepository(Context context) : base(context)
        {
            _context = context;
        }


        public List<TourPriceDto> GetTourPriceList(Guid tourId)
        {
            var now = DateTime.Now;
            /* var query = (from tp in _context.TourPrices
                         where tp.TourID == tourId
                            && tp.Status && !tp.IsDeleted
                            && now < tp.ToDate //&& now > tp.FromDate 
                          join tpi in _context.TourPriceItems on tp.Id equals tpi.TourPriceID into tpiGroup
                         from tpi in tpiGroup.DefaultIfEmpty()
                         orderby tp.Priority descending
                         select new TourPriceDto
                         {
                             FromDate = (DateTime)tp.FromDate,
                             ToDate = (DateTime)tp.ToDate,
                             Priorty = (int)tp.Priority,
                             Price = tpi.Price //tpi != null ? tpi.Price : (decimal?)null
                         });*/


            /*sqli
             
  select tpi.Id, tpi.TourPriceID, tp.FromDate,tp.ToDate,tp.[Priority], pp.FromPerson,pp.ToPerson,tpi.Price 
  FROM TourPriceItems tpi
  left join TourPrices tp on tpi.TourPriceID = tp.Id
  left Join PersonPolicies pp on tpi.PersonPolicyID = pp.Id
  --where tp.TourID = '27699D89-AF05-4259-996E-4CF95F95BBCA'
  --and GETDATE()< tp.ToDate 
             
             */
            var query2 = (from tpi in _context.TourPriceItems
                        join tp in _context.TourPrices on tpi.TourPriceID equals tp.Id
                        join pp in _context.PersonPolicies on tpi.PersonPolicyID equals pp.Id into ppGroup
                        from pp in ppGroup.DefaultIfEmpty()
                        where tp.TourID == tourId
                        && tp.Status && !tp.IsDeleted
                        && DateTime.Now < tp.ToDate // başlangıçla alakalı bir şartımız olmamalı arada bir gün çekilebilir. arada günleri sonraki hesaplarda yapalım //TODO: Belki buradaki dateyi = yapmalıyım
                        orderby tp.Priority descending
                        select new TourPriceDto
                        {
                            Id = tpi.Id,
                            TourPriceId = tpi.TourPriceID,
                            FromDate = (DateTime)tp.FromDate,
                            ToDate = (DateTime)tp.ToDate,
                            Priorty = (int)tp.Priority,
                            PricePerson = pp != null ? pp.FromPerson : 0,
                            PricePersonEnd = pp != null ? pp.ToPerson : 0,
                            Price = tpi.Price
                        });

            var results = query2.ToList();

            return results;
        }

        
        public TourPricesDto GetTourPriceListJson(Guid tourId)
        {
            var now = DateTime.Now;
            var query = (from tp in _context.TourPrices
                         where tp.TourID == tourId
                            && tp.Status && !tp.IsDeleted
                            && now < tp.ToDate //&& now > tp.FromDate 
                         join tpi in _context.TourPriceItems on tp.Id equals tpi.TourPriceID into tpiGroup
                         from tpi in tpiGroup.DefaultIfEmpty()
                         orderby tp.Priority descending
                         select new TourPricesDto
                         {
                             FromDate = (DateTime)tp.FromDate,
                             ToDate = (DateTime)tp.ToDate,
                             Priorty = (int)tp.Priority,
                             //Price = tpi.Price //tpi != null ? tpi.Price : (decimal?)null
                         });

            var results = query.FirstOrDefault();

            return results;
        }

        







        public List<TourPrice> GetHighPriorityTourPriceWithTourId(Guid id,int daycount) //todo: Buraya bir de datetime dönülmeli. Çünkü tour gününü rezervasyon yaparken değiştirebiliyoruz. Seçilen startDate buraya atılıp gün sayısı girilip aşağıdaki datetime.nowlar yerine gelen bu datetimelar girilecek.
        {

            //ilk yakaladığı günün fiyat gelecek
            //tarih parameter gelsin
          



            List<TourPrice> list = new();
           /* for (int i = 0; i < daycount; i++)
            {
                var tourPrice = _context.Set<TourPrice>().Where(x=>x.TourID == id).Where(x=>x.FromDate <= DateTime.Now.AddDays(i) && x.ToDate >= DateTime.Now.AddDays(i)).OrderBy(x=>x.Priority).FirstOrDefault();
                list.Add(tourPrice);
            }*/
            return list;
        }
    }
}
