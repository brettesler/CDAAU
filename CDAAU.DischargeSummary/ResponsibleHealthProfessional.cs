using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040AssignedEntity), ClassCode = "ASSIGNED")]
    public class ResponsibleHealthProfessional
    {
        public ResponsibleHealthProfessional() { }

        [RIMAttribute(Name = RIMAttributeType.Time)]
        public time_period ParticipationPeriod { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Code)]
        public codeable Role { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public name Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.HPII)]
        public string HPII { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.LocalProviderID)]
        public identifier LocalProviderId { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.PRN, IdRoot = "1.2.36.174030967.0.2")]
        public string ProviderNumber { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Person), ElementName = "assignedPerson")]
        [RIMAttribute(Name = RIMAttributeType.Employer)]
        public employer Employer { get; set; }
    }

}
