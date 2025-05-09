using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.Core
{

    /// <summary>
    ///  define atribute as well known RIM or Extension type
    /// </summary>
    public enum RIMAttributeType
    {
        /// <summary>
        ///  no action
        /// </summary>
        Ignore,

        /// <summary>
        ///  act mood
        /// </summary>
        MoodCode,
        
        /// <summary>
        ///  identifier
        /// </summary>
        Id,

        /// <summary>
        ///  document set id
        /// </summary>
        SetId,

        /// <summary>
        ///  document version number
        /// </summary>
        VersionNumber,

        /// <summary>
        ///  document confidentiality
        /// </summary>
        ConfidentialityCode,

        /// <summary>
        ///  document language
        /// </summary>
        LanguageCode,

        
        /// <summary>
        ///  AU document completion extension
        /// </summary>
        CompletionCode,

        /// <summary>
        ///  act/role code describing specific type
        /// </summary>
        Code,

        /// <summary>
        ///  address 
        /// </summary>
        Addr,

        /// <summary>
        ///  phone, fax, email
        /// </summary>
        Telecom,

        /// <summary>
        ///  status of the act e.g. completed, active, aborted
        /// </summary>
        StatusCode,

        /// <summary>
        ///  relevant time for the act
        /// </summary>
        EffectiveTime,

        /// <summary>
        ///  observation value recorded e.g. measurement or result
        /// </summary>
        Value,
        
        /// <summary>
        ///  title of section or document
        /// </summary>
        Title,

        /// <summary>
        ///  description or summary text
        /// </summary>
        Text,

        /// <summary>
        ///  person or organisation name
        /// </summary>
        Name,

        /// <summary>
        ///  gender 
        /// </summary>
        AdministrativeGenderCode,

        /// <summary>
        ///  used for indigenous status
        /// </summary>
        EthnicGroupCode,

        /// <summary>
        ///  person date of birth
        /// </summary>
        BirthTime,

        /// <summary>
        ///  recorded timestamp for the participation
        /// </summary>
        Time,

        /// <summary>
        ///  recorded participation signature code
        /// </summary>
        SignatureCode,

        /// <summary>
        ///  AU extension id HPII
        /// </summary>
        HPII,

        /// <summary>
        ///  AU extension id HPIO
        /// </summary>
        HPIO,

        /// <summary>
        ///  AU extension id IHI
        /// </summary>
        IHI,

        /// <summary>
        ///  AU extension id MRN
        /// </summary>
        MRN,

        /// <summary>
        ///  AU extension id Provider Number
        /// </summary>
        PRN,

        /// <summary>
        ///  AU extension id Local Provider reference
        /// </summary>
        LocalProviderID,

        /// <summary>
        ///  AU extension Employer detail
        /// </summary>
        Employer,

        /// <summary>
        /// discharge disposition
        /// </summary>
        DischargeDispositionCode,

    }

    /// <summary>
    ///  choice of date/time representation 
    /// </summary>
    public enum EffectiveTimeUseType
    {
        DateTimeZone,       // full date time + time zone
        DateTimeHHMMZone,   // date time HHmm + time zone
        DateTimeLocal,      // full date time no time zone
        DateOnly,
        MonthDateOnly,
    }

    public enum TelecomUseType
    {
        Undefined,
        HomePhone,
        WorkPhone,
        HomeFax,
        WorkFax,
        HomeEmail,
        WorkEmail,
        Mobile
    }


    public enum AddressUseType
    {
        Undefined,
        Workplace,
        Postal,
        Temporary,
        Home,
    }
        
    /// <summary>
    ///  assign RIM representation for an object (data) 
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class RIMAttributeAttribute : Attribute
    {
        /// <summary>
        /// choice of well known attribute names with known representations
        /// </summary>
        virtual public RIMAttributeType Name { get; set; }

        /// <summary>
        /// define date/time usage for effectivetime value, such as date only or full date
        /// </summary>
        public EffectiveTimeUseType EffectiveTimeUse { get; set; }

        /// <summary>
        /// set a fixed id root for id RIM attribute
        /// </summary>
        public string IdRoot { get; set; }

        /// <summary>
        /// set an id authority name
        /// </summary>
        public string IdAuthorityName { get; set; }

        /// <summary>
        ///  set a fixed telecom use
        /// </summary>
        public TelecomUseType TelecomUse { get; set; }

        /// <summary>
        ///  set a fixed address use
        /// </summary>
        public AddressUseType AddressUse { get; set; }

    }


    

}