using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.Core
{
    public interface IClinicalDocument
    {
        [RIMAttribute(Name = RIMAttributeType.Id)]
        Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.SetId)]
        Guid SetId { get; set; }

        [RIMAttribute(Name = RIMAttributeType.VersionNumber)]
        int VersionNumber { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        CD Code { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Title)]
        string Title { get; set; }

        [RIMAttribute(Name = RIMAttributeType.EffectiveTime)]
        DateTime CreationTime { get; set; }
   
    }
}
