using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.Core
{

    public enum ClassCodeType
    {
        /// <summary>
        /// </summary>
        [Coded(DisplayName = null, CodeSystem = null, CodeSystemName = null, Code = "OBS")]
        Observation,

        /// <summary>
        /// </summary>
        [Coded(DisplayName = null, CodeSystem = null, CodeSystemName = null, Code = "DOCSECT")]
        DocumentSection,
    }
}