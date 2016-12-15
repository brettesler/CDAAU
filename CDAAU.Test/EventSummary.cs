using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Oridashi.CDAAU.EventSummary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CDAAU.Test
{
    [TestClass]
    public class EventSummaryTest
    {
        protected EventSummary SampleDocument()
        {
            EventSummary es = new EventSummary()
            {
                Id = Guid.NewGuid(),
                SetId = Guid.NewGuid(),
                Title = "Hospital Admission",
                VersionNumber = 1,
                CreationTime = new DateTime(2015, 1, 1, 10, 0, 0),
                LegalAuthenicator = new EventSummary.LegalAuthenticator() { AttestationTime = DateTime.Now },
                Custodian = new Custodian()
                {
                    Id = System.Guid.NewGuid().ToString(),
                    HPIO = "8003621234567890",
                    OrganizationName = "General Hospital",
                },
                Author = new Author()
                {
                    Id = System.Guid.NewGuid().ToString(),
                    CreationTime = new DateTime(2011, 6, 1, 11, 10, 6),
                    Name = new name() { Family = "JONES", Given1 = "JANET", Title = "DR" },
                    LocalProviderId = new identifier() { AuthorityName = "Hospital X", Root = "1.2.36.1.2001.1005.41.8003621566684455", Extension = "0819432" },
                    HPII = "8003611234567890",
                    Employer = new employer() { DepartmentName = "ED", OrganizationName = "General Hospital", HPIO = "8003621231167899" },
                    Role = new codeable() { OriginalText = "TEST" }
                },
                Patient = new Patient()
                {
                    Id = new identifier(){ AuthorityName="Hospital X", Root="1.2.36.1.2001.1005.41.8003621566684455", Extension="0819432" },
                    Name = new name() { Family = "SMITH", Given1 = "GEORGE", Title = "MR" },
                    IHI = "8003601234512345",
                    MRN = new identifier(){ AuthorityName="Hospital X", Root="1.2.36.1.2001.1005.41.8003621566684455", Extension="0819432" },
                    Gender = Patient.GenderType.Male,
                    DateOfBirth = new DateTime(1944, 12, 15),
                    Address = new address() { AddressLine1 = "line 1", AddressLine2 = "line2", Postcode = "3071", Suburb = "Thornbury", State = "VIC" },
                    IndigenousStatus = Patient.IndigenousStatusType.BothAboriginalAndTSI,
                },
                Encounter = new Encounter()
                {
                    EncounterPeriod = new time_period() { From = new DateTime(2011, 6, 1, 11, 0, 0), To = new DateTime(2011, 6, 1, 11, 10, 30), FromInclusive = true, ToInclusive = false },
                },
                Body = new EventSummaryBody()
                {
                    Logo = new LogoSection()
                    {
                        LogoFile = "test.png"
                    },
                    AdministrativeObservations = new AdministrativeObservationsSection()
                    {
                        StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">administrative observations</paragraph>",
                        Age = new physical_quantity() { Unit = "a", Value = 64.0f },
                        MedicareEntitlement = new AdministrativeObservationsSection.MedicareNumber()
                        {
                            Id = "3423423213123",
                            EntitlementValidityDuration = new time_period() { From = new DateTime(2001, 1, 1), To = new DateTime(2017, 1, 1) },
                            EntitlementSubject = new AdministrativeObservationsSection.EntitlementDetails.EntitlementSubjectDetails()
                            {
                                PatientId = new identifier() { Root = "1.2.36.1.2001.1005.41.8003621566684455", Extension = "0819432", AuthorityName = "Main Hospital" },
                            }

                        },
                    },
                    Event = new EventDetailsSection()
                    {
                        StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">event text is the clinical synopsis</paragraph>",
                        ClinicalSynopsisDescription = "event text is the clinical synopsis",
                    }
                }
            };

            return es;
        }

        [TestMethod]
        public void Generate()
        {
            EventSummary es = SampleDocument();

            Nehta.HL7.CDA.POCD_MT000040ClinicalDocument cda = CDAGenerator.Generate(es, "Cen. Australia Standard Time" );

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Nehta.HL7.CDA.POCD_MT000040ClinicalDocument));
            System.IO.StringWriter sw = new System.IO.StringWriter();
            ser.Serialize(sw, cda);
            string xml = sw.GetStringBuilder().ToString();
            System.IO.File.WriteAllText("outputes.xml", xml, System.Text.UnicodeEncoding.Unicode);
            System.Diagnostics.Process.Start("outputes.xml");

            System.Xml.Xsl.XslCompiledTransform tf = new System.Xml.Xsl.XslCompiledTransform();
            tf.Load(@"file://D:\Oridashi\Products\CDAAU\CDAAU.Test\NEHTA_Generic_CDA_Stylesheet-1.2.7.xsl");
            tf.Transform("outputes.xml", "renderedes.html");

            System.Diagnostics.Process.Start("renderedes.html");
        }
    }
}
