using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040AssociatedEntity), ClassCode = "SDLOC")]
    public class LocationOfDischarge
    {
        public LocationOfDischarge() { }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        public string WardDeparment { get; set; }
    }

}
