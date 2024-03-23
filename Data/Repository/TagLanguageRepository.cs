using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TagLanguageRepository : GenericRepository<TagLanguageItem>, ITagLanguageRepository
    {
        public TagLanguageRepository(Context context) : base(context)
        {
        }

        public string GetTagDisplayName(Guid tagId, int languageId)
        {
            var a = _context.Tags.Where(x => x.Id == tagId).Include(x => x.TagLanguageItems).FirstOrDefault().TagLanguageItems.Where(x => x.LangaugeID == languageId).FirstOrDefault().DisplayName;
            return a;
        }
    }
}
