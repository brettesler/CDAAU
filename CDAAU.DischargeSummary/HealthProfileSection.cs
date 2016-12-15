using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType=typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.16011", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Health Profile")]
    [RIMActTitle(Title = "Health Profile")]
    public class HealthProfileSection
    {
        public HealthProfileSection() 
        {
            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">Health profile information for the patient.</paragraph>";
        }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public AdverseReactionsSection AdverseReactions { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public AlertsSection Alerts { get; set; }

    }
}
