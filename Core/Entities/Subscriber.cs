﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
