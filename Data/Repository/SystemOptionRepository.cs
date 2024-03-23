using Core.Entities;
using Core.IRepository;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class SystemOptionRepository : GenericRepository<SystemOption>, ISystemOptionRepository
    {
        public SystemOptionRepository(Context context) : base(context)
        {
        }


        public List<SelectListOptionDto> GetSystemOptionByCategoryId(int systemOptionCategoryId, int languageId)
        {
            List<SelectListOptionDto> SelectList =
            (from systemOption in _context.SystemOptions.ToList()
             where systemOption.SystemOptionCategoryID == systemOptionCategoryId && !systemOption.IsDeleted && systemOption.Status
             join languageItem in _context.SystemOptionLanguageItems.ToList()
                 on systemOption.Id equals languageItem.SystemOptionId
             where languageItem.LanguageID == languageId
             orderby systemOption.Order ascending
             select new SelectListOptionDto()
             {
                 OptionID = systemOption.Id,
                 Option = languageItem.Name
             }).ToList();

            return SelectList;
        }
        public SelectListOptionDto GetCityById(int systemOptionCategoryId, int languageId, Guid cityId)
        {
            SelectListOptionDto SelectList =
            (from systemOption in _context.SystemOptions.ToList()
             where systemOption.SystemOptionCategoryID == systemOptionCategoryId && !systemOption.IsDeleted && systemOption.Status
             join languageItem in _context.SystemOptionLanguageItems.ToList()
                 on systemOption.Id equals languageItem.SystemOptionId
             where languageItem.LanguageID == languageId && systemOption.Id == cityId
             select new SelectListOptionDto()
             {
                 OptionID = systemOption.Id,
                 Option = languageItem.Name
             }).FirstOrDefault();

            return SelectList;
        }

        public SystemOption GetSystemOptionById(Guid systemOptionId, int languageId)
        {
            var data = _context.SystemOptions.Include(x => x.SystemOptionLanguageItems.Where(x => x.LanguageID == languageId)).FirstOrDefault(x => x.Id == systemOptionId);
            return data;
        }
    }
}


