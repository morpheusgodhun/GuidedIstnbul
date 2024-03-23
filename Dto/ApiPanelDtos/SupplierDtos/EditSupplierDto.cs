using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SupplierDtos
{
    public class EditSupplierDto
    {
        public EditSupplierDto(string ıd, string name, int supplierType)
        {
            Id = ıd;
            Name = name;
            SupplierType = supplierType;
        }
        public EditSupplierDto()
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int SupplierType { get; set; }
    }
}
