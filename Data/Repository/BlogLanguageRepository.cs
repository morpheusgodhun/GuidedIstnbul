using Core.Entities;
using Core.IRepository;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BlogLanguageRepository : GenericRepository<BlogLanguageItem>, IBlogLanguageRepository
    {
        public BlogLanguageRepository(Context context) : base(context)
        {
        }

        public void FixBlogContentImageWidths()
        {
            var langItems = _context.BlogLanguageItems.Where(x => x.Content1.Contains("<div id=\"attachment_")).ToList();

            foreach (var langItem in langItems)
            {
                //string pattern = @"/< div id =\"attachment_\d+\" style=\"width:\s*\d+\s*px\"[^>]*>/gm"
                string pattern = @"(<div id=""attachment_\d+"" style=""width:\s*\d+\s*px""[^>]*>)";

                Match match = Regex.Match(langItem.Content1, pattern);

                if (match.Success)
                {
                    string firstMatchedString = match.Groups[1].Value;

                    string secondPattern = @"width:\s*(\d+)\s*px";
                    Match secondMatch = Regex.Match(firstMatchedString, secondPattern);
                    if (secondMatch.Success)
                    {
                        string secondMatchString = secondMatch.Groups[0].Value;
                        string replacedStringContent = langItem.Content1.Replace(secondMatchString, string.Empty);
                        langItem.Content1 = replacedStringContent;
                        _context.BlogLanguageItems.Update(langItem);
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}
