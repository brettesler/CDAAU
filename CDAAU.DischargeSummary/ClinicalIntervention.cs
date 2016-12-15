using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{

    [RIMAct(ElementType = typeof(POCD_MT000040Procedure), ClassCode = "PROC", MoodCode = "EVN")]
    public class ClinicalIntervention
    {
        public ClinicalIntervention() 
        {
            Id = System.Guid.NewGuid();
        }

        [RIMAttribute(Name=RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        public codeable ClinicalInterventionDescription { get; set; }
    }
}
