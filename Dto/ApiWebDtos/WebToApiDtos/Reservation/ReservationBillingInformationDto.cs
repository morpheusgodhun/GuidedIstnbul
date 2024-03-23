using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    public class ReservationBillingInformationDto
    {
        public List<ConstantValueDto>? ConstantValues { get; set; }
        public Guid ReservationId { get; set; }
        public bool IsIndividual { get; set; }  // Else Corporate
        public string? Fullname { get; set; }  //For Just Individual
        public string? TcOrPassportNo { get; set; }  //For Just Individual
        public string? EmailIndividual { get; set; }  //For Just Individual
        public string? PhoneIndividual { get; set; }  //For Just Individual
        public string? AddressIndividual { get; set; } //For Just Individual
        public string? PhoneCorporate { get; set; } //For Just Corporate
        public string? EmailCorporate { get; set; } //For Just Corporate
        public string? AddressCorporate { get; set; } //For Just Corporate
        public string? TaxNumber { get; set; } //For Just Corporate
        public string? TaxAdministration { get; set; }//For Just Corporate
    }
}
