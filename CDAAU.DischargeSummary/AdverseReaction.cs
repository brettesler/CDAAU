using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{

    [RIMAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN")]
    [RIMActCode(Code = "102.15517", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Adverse Reaction")]
    public class AdverseReaction
    {
        public AdverseReaction() 
        {
            Id = System.Guid.NewGuid();
        }

        [RIMAttribute(Name=RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Value)]
        public codeable AdverseReactionType { get; set; }

        [RIMRelationship(ElementName = "participant", ElementType = typeof(POCD_MT000040Participant2), TypeCode = "CAGNT", TargetElementName = "participantRole")]
        public Agent AgentDescription { get; set; }

        [RIMRole(ElementType = typeof(POCD_MT000040ParticipantRole), ClassCode = "ROL")]
        public class Agent
        {
            public Agent() { }

            [RIMEntity(ElementType = typeof(POCD_MT000040PlayingEntity), ElementName = "playingEntity")]
            [RIMAttribute(Name = RIMAttributeType.Code)]
            public codeable AgentDescription { get; set; }
        }
    }
}
