﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Certificate : BaseEntity
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; }
    }
}
