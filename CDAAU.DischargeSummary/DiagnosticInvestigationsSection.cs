using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.20117", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Diagnostic Investigations")]
    [RIMActTitle(Title = "Diagnostic Investigations")]
    public class DiagnosticInvestigationsSection
    {
        public DiagnosticInvestigationsSection() 
        {
            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">Diagnostic investigations associated with this admission.</paragraph>";
        }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public List<PathologyTestResultSection> PathologyTestResults { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public List<ImagingExaminationResultSection> ImagingExaminationResults { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component5), TypeCode = "COMP", TargetElementName = "section")]
        public List<DiagnosticInvestigationResultSection> DiagnosticInvestigations { get; set; }


    }
}
