using Core.Entities;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        public string GetDisplayNameByPageName(string pageName, int languageId);
        public PageDto GetByPageName(string PageName, int languageId);
        string GetPageSlug(string pageName, int languageId);
        PageDto GetSlugByPageName(string pageName, int languageId);
        public Dictionary<string, string> GetPagesAndUrls(int languageId);
        Task<List<string>> GetPageSlugsForSitemapAsync(int languageId);
    }
}
