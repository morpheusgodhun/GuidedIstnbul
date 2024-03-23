using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CertificateManagementDtos
{
    public class CertificateListDto
    {
        public Guid CertificateID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
