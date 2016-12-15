using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "INFRM", MoodCode = "PRP")]
    [RIMActCode(Code = "102.20016.4.1.1", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Recommendations Provided")]
    public class RecommendationDetails
    {
        public RecommendationDetails() {
            Id = System.Guid.NewGuid();
        }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string RecommendationNote { get; set; }

        [RIMRelationship(ElementName = "performer", ElementType = typeof(POCD_MT000040Performer2), TypeCode = "PRF", TargetElementName = "assignedEntity")]
        public RecommendationRecipientPersonDetails RecommendationRecipientPerson { get; set; }

        [RIMRelationship(ElementName = "performer", ElementType = typeof(POCD_MT000040Performer2), TypeCode = "PRF", TargetElementName = "assignedEntity")]
        public RecommendationRecipientOrganizationDetails RecommendationRecipientOrganization { get; set; }
    }
}
