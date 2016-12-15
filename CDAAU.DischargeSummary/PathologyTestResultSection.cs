using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "102.16144", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Pathology Test Result")]
    [RIMActTitle(Title = "Pathology Test Result")]
    public class PathologyTestResultSection
    {
        public PathologyTestResultSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }


    }
}
