using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.16021", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Arranged Services")]
    [RIMActTitle(Title = "Arranged Services")]
    public class ArrangedServicesSection
    {
        public ArrangedServicesSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "act")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public List<ArrangedService> ArrangedServices { get; set; }

        [RIMAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "ACT")]
        public class ArrangedService
        {
            public ArrangedService() {
                Id = System.Guid.NewGuid();
            }

            [RIMAttribute(Name = RIMAttributeType.MoodCode)]
            public ServceBookingStatusType ServiceBookingStatus { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Id)]
            public Guid Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.EffectiveTime, EffectiveTimeUse = EffectiveTimeUseType.DateTimeHHMMZone)]
            public time_period ServiceCommencementWindow { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            public codeable ArrangedServiceDescription { get; set; }

            public string OrganisationName { get; set; }

            public string OrganisationId { get; set; }

            public enum ServceBookingStatusType
            {
                [Coded(Code="INT")]
                Intended,

                [Coded(Code="APT")]
                AppointmentMade,

                [Coded(Code="ARQ")]
                AppointmentRequested,

                [Coded(Code="PRP")]
                Proposed,

                [Coded(Code="PRMS")]
                Promised,

                [Coded(Code="RQO")]
                Requested,
            }
        }
    }
}
