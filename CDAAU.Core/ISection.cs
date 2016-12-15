using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.Core
{
    public interface ISection
    {
        [RIMAttribute(Name = RIMAttributeType.Code)]
        CD Code { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Title)]
        string Title { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        string StructuredNarrative { get; set; }
    }
}
