using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IFaqCategoryLanguageRepository : IGenericRepository<FaqCategoryLanguageItem>
    {
        string GetPageFaqCategorySlug(Guid pageId, int languageId);
    }
}
