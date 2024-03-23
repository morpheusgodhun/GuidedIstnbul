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
    public class FaqRepository : GenericRepository<Faq>, IFaqRepository
    {

        public FaqRepository(Context context) : base(context)
        {
           
        }


    }
}
