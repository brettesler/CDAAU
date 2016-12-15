using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType=typeof(POCD_MT000040EncompassingEncounter), ClassCode="ENC", MoodCode="EVN")]
    public class Encounter
    {
        public Encounter() { }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public string EpisodeId { get; set; }

        [RIMAttribute(Name = RIMAttributeType.EffectiveTime, EffectiveTimeUse = EffectiveTimeUseType.DateTimeZone)]
        public time_period AdmissionPeriod { get; set; }

        [RIMAttribute(Name = RIMAttributeType.DischargeDispositionCode)]
        public DischargeDispositionType SeparationMode { get; set; }

        [RIMRelationship(ElementName = "encounterParticipant", ElementType = typeof(POCD_MT000040EncounterParticipant), TypeCode = "DIS", TargetElementName = "assignedEntity")]
        public ResponsibleHealthProfessional ResponsibleHealthProfessionalAtDischarge { get; set; }

        [RIMRelationship(ElementName = "location", ElementType = typeof(POCD_MT000040Location), TypeCode = "LOC", TargetElementName = "healthCareFacility")]
        public HealthCareFacility Facility { get; set; }

        [RIMRole(ElementType=typeof(POCD_MT000040HealthCareFacility), ClassCode="SDLOC")]
        [RIMActCode(DisplayName = "Hospital")]  // fixed in this case
        public class HealthCareFacility
        {
            public HealthCareFacility() { }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            public codeable Role { get; set; }

            [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "serviceProviderOrganization")]
            [RIMAttribute(Name = RIMAttributeType.Name)]
            public string Department { get; set; }

            [RIMEntity(ElementType = typeof(POCD_MT000040Organization), ElementName = "serviceProviderOrganization")]
            [RIMRelationship(TargetElementName="asOrganizationPartOf")]
            public ParentOrganizationDetails ParentOrganization { get; set; }
        }
    }

    public enum DischargeDispositionType
    {
        [Coded(Code = "1", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Discharge/transfer to (an)other acute hospital")]
        TransferToAcuteHospital,

        [Coded(Code = "2", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Discharge/transfer to a residential aged care service, unless this is the usual place of residence")]
        TransferToNewAgedCareResidence,

        [Coded(Code = "3", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Discharge/transfer to (an)other psychiatric hospital")]
        TransferToPsychiatricHospital,

        [Coded(Code = "4", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Discharge/transfer to other health care accommodation (includes mothercraft hospitals)")]
        TransferToHealthCareAccommodation,

        [Coded(Code = "5", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Statistical discharge - type change")]
        StatisticalDischargeTypeChange,

        [Coded(Code = "6", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Left against medical advice/discharge at own risk")]
        LeftAgainstAdvice,

        [Coded(Code = "7", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Statistical discharge from leave")]
        StatisticalDischargeFromLeave,

        [Coded(Code = "8", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Died")]
        Died,

        [Coded(Code = "9", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Other (includes discharge to usual residence, own accommodation/welfare institution (includes prisons, hostels and group homes providing primarily welfare services))")]
        DischargeToResidence,

        [Coded(Code = "9", CodeSystem = "2.16.840.1.113883.13.65", CodeSystemName = "AIHW Mode of Separation", DisplayName = "Other (includes discharge to usual residence, own accommodation/welfare institution (includes prisons, hostels and group homes providing primarily welfare services))")]
        Other,

    }

}
