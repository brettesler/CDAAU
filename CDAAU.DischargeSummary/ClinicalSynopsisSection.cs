using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "102.15513.4.1.1", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Clinical Synopsis")]
    [RIMActTitle(Title = "Clinical Synopsis")]
    public class ClinicalSynopsisSection
    {
        public ClinicalSynopsisSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }
    
        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "act")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "ACT", MoodCode = "EVN", AutoId = true)]
        [RIMContainerActCode(Code = "103.15582", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Clinical Synopsis Description")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public string ClinicalSynopsisDescription { get; set; }
    }
}
