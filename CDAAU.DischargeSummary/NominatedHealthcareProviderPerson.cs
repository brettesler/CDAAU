using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    // note: class coded fixed to provider PROV 
    [RIMRole(ElementType = typeof(POCD_MT000040AssociatedEntity), ClassCode = "PROV")]
    public class NominatedHealthcareProviderPerson
    {
        public NominatedHealthcareProviderPerson() { }

        [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "2.16.840.1.113883.2.3.3.42.1.3.6")]
        public string Id { get; set; }
               
        [RIMAttribute(Name = RIMAttributeType.Code)]
        public codeable Role { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Addr, AddressUse = AddressUseType.Workplace)]
        public address Address { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse=TelecomUseType.WorkPhone)]
        public string Phone { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse=TelecomUseType.WorkFax)]
        public string Fax { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse=TelecomUseType.WorkEmail)]
        public string Email { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "associatedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public name Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "associatedPerson")]
        [RIMAttribute(Name = RIMAttributeType.HPII)]
        public string HPII { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "associatedPerson")]
        [RIMAttribute(Name = RIMAttributeType.LocalProviderID)]
        public identifier LocalProviderId { get; set; }


        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "associatedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Employer)]
        public employer Employer { get; set; }
    }

}
