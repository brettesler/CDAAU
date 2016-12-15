using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMRole(ElementType = typeof(POCD_MT000040AssignedEntity), ClassCode = "ASSIGNED")]
    public class RecommendationRecipientPersonDetails
    {
        public RecommendationRecipientPersonDetails() { }

        [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "2.16.840.1.113883.2.3.3.42.1.3.6")]
        public string Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        public codeable Role { get; set; }

        //todo: address and telecom

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public name Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.HPII)]
        public string HPII { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Employer)]
        public employer Employer { get; set; }
    }
}
