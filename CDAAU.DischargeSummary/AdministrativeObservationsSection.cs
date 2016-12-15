using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "102.16080", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Administrative Observations")]
    [RIMActTitle(Title = "Administrative Observations")]
    public class AdministrativeObservationsSection
    {
        public AdministrativeObservationsSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "observation")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN", AutoId = true)]
        [RIMContainerActCode(Code = "103.20109", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Age")]
        public physical_quantity Age { get; set; }


        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "observation")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN", AutoId = true)]
        [RIMContainerActCode(Code = "103.16028", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Specialty")]
        public codeable Speciality { get; set; }


        [RIMRelationship(ElementName = "coverage2", ElementType = typeof(Coverage2), TypeCode = "COVBY", TargetElementName = "entitlement")]
        public MedicareNumber MedicareEntitlement { get; set; }

        [RIMRelationship(ElementName = "coverage2", ElementType = typeof(Coverage2), TypeCode = "COVBY", TargetElementName = "entitlement")]
        public DVAGoldNumber DVAGoldEntitlement { get; set; }

        [RIMRelationship(ElementName = "coverage2", ElementType = typeof(Coverage2), TypeCode = "COVBY", TargetElementName = "entitlement")]
        public DVAWhiteNumber DVAWhiteEntitlement { get; set; }

        [RIMRelationship(ElementName = "coverage2", ElementType = typeof(Coverage2), TypeCode = "COVBY", TargetElementName = "entitlement")]
        public DVAOrangeNumber DVAOrangeEntitlement { get; set; }

        [RIMRelationship(ElementName = "coverage2", ElementType = typeof(Coverage2), TypeCode = "COVBY", TargetElementName = "entitlement")]
        public PensionNumber PensionNumberEntitlement { get; set; }

        //[RIMRelationship(ElementName = "coverage2", ElementType = typeof(Coverage2), TypeCode = "DRIV", TargetElementName = "entitlement")]
        //public List<EntitlementDetails> Entitlements { get; set; }
        
        [RIMAct(ElementType = typeof(Entitlement), ClassCode = "COV", MoodCode = "EVN")]
        public class EntitlementDetails
        {
            public EntitlementDetails() { }

            [RIMAttribute(Name = RIMAttributeType.Id)]
            virtual public string Id { get; set; }

            [RIMAttribute(Name=RIMAttributeType.Code)]
            virtual public EntitlementType EntitlementTypeValue { get; set; }

            [RIMAttribute(Name = RIMAttributeType.EffectiveTime)]
            virtual public time_period EntitlementValidityDuration { get; set; }

            [RIMRelationship(ElementName = "participant", ElementType = typeof(Participant), TypeCode = "BEN", TargetElementName = "participantRole")]
            public EntitlementSubjectDetails EntitlementSubject { get; set; }

            [RIMRole(ElementType=typeof(ParticipantRole), ClassCode="PAT")]
            public class EntitlementSubjectDetails
            {
                [RIMAttribute(Name = RIMAttributeType.Id)]
                public identifier PatientId { get; set; }
            }
        }

        [RIMAct(ElementType = typeof(Entitlement), ClassCode = "COV", MoodCode = "EVN")]
        public class MedicareNumber : EntitlementDetails
        {
            [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "1.2.36.1.5001.1.0.7")]
            override public string Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.EffectiveTime, EffectiveTimeUse=EffectiveTimeUseType.MonthDateOnly)]
            override public time_period EntitlementValidityDuration { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            override public EntitlementType EntitlementTypeValue { get { return EntitlementType.MedicareBenefits; } set { throw new NotImplementedException("fixed value do not set"); } }
        }

        [RIMAct(ElementType = typeof(Entitlement), ClassCode = "COV", MoodCode = "EVN")]
        public class DVAGoldNumber : EntitlementDetails
        {
            [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "1.2.36.1.5001.1.0.7")]
            override public string Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            override public EntitlementType EntitlementTypeValue { get { return EntitlementType.RepatriationHealthGoldBenefits; } set { throw new NotImplementedException("fixed value do not set"); } }
        }

        [RIMAct(ElementType = typeof(Entitlement), ClassCode = "COV", MoodCode = "EVN")]
        public class DVAWhiteNumber : EntitlementDetails
        {
            [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "1.2.36.1.5001.1.0.7")]
            override public string Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            override public EntitlementType EntitlementTypeValue { get { return EntitlementType.RepatriationHealthWhiteBenefits; } set { throw new NotImplementedException("fixed value do not set"); } }
        }

        [RIMAct(ElementType = typeof(Entitlement), ClassCode = "COV", MoodCode = "EVN")]
        public class DVAOrangeNumber : EntitlementDetails
        {
            [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "1.2.36.1.5001.1.0.7")]
            override public string Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            override public EntitlementType EntitlementTypeValue { get { return EntitlementType.RepatriationHealthOrangeBenefits; } set { throw new NotImplementedException("fixed value do not set"); } }
        }

        [RIMAct(ElementType = typeof(Entitlement), ClassCode = "COV", MoodCode = "EVN")]
        public class PensionNumber : EntitlementDetails
        {
            [RIMAttribute(Name = RIMAttributeType.Id, IdRoot = "1.2.36.1.5001.1.0.7")]
            override public string Id { get; set; }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            override public EntitlementType EntitlementTypeValue { get { return EntitlementType.PensionerConcession; } set { throw new NotImplementedException("fixed value do not set"); } }
        }



        public enum EntitlementType
        {
            [Coded( DisplayName="Medicare Benefits", Code="1", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            MedicareBenefits,

            [Coded( DisplayName="Pensioner Concession", Code="2", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            PensionerConcession,

            [Coded( DisplayName="Commonwealth Seniors Health Concession", Code="3", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            CommonwealthSeniorsHealthConcession,

            [Coded( DisplayName="Health Care Concession", Code="4", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            HealthCareConcession,

            [Coded( DisplayName="Repatriation Health Gold Benefits", Code="5", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            RepatriationHealthGoldBenefits,

            [Coded( DisplayName="Repatriation Health White Benefits", Code="6", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            RepatriationHealthWhiteBenefits,

            [Coded( DisplayName="Repatriation Health Orange Benefits", Code="7", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            RepatriationHealthOrangeBenefits,

            [Coded( DisplayName="Safety Net Concession", Code="8", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            SafetyNetConcession,

            [Coded( DisplayName="Safety Net Entitlement", Code="9", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            SafetyNetEntitlement,

            [Coded( DisplayName="Medicare Prescriber Number", Code="10", CodeSystemName="NCTIS Entitlement Type Values", CodeSystem="1.2.36.1.2001.1001.101.104.16047")]
            MedicarePrescriberNumber,

            [Coded(DisplayName = "Medicare Pharmacy Approval Number", Code = "11", CodeSystemName = "NCTIS Entitlement Type Values", CodeSystem = "1.2.36.1.2001.1001.101.104.16047")]  
            MedicarePharmacyApprovalNumber,
        }
    }
}
