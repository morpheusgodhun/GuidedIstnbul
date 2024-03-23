using Core.Entities;
using Core.IRepository;
using Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserExtensionMailRepository : GenericRepository<UserExtensionMail>, IUserExtensionMailRepository
    {
        public UserExtensionMailRepository(Context context) : base(context)
        {
        }

        public UserExtensionMail GetByUrlCode(string urlCode)
        {
            return _context.UserExtensionsMails.Where(x => x.UrlCode == Guid.Parse(urlCode) && !x.IsDeleted).FirstOrDefault();
        }

        public List<UserExtensionMail> GetUserMails(string userId)
        {
            return _context.UserExtensionsMails.Where(m => m.UserId == Guid.Parse(userId) && !m.IsDeleted).ToList();
        }
    }
}
