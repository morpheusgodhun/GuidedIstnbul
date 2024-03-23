using Core.Entities;
using Core.IRepository;
using Core.StaticClass;
using Dto.ApiPanelDtos.SendMailDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SendMailRepository : GenericRepository<SendMail>, ISendMailRepository
    {
        public SendMailRepository(Context context) : base(context)
        {
        }

        //public void WritePlaceholdersToFile()
        //{
        //    var languageItems = _context.SendMailTemplateLanguageItems.Include(x => x.SendMailTemplate).ToList();

        //    List<string> placeholderList = new();
        //    foreach (SendMailTemplateLanguageItem item in languageItems)
        //    {
        //        List<string> placeholders = MailPlaceholderUtil.ExtractPlaceholders(item.Content);
        //        placeholders.ForEach(x =>
        //        {
        //            if (!placeholderList.Contains(x))
        //                placeholderList.Add(x);
        //        });
        //    }
        //    string filePath = "C:\\Users\\mehme\\OneDrive\\Masaüstü\\mail\\mailPlaceholders.txt";

        //    using StreamWriter writer = new(filePath);
        //    foreach (var placeholder in placeholderList.OrderBy(x => x))
        //        writer.WriteLine("[[" + placeholder + "]]");
        //}

    }
}
