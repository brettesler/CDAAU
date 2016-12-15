using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.EventSummary
{

    [RIMRole(ElementType = typeof(POCD_MT000040PatientRole), ClassCode = "PAT")]
    public class Patient
    {
        public Patient() { }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public identifier Id { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Patient), ElementName = "patient")]
        [RIMAttribute(Name = RIMAttributeType.IHI)]
        public string IHI { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Patient), ElementName = "patient")]
        [RIMAttribute(Name = RIMAttributeType.MRN)]
        public identifier MRN { get; set; }
        
        [RIMEntity(ElementType = typeof(POCD_MT000040Patient), ElementName = "patient")]
        [RIMAttribute(Name = RIMAttributeType.Name)]
        public name Name { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Patient), ElementName = "patient")]
        [RIMAttribute(Name = RIMAttributeType.AdministrativeGenderCode)]
        public GenderType Gender { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Patient), ElementName = "patient")]
        [RIMAttribute(Name = RIMAttributeType.BirthTime, EffectiveTimeUse=EffectiveTimeUseType.DateOnly)]
        public DateTime? DateOfBirth { get; set; }

        [RIMEntity(ElementType = typeof(POCD_MT000040Patient), ElementName = "patient")]
        [RIMAttribute(Name = RIMAttributeType.EthnicGroupCode)]
        public IndigenousStatusType IndigenousStatus { get; set; }
        
        [RIMAttribute(Name = RIMAttributeType.Addr, AddressUse = AddressUseType.Home)]
        public address Address { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.Mobile)]
        public string Mobile { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.HomePhone)]
        public string HomePhone { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.HomeFax)]
        public string HomeFax { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.HomeEmail)]
        public string HomeEmail { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkPhone)]
        public string WorkPhone { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkFax)]
        public string WorkFax { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Telecom, TelecomUse = TelecomUseType.WorkEmail)]
        public string WorkEmail { get; set; }
        
        public enum GenderType
        {
            [Coded(Code = "N", CodeSystem = "2.16.840.1.113883.13.68", CodeSystemName = "AS 5017-2006 Health Care Client Identifier Sex", DisplayName = "Not Stated/Inadequately Described")]
            NotStated,

            [Coded(Code = "I", CodeSystem = "2.16.840.1.113883.13.68", CodeSystemName = "AS 5017-2006 Health Care Client Identifier Sex", DisplayName = "Intersex or Indeterminate")]
            Indeterminate,

            [Coded(Code = "M", CodeSystem = "2.16.840.1.113883.13.68", CodeSystemName = "AS 5017-2006 Health Care Client Identifier Sex", DisplayName = "Male")]
            Male,

            [Coded(Code = "F", CodeSystem = "2.16.840.1.113883.13.68", CodeSystemName = "AS 5017-2006 Health Care Client Identifier Sex", DisplayName = "Female")]
            Female,
        }

        public enum IndigenousStatusType
        {
            [Coded(Code = "9", CodeSystem = "2.16.840.1.113883.3.879.291036", CodeSystemName = "METeOR Indigenous Status", DisplayName = "Not stated/inadequately described")]
            Default = 0,

            [Coded(Code = "1", CodeSystem = "2.16.840.1.113883.3.879.291036", CodeSystemName = "METeOR Indigenous Status", DisplayName = "Aboriginal but not Torres Strait Islander origin")]
            Aboriginal = 1,                  
                                             
            [Coded(Code = "2", CodeSystem = "2.16.840.1.113883.3.879.291036", CodeSystemName = "METeOR Indigenous Status", DisplayName = "Torres Strait Islander but not Aboriginal origin")]
            TSINotAboriginal = 2,            
                                             
            [Coded(Code = "3", CodeSystem = "2.16.840.1.113883.3.879.291036", CodeSystemName = "METeOR Indigenous Status", DisplayName = "Both Aboriginal and Torres Strait Islander origin")]
            BothAboriginalAndTSI = 3,        
                                             
            [Coded(Code = "4", CodeSystem = "2.16.840.1.113883.3.879.291036", CodeSystemName = "METeOR Indigenous Status", DisplayName = "Neither Aboriginal nor Torres Strait Islander origin")]
            NeitherAboriginalNorTSI = 4,     
                                             
            [Coded(Code = "9", CodeSystem = "2.16.840.1.113883.3.879.291036", CodeSystemName = "METeOR Indigenous Status", DisplayName = "Not stated/inadequately described")]
            NotStated = 9,
        }

    }
}
