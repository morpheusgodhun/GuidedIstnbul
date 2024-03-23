using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ServiceLanguageItem : BaseEntity
    {
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public int LanguageID { get; set; }

        //Service

        [ForeignKey("Service")]
        public Guid ServiceID { get; set; }
        public Service Service { get; set; }


    }
}
