using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SupplierDtos
{
    public class SupplierListDto
    {
        public SupplierListDto(string id, string name, string supplierType, bool status)
        {
            Id = id;
            SupplierType = supplierType;
            Name = name;
            Status = status;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string SupplierType { get; set; }
        public bool Status { get; set; }
    }
}
