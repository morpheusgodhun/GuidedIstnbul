using Core.Entities;
using Core.IRepository;
using Core.StaticClass;
using Dto.ApiPanelDtos.ConstantValueDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class ConstantValueRepository : GenericRepository<ConstantValue>, IConstantValueRepository
    {
        Context _context;
        DbSet<ConstantValue> _constantValues;
        DbSet<ConstantValueLanguageItem> _constantValueLanguageItems;
        DbSet<Page> _pages;

        public ConstantValueRepository(Context context) : base(context)
        {
            _context = context;
            _constantValues = _context.ConstantValues;
            _constantValueLanguageItems = _context.ConstantValueLanguageItems;
            _pages = _context.Pages;
        }

        public void AddConstantValue(AddConstantValueDto addConstantValueDto)
        {
            ConstantValue constantValue = new ConstantValue()
            {
                Key = addConstantValueDto.Key,
                PageID = addConstantValueDto.PageID
            };
            _constantValues.Add(constantValue);

            foreach (var language in LanguageList.AllLanguages)
            {
                ConstantValueLanguageItem constantValueLanguageItem = new ConstantValueLanguageItem()
                {
                    ConstantValueID = constantValue.Id,
                    Value = addConstantValueDto.Value,
                    LanguageID = language.Id

                };

                _constantValueLanguageItems.Add(constantValueLanguageItem);
            }
        }

        public async Task<List<ConstantValueDto>> GetConstantValueByPageNameAsync(string pageName, int languageId)
        {
            var constantValueDto = await Task.Run(() =>
            {
                return (from item in _constantValues.ToList()
                        join page in _pages.Where(x => x.PageName == pageName).ToList()
                        on item.PageID equals page.Id
                        join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                        on item.Id equals languageItem.ConstantValueID
                        select new ConstantValueDto()
                        {
                            Key = item.Key,
                            Value = languageItem.Value
                        }).ToList();
                //if (pageName == "Footer")
                //{
                //    return (from item in _constantValues.ToList()
                //            join page in _pages.Where(x => x.PageName == pageName || x.PageName == "Apply As Agent").ToList()
                //            on item.PageID equals page.Id
                //            join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                //            on item.Id equals languageItem.ConstantValueID
                //            select new ConstantValueDto()
                //            {
                //                Key = item.Key,
                //                Value = languageItem.Value
                //            }).ToList();
                //}
                //else //if (pageName == "Login")
                //{
                //    return (from item in _constantValues.ToList()
                //            join page in _pages.Where(x => x.PageName == pageName || x.PageName == "Register" || x.PageName== "Agent Reservations").ToList()
                //            on item.PageID equals page.Id
                //            join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                //            on item.Id equals languageItem.ConstantValueID
                //            select new ConstantValueDto()
                //            {
                //                Key = item.Key,
                //                Value = languageItem.Value
                //            }).ToList();
                //}
                //else    return;

            });

            return constantValueDto;
        }

        public async Task<List<ConstantValueDto>> GetAllConstantValue(int languageId)
        {
            var constantValueDto = await Task.Run(async () =>
            {
                return (from item in await _constantValues.ToListAsync()
                        join page in await _pages.ToListAsync()
                        on item.PageID equals page.Id
                        join languageItem in _constantValueLanguageItems.Where(x => x.LanguageID == languageId)
                        on item.Id equals languageItem.ConstantValueID
                        select new ConstantValueDto()
                        {
                            Key = item.Key,
                            Value = languageItem.Value
                        }).ToList();
            });

            return constantValueDto;
        }


        public List<ConstantValueListDto> GetConstantValueListDtos()
        {
            var constantValueListDtos = (from constantValue in _constantValues.ToList()
                                         where constantValue.IsDeleted == false
                                         join constantValueLanguageItem in _constantValueLanguageItems
                                         on constantValue.Id equals constantValueLanguageItem.ConstantValueID
                                         where constantValueLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                         select new ConstantValueListDto
                                         {
                                             ConstantValueID = constantValue.Id,
                                             Key = constantValue.Key,
                                             Value = constantValueLanguageItem.Value
                                         }).ToList();
            return constantValueListDtos;
        }

        public LanguageEditConstantValueDto GetLanguageEditConstantValueDto(Guid id, int languageId)
        {
            var languageEditDto = (from constantValue in _constantValues
                                   where constantValue.Id == id
                                   join constantValueLanguageItem in _constantValueLanguageItems
                                   on constantValue.Id equals constantValueLanguageItem.ConstantValueID
                                   where constantValueLanguageItem.LanguageID == languageId
                                   select new LanguageEditConstantValueDto
                                   {
                                       ConstantValueLanguageItemID = constantValueLanguageItem.Id,
                                       Key = constantValue.Key,
                                       Value = constantValueLanguageItem.Value
                                   }).FirstOrDefault();
            return languageEditDto;
        }

        public void LanguageEditConstantValue(LanguageEditConstantValueDto languageEditConstantValueDto)
        {
            ConstantValueLanguageItem constantValueLanguageItem = _constantValueLanguageItems.Find(languageEditConstantValueDto.ConstantValueLanguageItemID);

            constantValueLanguageItem.Value = languageEditConstantValueDto.Value;
            _constantValueLanguageItems.Update(constantValueLanguageItem);
        }
    }
}
