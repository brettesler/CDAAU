using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.16146.4.1.1", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Current Medications on Discharge")]
    [RIMActTitle(Title = "Current Medications on Discharge")]
    public class CurrentMedicationsSection
    {
        public CurrentMedicationsSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "observation")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN", AutoId = true)]
        [RIMContainerActCode(Code = "103.16302.4.3.2", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Global Statement")]
        [NeHTALevel( Level=NeHTALevelAttribute.NeHTALevels.Level3a) ]
        public GlobalStatementType? ExclusionStatement { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "substanceAdministration")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public List<TherapeuticGood> TherapeuticGoods { get; set; }
    }
}
