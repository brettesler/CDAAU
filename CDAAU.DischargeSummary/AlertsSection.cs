using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType=typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.20021", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Alerts")]
    [RIMActTitle(Title = "Alerts")]
    public class AlertsSection
    {
        public AlertsSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "observation")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public List<AlertDetails> Alerts { get; set; }

        [RIMAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN")]
        public class AlertDetails
        {
            public AlertDetails()
            {
                Id = System.Guid.NewGuid();
            }

            [RIMAttribute(Name = RIMAttributeType.Id)]
            public Guid Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            public codeable AlertType { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Value)]
            public codeable AlertDescription { get; set; }
        }

    }
}
