using Core.Entities;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IPageService : IGenericService<Page>
    {
        PageDto GetPageById(string id, int languageId);
        string GetDisplayNameByPageName(string pageName, int languageId);
        PageDto GetByPageName(string PageName, int languageId);
        PageDto GetSlugByPageName(string pageName, int languageId);
        string GetPageSlug(string pageName, int languageId);
        Dictionary<string, string> GetPagesAndUrls(int languageId); //GetDisplayNameByPageName üzerine türedi
    }
}
