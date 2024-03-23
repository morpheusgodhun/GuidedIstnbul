using Core.Entities;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SendMailTemplateLanguageRepository : GenericRepository<SendMailTemplateLanguageItem>, ISendMailTemplateLanguageRepository
    {
        public SendMailTemplateLanguageRepository(Context context) : base(context)
        {
        }
    }
}
