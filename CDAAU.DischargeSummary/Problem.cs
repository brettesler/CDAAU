using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;
using Oridashi.CDAAU.Core;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN")]
    public class Problem
    {
        public Problem()
        {
        }

        [RIMAttribute(Name=RIMAttributeType.EffectiveTime)]
        public DateTime? Onset { get; set; }

        [RIMAttribute(Name=RIMAttributeType.Code)]
        public ProblemDiagnosisType ProblemClass { get; set; }

        [RIMAttribute(Name=RIMAttributeType.Value)]
        public CD ProblemDescription { get; set; }
    }

    public enum ProblemDiagnosisType
    {
        /// <summary>
        /// </summary>
        [Coded(DisplayName = "Diagnosis Interpretation", CodeSystem = "2.16.840.1.113883.6.96", CodeSystemName = "SNOMED CT-AU", Code = "282291009")]
        DiagnosisInterpretation,
    }
}
