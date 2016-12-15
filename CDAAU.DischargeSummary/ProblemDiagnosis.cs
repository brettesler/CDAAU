using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{

    [RIMAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN")]
    [RIMActCode(Code = "282291009", CodeSystem = "2.16.840.1.113883.6.96", CodeSystemName = "SNOMED CT-AU", DisplayName = "Diagnosis interpretation")]
    public class ProblemDiagnosis
    {
        public ProblemDiagnosis() {
            Id = System.Guid.NewGuid();
        }

        // fixed act.code only implemented

        //[RIMAttribute(Name = RIMAttributeType.EffectiveTime)]
        //public DateTime? Onset { get; set; }

        [RIMAttribute(Name=RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Value)]
        public codeable ProblemDiagnosisDescription { get; set; }
    }
}
