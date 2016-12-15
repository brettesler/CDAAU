using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct( ElementType=typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.16006", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Event")]
    [RIMActTitle(Title = "Event")]
    public class EventSection
    {
        public EventSection() 
        {
            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">Details of the admission at discharge.</paragraph>";
        }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public ClinicalSynopsisSection ClinicalSynopsis { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public ProblemDiagnosesSection ProblemDiagnoses { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public ClinicalInterventionsSection ClinicalInterventions { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public DiagnosticInvestigationsSection DiagnosticInvestigations { get; set; }

    }
}
