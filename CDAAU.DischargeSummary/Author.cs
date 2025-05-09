using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040AssignedAuthor), ClassCode = "ASSIGNED")]
    public class Author
    {
        public Author() { }

        [RIMAttribute(Name = RIMAttributeType.Time)]
        public DateTime CreationTime { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public string Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        public codeable Role { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public name Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.HPII)]
        public string HPII { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.LocalProviderID)]
        public identifier LocalProviderId { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Addr, AddressUse = AddressUseType.Workplace)]
        public address Address { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkPhone)]
        public string WorkPhone { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkFax)]
        public string WorkFax { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkEmail)]
        public string WorkEmail { get; set; }
        
        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Employer)]
        public employer Employer { get; set; }
    }

}
