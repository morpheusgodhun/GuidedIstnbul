using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationAdditionalServiceDto
    {
        public Guid OptionId { get; set; }
        public ICollection<ReservationAdditionalServiceInputDto>? Inputs { get; set; }

        // bu değerler sonradan eklendi hesaplamayı ve takibi kolaylaştırması için
        public decimal Price { get; set; }
        public decimal CalcPrice { get; set; }
        public Guid AdditionalServiceOptionPricesId { get; set; }
        public int ParticipantNumber { get; set; }
        public Guid AdditionalServiceId { get; set; }
        public bool IsComissionValid { get; set; }
    }

    public class ReservationAdditionalServiceInputDto
    {
        public Guid InputId { get; set; }
        public string? Value { get; set; }

    }
}
