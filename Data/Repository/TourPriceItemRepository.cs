﻿using Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TourPriceItemRepository : GenericRepository<TourPriceItem>
    {
        public TourPriceItemRepository(Context context) : base(context)
        {
        }
    }
}
