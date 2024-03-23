using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IFaqCategoryService : IGenericService<FaqCategory>
    {
        string GetPageFaqCategorySlug(string pageId, int languageId);
    }
}
