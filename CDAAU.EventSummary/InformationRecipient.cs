using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.EventSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040IntendedRecipient), ClassCode = "ASSIGNED")]
    public class InformationRecipient
    {
        public InformationRecipient() { }

        [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "2.16.840.1.113883.2.3.3.42.1.3.6")]
        public string ProviderId { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "informationRecipient")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public name Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "informationRecipient")]
        [RIMAttribute(Name = RIMAttributeType.PRN, IdRoot = "1.2.36.174030967.0.2")]
        public string ProviderNumber { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkPhone)]
        public string Phone { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkFax)]
        public string Fax { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkEmail)]
        public string Email { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Addr, AddressUse = AddressUseType.Workplace)]
        public address Address { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "informationRecipient")]
        [RIMAttribute(Name = RIMAttributeType.HPII)]
        public string HPII { get; set; }
 
        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "receivedOrganization")]
        [RIMAttribute(Name = RIMAttributeType.HPIO)]
        public string HPIO { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "receivedOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public string OrganizationName { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "receivedOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "2.16.840.1.113883.2.3.3.42.1.3.7")]
        public string OrganizationId { get; set; }
    }

}
