using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.EventSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040AssignedEntity), ClassCode = "ASSIGNED")]
    public class LegalAuthenticator
    {
        public LegalAuthenticator() {
        }

        [RIMAttribute(Name = RIMAttributeType.Time)]
        public DateTime AttestationTime { get; set; }


        [RIMAttribute(Name = RIMAttributeType.SignatureCode)]
        public codeable SignatureCode { get { return new codeable() { Code = "S" }; } }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public string Id { get; set; }

    }

}
