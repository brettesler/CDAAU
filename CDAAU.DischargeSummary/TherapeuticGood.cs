using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{

    [RIMAct(ElementType = typeof(POCD_MT000040SubstanceAdministration), ClassCode = "SBADM", MoodCode = "EVN")]
    public class TherapeuticGood
    {
        public TherapeuticGood() { }

        [RIMAttribute(Name=RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.EffectiveTime, EffectiveTimeUse = EffectiveTimeUseType.DateOnly)]
        public time_period MedicationDuration { get; set; }

        [RIMAttribute(Name = RIMAttributeType.StatusCode)]
        public ActStatusType Status { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string DoseInstruction { get; set; }

        [RIMRelationship(ElementName = "consumable", ElementType = typeof(POCD_MT000040Consumable), TypeCode = "CSM", TargetElementName = "manufacturedProduct")]
        public ManufacturedProduct TherapeuticGoodIdentification { get; set; }
        
        [RIMRole(ElementType = typeof(POCD_MT000040ManufacturedProduct), ClassCode = "MANU")]
        public class ManufacturedProduct
        {
            public ManufacturedProduct() { }

            [RIMEntity(ElementType = typeof(POCD_MT000040Material), ElementName = "manufacturedMaterial")]
            [RIMAttribute(Name = RIMAttributeType.Code)]
            public codeable TherapeuticGoodIdentification { get; set; }
        }

        [RIMRelationship(ElementName = "entryRelationship", ElementType = typeof(POCD_MT000040EntryRelationship), TypeCode = "RSON", TargetElementName = "act")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "INFRM", MoodCode = "RQO", AutoId=true)]
        [RIMContainerActCode(Code = "103.10141", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Reason for Therapeutic Good")]
        public string ReasonForTherapeuticGood { get; set; }

        [RIMRelationship(ElementName = "entryRelationship", ElementType = typeof(POCD_MT000040EntryRelationship), TypeCode = "COMP", TargetElementName = "act")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "INFRM", MoodCode = "EVN", AutoId=true)]
        [RIMContainerActCode(Code = "103.16044", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Additional Comments")]
        public string AdditionalComments { get; set; }
        
        // item status

        [RIMRelationship(ElementName = "entryRelationship", ElementType = typeof(POCD_MT000040EntryRelationship), TypeCode = "RSON", TargetElementName = "observation")]
        public ChangeDetails ChangeDetail { get; set; }

        [RIMAct(ElementType = typeof(POCD_MT000040Observation), ClassCode = "OBS", MoodCode = "EVN", AutoId=true)]
        public class ChangeDetails
        {
            public ChangeDetails() { }

            [RIMAttribute(Name = RIMAttributeType.Code)]
            public codeable ChangesMade { get; set; }

            [RIMRelationship(ElementName = "entryRelationship", ElementType = typeof(POCD_MT000040EntryRelationship), TypeCode = "RSON", TargetElementName = "act")]
            [RIMContainerAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "INFRM", MoodCode = "EVN", AutoId = true)]
            [RIMContainerActCode(Code = "103.10177", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Reason for Change")]
            public string ReasonForChange { get; set; }
        }
    }
}
