using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using webapp.SharedServies;

namespace webapp.Domain
{
    [ServiceLayerObjectName("BusinessPartners")]
    public class BusinessPartner
    {
        [OrderBy,SelectColumn,SAPPrimaryKey]
        public string? CardCode { get; set; }
        [SelectColumn]
        public int Series { get; set; }
        [SelectColumn]
        public string? CardType { get; set; }
        [SelectColumn]
        public string? CardName { get; set; }
        [SelectColumn]
        public string? GroupCode { get; set; }
        [SelectColumn]
        public string? CardForeignName { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public decimal OpenDeliveryNotesBalance { get; set; }
        public decimal OpenOrdersBalance { get; set; }
        public string? Currency { get; set; }
        public string? FederalTaxID { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? MailAddress { get; set; }
        public string? Website { get; set; }
        public string? Fax { get; set; }
        public string? Cellular { get; set; }
        public int SalesPersonCode { get; set; }
        public List<BPAddresses>? BPAddresses { get; set; }
        public List<BPPaymentMethods>? BPPaymentMethods { get; set; }
        public int PriceListNum { get; set; }
        public string? FatherCard { get; set; }
    }
    public class BPPaymentMethods
    {
        public int? DetailID { get; set; }
        public string? BPCode { get; set; }
        public string? DBName { get; set; }
        public string? PaymentMethodCode { get; set; }
    }
    public class BPAddresses
    {
        public int? DetailID { get; set; }
        public string? BPCode { get; set; }
        public string? DBName { get; set; }
        public string? AddressName { get; set; }
        public string? Street { get; set; }
        public string? Block { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? BuildingFloorRoom { get; set; }
        public string? AddressType { get; set; }
        public string? AddressName2 { get; set; }
        public string? AddressName3 { get; set; }
        public string? TypeOfAddress { get; set; }
        public string? StreetNo { get; set; }
        public string? GlobalLocationNumber { get; set; }
        public string? Nationality { get; set; }
        public string? TaxOffice { get; set; }
        public string? GSTIN { get; set; }
        public string? GstType { get; set; }
    }
}
