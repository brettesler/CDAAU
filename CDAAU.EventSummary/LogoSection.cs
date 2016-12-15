using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.EventSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    public class LogoSection
    {
        public LogoSection() { }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "observationMedia")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040ObservationMedia), ClassCode = "OBS", MoodCode = "EVN", AutoId = true, ID = "LOGO")]
        public string LogoFile { get; set; }
    }
}
