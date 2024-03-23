using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IUserExtensionMailRepository : IGenericRepository<UserExtensionMail>
    {
        UserExtensionMail GetByUrlCode(string urlCode);
        List<UserExtensionMail> GetUserMails(string userId);
    }
}
