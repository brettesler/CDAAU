using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.EventSummary
{
    [RIMAct( ElementType=typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.16672", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Event Details")]
    [RIMActTitle(Title = "Event Details")]
    public class EventDetailsSection
    {
        public EventDetailsSection() 
        {
        }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "act")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "ACT", MoodCode = "EVN", AutoId = true)]
        [RIMContainerActCode(Code = "102.15513", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Clinical Synopsis")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public string ClinicalSynopsisDescription { get; set; }


    }
}
