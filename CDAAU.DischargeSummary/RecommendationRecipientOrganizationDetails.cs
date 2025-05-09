using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    [RIMRole(ElementType = typeof(POCD_MT000040AssignedEntity), ClassCode = "ASSIGNED")]
    public class RecommendationRecipientOrganizationDetails
    {
        public RecommendationRecipientOrganizationDetails() { }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        public codeable Role { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "representedOrganization")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public string Department { get; set; }

        //todo: address and telecom

        [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "representedOrganization")]
        [RIMRelationship(TargetElementName = "asOrganizationPartOf")]
        public ParentOrganizationDetails ParentOrganization { get; set; }
    }
}
