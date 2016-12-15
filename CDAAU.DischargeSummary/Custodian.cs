using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040AssignedCustodian), ClassCode = "ASSIGNED")]
    public class Custodian
    {
        public Custodian() { }

        [RIMEntity(ElementType = typeof(POCD_MT000040CustodianOrganization), ElementName = "representedCustodianOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Id)]
        public string Id { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040CustodianOrganization), ElementName = "representedCustodianOrganization")]
        [RIMAttribute(Name = RIMAttributeType.HPIO)]
        public string HPIO { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040CustodianOrganization), ElementName = "representedCustodianOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public string OrganizationName { get; set; }
    }

}
