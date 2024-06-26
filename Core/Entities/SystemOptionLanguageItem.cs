﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SystemOptionLanguageItem : BaseEntity
    {
        public string Name { get; set; }
        public int LanguageID { get; set; }

        //SystemOption

        [ForeignKey("SystemOption")]
        public Guid SystemOptionId { get; set; }
        public SystemOption SystemOption { get; set; }

    }
}
