using Core.Entities;
using Core.IRepository;
using Dto.ApiWebDtos.ApiToWebDtos.Service;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AdditionalServiceOptionPriceRepository : GenericRepository<AdditionalServiceOptionPrice>, IAdditionalServiceOptionPriceRepository
    {
        public AdditionalServiceOptionPriceRepository(Context context) : base(context)
        {
        }

        public List<AdditionalServicePriceDto> GetOptionPriceList(Guid optionId)
        {

            /*sqli
             
              select asop.Id, asop.AdditionalServiceOptionPriceDateID, asopd.FromDate,asopd.ToDate,asopd.[Priority], pp.FromPerson,pp.ToPerson,asop.Price 
              FROM AdditionalServiceOptionPrices asop
              left join AdditionalServiceOptionPriceDates asopd on asop.AdditionalServiceOptionPriceDateID = asopd.Id
              left Join PersonPolicies pp on asop.PersonPolicyID = pp.Id
              where asopd.AdditionalServiceOptionID = '826E28C3-475F-4267-8221-3EB1434C330F'
              and GETDATE()< asopd.ToDate 
             
             */

            var query2 = (from asop in _context.AdditionalServiceOptionPrices
                          join asopd in _context.AdditionalServiceOptionPriceDates on asop.AdditionalServiceOptionPriceDateID equals asopd.Id
                          join pp in _context.PersonPolicies on asop.PersonPolicyID equals pp.Id into ppGroup
                          from pp in ppGroup.DefaultIfEmpty()
                          where asopd.AdditionalServiceOptionID == optionId
                          && asopd.Status && !asopd.IsDeleted
                          && DateTime.Now < asopd.ToDate
                          orderby asopd.Priority descending
                          select new AdditionalServicePriceDto
                          {
                              Id = asop.Id,
                             AdditionalServiceOptionPriceDatesId =  asop.AdditionalServiceOptionPriceDateID,
                             FromDate =  asopd.FromDate,
                             ToDate =  asopd.ToDate,
                             Priorty =  asopd.Priority,
                             PricePerson =  pp != null ? pp.FromPerson : 0,
                             PricePersonEnd =  pp != null ? pp.ToPerson : 0,
                             Price =  asop.Price
                          });

            var results = query2.ToList();

            return results;

        }
    }
}
