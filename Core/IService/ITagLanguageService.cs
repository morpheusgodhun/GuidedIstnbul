using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ITagLanguageService : IGenericService<TagLanguageItem>
    {
        string GetTagDisplayName(Guid tagId, int languageId);
    }
}
