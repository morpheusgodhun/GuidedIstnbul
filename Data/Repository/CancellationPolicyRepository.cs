using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiWebDtos.GeneralDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CancellationPolicyRepository : GenericRepository<CancellationPolicy>, ICancellationPolicyRepository
    {

        Context _context;
        DbSet<CancellationPolicy> _cancellationPolicies;
        DbSet<CancellationPolicyLanguageItem> _cancellationPolicyLanguageItems;

        public CancellationPolicyRepository(Context context) : base(context)
        {
            _context = context;
            _cancellationPolicies = context.CancellationPolicies;
            _cancellationPolicyLanguageItems = context.CancellationPolicyLanguageItems;
        }

        public void AddCancellationPolicy(AddCancellationPolicyDto addCancellationPolicyDto)
        {
            CancellationPolicy cancellationPolicy = new CancellationPolicy()
            {
                Name = addCancellationPolicyDto.Name,
                UncancellableHours = addCancellationPolicyDto.UncancellableHour
            };

            _cancellationPolicies.Add(cancellationPolicy);

            foreach (var language in LanguageList.AllLanguages)
            {
                CancellationPolicyLanguageItem cancellationPolicyLanguageItem = new CancellationPolicyLanguageItem()
                {
                    CancellationPolicyID = cancellationPolicy.Id,
                    LangaugeID = language.Id,
                    Content = addCancellationPolicyDto.Content
                };

                _cancellationPolicyLanguageItems.Add(cancellationPolicyLanguageItem);
            }
        }

        public void EditCancellationPolicy(EditCancellationPolicyDto editCancellationPolicyDto)
        {
            CancellationPolicy cancellationPolicy = _cancellationPolicies.Find(editCancellationPolicyDto.CancellationPolicyID);
            cancellationPolicy.Name = editCancellationPolicyDto.Name;
            cancellationPolicy.UncancellableHours = editCancellationPolicyDto.UncancellableHour;

            _cancellationPolicies.Update(cancellationPolicy);
        }

        public List<CancellationPolicyListDto> GetCancellationPolicyListDtos()
        {
            var cancellationPolicyListDtos = (from cancellationPolicy in _cancellationPolicies
                                              select new CancellationPolicyListDto
                                              {
                                                  CancellationPolicyID = cancellationPolicy.Id,
                                                  Name = cancellationPolicy.Name,
                                                  Status = cancellationPolicy.Status,
                                                  UnCancellableHour = cancellationPolicy.UncancellableHours,

                                              }).ToList();


            return cancellationPolicyListDtos;
        }

        public List<SelectListOptionDto> GetCancellationPolicySelectList()
        {
            List<SelectListOptionDto> cancellationPolicySelectList = (from cancellationPolicy in _cancellationPolicies
                                                                      where cancellationPolicy.IsDeleted == false
                                                                      select new SelectListOptionDto
                                                                      {
                                                                          OptionID = cancellationPolicy.Id,
                                                                          Option = cancellationPolicy.Name
                                                                      }).ToList();
            return cancellationPolicySelectList;
        }

        public EditCancellationPolicyDto GetEditCancellationPolicyDto(Guid id)
        {
            CancellationPolicy cancellationPolicy = _cancellationPolicies.Find(id);

            EditCancellationPolicyDto editCancellationPolicyDto = new EditCancellationPolicyDto()
            {
                CancellationPolicyID = id,
                Name = cancellationPolicy.Name,
                UncancellableHour = cancellationPolicy.UncancellableHours
            };

            return editCancellationPolicyDto;
        }

        public LanguageEditCancellationPolicyDto GetLanguageEditCancellationPolicyDto(Guid id, int languageId)
        {
            CancellationPolicyLanguageItem cancellationPolicyLanguageItem = _cancellationPolicyLanguageItems.Where(x => x.CancellationPolicyID == id && x.LangaugeID == languageId).Include(x => x.CancellationPolicy).FirstOrDefault();


            LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto = new LanguageEditCancellationPolicyDto()
            {
                CancellationPolicyLanguageItemID = cancellationPolicyLanguageItem.Id,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Content = cancellationPolicyLanguageItem.Content,
                Name = cancellationPolicyLanguageItem.CancellationPolicy.Name
            };

            return languageEditCancellationPolicyDto;
        }

        public void LanguageEditCancellationPolicy(LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto)
        {
            CancellationPolicyLanguageItem cancellationPolicyLanguageItem = _cancellationPolicyLanguageItems.Find(languageEditCancellationPolicyDto.CancellationPolicyLanguageItemID);

            cancellationPolicyLanguageItem.Content = languageEditCancellationPolicyDto.Content;

            _cancellationPolicyLanguageItems.Update(cancellationPolicyLanguageItem);
        }
    }
}
