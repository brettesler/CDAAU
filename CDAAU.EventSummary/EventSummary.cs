using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;
using TeamUnicorn.CDAAU.Core;


namespace TeamUnicorn.CDAAU.EventSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040ClinicalDocument), ClassCode = "DOCCLIN", MoodCode = "EVN")]
    [RIMTypeId(Root = "2.16.840.1.113883.1.3", Extension = "POCD_HD000040")]
    [RIMTemplateId(Root = "1.2.36.1.2001.1001.101.100.1002.136", Extension = "1.3")]
    [RIMTemplateId(Root = "1.2.36.1.2001.1001.100.149", Extension = "1.0")]
    [RIMActCode(Code = "34133-9", CodeSystem = "2.16.840.1.113883.6.1", CodeSystemName = "LOINC", DisplayName = "Summary of episode note")]
    public class EventSummary
    {

        public EventSummary()
        {
            Id = Guid.NewGuid();
            SetId = Guid.NewGuid();
            VersionNumber = 1;
        }

        [RIMAttribute(Name = RIMAttributeType.Title)]
        public string Title { get; set; }

        [RIMAttribute(Name = RIMAttributeType.Id)]
        public Guid Id { get; set; }

        [RIMAttribute(Name = RIMAttributeType.SetId)]
        public Guid SetId { get; set; }

        [RIMAttribute(Name = RIMAttributeType.VersionNumber)]
        public int VersionNumber { get; set; }

        [RIMAttribute(Name = RIMAttributeType.CompletionCode)]
        public codeable CompletionCode { get { return new codeable() { Code = "F", CodeSystem = "1.2.36.1.2001.1001.101.104.20104", CodeSystemName = "NCTIS Document Status Values", DisplayName = "Final" }; } }

        [RIMAttribute(Name = RIMAttributeType.EffectiveTime)]
        public DateTime CreationTime { get; set; }

        [RIMAttribute(Name = RIMAttributeType.ConfidentialityCode)]
        public codeable Confidentiality { get { return new codeable() { NullFlavor = "NA" }; } }

        [RIMAttribute(Name = RIMAttributeType.LanguageCode)]
        public string LanguageCode { get { return "en-AU"; } }

        [RIMRelationship(ElementName = "informationRecipient", ElementType = typeof(POCD_MT000040InformationRecipient), TypeCode = "PRCP", TargetElementName = "intendedRecipient")]
        public List<InformationRecipient> InformationRecipient { get; set; }

        [RIMRelationship(ElementName = "custodian", ElementType = typeof(POCD_MT000040Custodian), TypeCode = "CST", TargetElementName = "assignedCustodian")]
        public Custodian Custodian { get; set; }

        [RIMRelationship(ElementName = "author", ElementType = typeof(POCD_MT000040Author), TypeCode = "AUT", TargetElementName = "assignedAuthor")]
        public Author Author { get; set; }

        [RIMRelationship(ElementName = "legalAuthenticator", ElementType = typeof(POCD_MT000040LegalAuthenticator), TypeCode = "LA", TargetElementName = "assignedEntity")]
        public LegalAuthenticator LegalAuthenicator { get; set; }

       

        [RIMRelationship(ElementName = "recordTarget", ElementType = typeof(POCD_MT000040RecordTarget), TypeCode = "RCT", TargetElementName = "patientRole")]
        public Patient Patient { get; set; }

        [RIMRelationship(ElementName = "componentOf", ElementType = typeof(POCD_MT000040Component1), TypeCode = "COMP", TargetElementName = "encompassingEncounter")]
        public Encounter Encounter { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component2), TypeCode = "COMP", TargetElementName = "structuredBody")]
        public EventSummaryBody Body { get; set; }
    }


    [RIMAct(ElementType = typeof(POCD_MT000040StructuredBody), ClassCode = "DOCBODY", MoodCode = "EVN")]
    public class EventSummaryBody
    {
        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component3), TypeCode = "COMP", TargetElementName = "section")]
        public AdministrativeObservationsSection AdministrativeObservations { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component3), TypeCode = "COMP", TargetElementName = "section")]
        public EventDetailsSection Event { get; set; }

        [RIMRelationship(ElementName = "component", ElementType = typeof(POCD_MT000040Component3), TypeCode = "COMP", TargetElementName = "section")]
        public LogoSection Logo { get; set; }

    }
    
}
