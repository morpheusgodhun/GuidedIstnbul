using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TagLanguageItem : BaseEntity
    {
        public string DisplayName { get; set; }
        public int LangaugeID { get; set; }

        public string? Slug { get; set; } //şimdilik bir şey patlamasın diye nullable yapıyorum
        //Tag

        [ForeignKey("Tag")]
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }

    }
}
