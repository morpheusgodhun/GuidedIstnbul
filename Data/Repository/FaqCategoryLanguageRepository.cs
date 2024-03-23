using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class FaqCategoryLanguageRepository : GenericRepository<FaqCategoryLanguageItem>, IFaqCategoryLanguageRepository
    {
        public FaqCategoryLanguageRepository(Context context) : base(context)
        {
        }

        public string GetPageFaqCategorySlug(Guid pageId, int languageId)
        {
            var data = (from faqCategory in _context.FaqCategories
                        join page in _context.Pages on faqCategory.PageId equals page.Id
                        join pageLanguageItem in _context.PageLanguageItems on page.Id equals pageLanguageItem.PageID
                        where pageLanguageItem.LanguageId == languageId && page.Id == pageId
                        select pageLanguageItem.Slug).FirstOrDefault();

            return data;
            //CA9441F4-4480-4CD6-9864-D0C4EF1918FC
        }
    }
}
