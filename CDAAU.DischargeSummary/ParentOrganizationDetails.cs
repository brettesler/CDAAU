using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    [RIMRole(ElementType = typeof(POCD_MT000040OrganizationPartOf), ClassCode = "PART")]
    public class ParentOrganizationDetails
    {

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "wholeOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public string Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "wholeOrganization")]
        [RIMAttribute(Name = RIMAttributeType.HPIO)]
        public string HPIO { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "wholeOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Addr, AddressUse = AddressUseType.Workplace)]
        public address Address { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "wholeOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkPhone)]
        public string WorkPhone { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "wholeOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkFax)]
        public string WorkFax { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "wholeOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkEmail)]
        public string WorkEmail { get; set; }

    }
}
