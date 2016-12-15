using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Oridashi.CDAAU.DischargeSummary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CDAAU.Test
{
    [TestClass]
    public class DischargeSummaryTest
    {

        protected DischargeSummary SampleDocument()
        {
            DischargeSummary ds = new DischargeSummary()
            {
                Id = Guid.NewGuid(),
                SetId = Guid.NewGuid(),
                VersionNumber = 1,
                CreationTime = new DateTime(2015, 1, 1, 10, 0, 0),
                LocationOfDischarge = new LocationOfDischarge() { WardDeparment = "Ward 7" },
                Encounter = new Encounter()
                {
                    AdmissionPeriod = new time_period() { To = new DateTime(2011, 5, 1, 10, 22, 0), From = new DateTime(2011, 6, 1, 11, 10, 0) },
                    SeparationMode = DischargeDispositionType.DischargeToResidence,
                    ResponsibleHealthProfessionalAtDischarge = new ResponsibleHealthProfessional()
                    {
                        Id = Guid.NewGuid(),
                        Name = new name() { Family = "JONES", Given1 = "JANET", Title = "DR" },
                        HPII = "8003611234567890",
                    },
                    Facility = new Encounter.HealthCareFacility()
                    {
                        Role = new codeable() { Code = "HOSP", CodeSystem = "2.16.840.1.113883.1.11.17660", CodeSystemName = "HL7 ServiceDeliveryLocationRoleType", DisplayName = "Hospital" },
                        Department = "Ward 7",
                        ParentOrganization = new ParentOrganizationDetails()
                        {
                            Name = "General Hospital",
                            Address = new address() { AddressLine1 = "line 1", AddressLine2 = "line2", Postcode = "3071", Suburb = "Thornbury", State = "VIC" },
                        },
                    }
                },
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
                    Role = new codeable() { OriginalText = "TEST" },
                    HPII = "8003611234567890",
                    Employer = new employer() { DepartmentName = "ED", OrganizationName = "General Hospital", HPIO = "8003621231167899" }
                },
                Patient = new Patient()
                {
                    Id = new identifier() { AuthorityName = "Hospital X", Root = "1.2.36.1.2001.1005.41.8003621566684455", Extension = "0819432" },
                    Name = new name() { Family = "SMITH", Given1 = "GEORGE", Title = "MR" },
                    IHI = "8003601234512345",
                    MRN = new identifier() { Root = "1.2.36.1.2001.1005.41.8003621566684455", Extension = "0819432", AuthorityName = "Main Hospital" },
                    Gender = Patient.GenderType.Male,
                    DateOfBirth = new DateTime(1944, 12, 15),
                    Address = new address() { AddressLine1 = "line 1", AddressLine2 = "line2", Postcode = "3071", Suburb = "Thornbury", State = "VIC" },
                },
                InformationRecipient = new List<InformationRecipient>() {
                    new InformationRecipient() {
                        Name = new name() { Family="KLEIN", Given1="MARY", Title="DR" },
                        OrganizationName = "Main Street Clinic",
                        HPIO = "8003621234567890",
                    }
                },
                NominatedHealthCareProviderOrganization = new List<NominatedHealthcareProviderOrganization>()
                {
                    new NominatedHealthcareProviderOrganization() {
                        Id = "555",
                        Role = new codeable() { OriginalText="GP" },
                        Department = "Admin",
                        ParentOrganization = new ParentOrganizationDetails()
                        {
                            Name = "Main Street Clinic",
                            HPIO = "8003621234567890", 
                        },
                         Address = new address() { AddressLine1 = "line 1", AddressLine2 = "line2", Postcode = "3071", Suburb = "Thornbury", State = "VIC" },
                         Phone = "0394951429",
                         Fax = "0394951428",
                         Email = "mary@mainstreet.com.au",
                    },
                },
                NominatedHealthCareProviderPerson = new List<NominatedHealthcareProviderPerson>()
                {
                    new NominatedHealthcareProviderPerson() {
                         HPII = "8003611234567890",
                         Employer = new employer() { DepartmentName = "Admin", OrganizationName = "Main Street Clinic", HPIO = "8003621231167899" },
                         Name = new name() { Family="KLEIN", Given1="MARY", Title="DR" },
                         Address = new address() { AddressLine1 = "line 1", AddressLine2 = "line2", Postcode = "3071", Suburb = "Thornbury", State = "VIC" },
                         Phone = "0394951429",
                         Fax = "0394951428",
                         Email = "mary@mainstreet.com.au",
                         Role = new codeable { OriginalText = "GP" },
                    },
                },
                Body = new DischargeSummaryBody()
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
                    Event = new EventSection()
                    {
                        StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">event</paragraph>",
                        ClinicalInterventions = new ClinicalInterventionsSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">intervention</paragraph>",
                            ClinicalInterventions = new List<ClinicalIntervention>()
                            {
                                new ClinicalIntervention() { Id = Guid.NewGuid(), ClinicalInterventionDescription = new codeable() { OriginalText = "Blood Transfusion" } },
                                new ClinicalIntervention() { Id = Guid.NewGuid(), ClinicalInterventionDescription = new codeable() { Code="410177006", CodeSystem="2.16.840.1.113883.6.96", CodeSystemName="SNOMED CT-AU", DisplayName="Special Diet Education" } },
                            }
                        },
                        ProblemDiagnoses = new ProblemDiagnosesSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">diagnosis</paragraph>",
                            ProblemDiagnoses = new List<ProblemDiagnosis>() { 
                                new ProblemDiagnosis() { Id = Guid.NewGuid(), ProblemDiagnosisDescription = new codeable() { OriginalText="Chronic radiation cystitis" } },
                                new ProblemDiagnosis() { Id = Guid.NewGuid(), ProblemDiagnosisDescription = new codeable() { Code="191268006", CodeSystem="2.16.840.1.113883.6.96", CodeSystemName="SNOMED CT-AU", DisplayName="Chronic anaemia" } } 
                            },
                        },
                        ClinicalSynopsis = new ClinicalSynopsisSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">Synopsis</paragraph>",
                        },
                        DiagnosticInvestigations = new DiagnosticInvestigationsSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">diagnostic investigations</paragraph>",
                            PathologyTestResults = new List<PathologyTestResultSection>()
                            {
                               new PathologyTestResultSection() 
                               {
                                   StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">pathology test result 1</paragraph>",
                               },
                               new PathologyTestResultSection() 
                               {
                                   StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">pathology test result 2</paragraph>",
                               },
                            },
                            ImagingExaminationResults = new List<ImagingExaminationResultSection>()
                            {
                                new ImagingExaminationResultSection() 
                               {
                                   StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">imaging examination result 1</paragraph>",
                               },
                               new ImagingExaminationResultSection() 
                               {
                                   StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">imaging examination result 2</paragraph>",
                               },
                            },
                        },
                    },
                    Medications = new MedicationsSection()
                    {
                        StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">medications</paragraph>",
                        CurrentMedications = new CurrentMedicationsSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">current medications</paragraph>",
                            TherapeuticGoods = new List<TherapeuticGood>()
                            {
                                new TherapeuticGood()
                                {
                                    Id = Guid.NewGuid(),
                                    Status = ActStatusType.active,
                                    TherapeuticGoodIdentification = new TherapeuticGood.ManufacturedProduct()
                                    {
                                        TherapeuticGoodIdentification = new codeable() { OriginalText = "Iron Up+" },
                                    },
                                    MedicationDuration = new time_period()
                                    {
                                        To = new DateTime(2011, 6, 1),
                                        From = new DateTime(2011, 7, 1)
                                    },
                                    ReasonForTherapeuticGood = "Low iron",
                                    AdditionalComments = "Take at the same time each day",
                                    ChangeDetail = new TherapeuticGood.ChangeDetails()
                                    {
                                        ChangesMade = new codeable() { OriginalText = "increased dose" },
                                        ReasonForChange = "Iron Deficiency"
                                    }
                                }
                            }
                        },
                        CeasedMedications = new CeasedMedicationsSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">ceased medications</paragraph>",
                            ExclusionStatement = GlobalStatementType.NoneKnown,
                        }
                    },
                    HealthProfile = new HealthProfileSection()
                    {
                        StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">health profile section</paragraph>",
                        AdverseReactions = new AdverseReactionsSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">adverse reactions section</paragraph>",
                            AdverseReactions = new List<AdverseReaction>()
                            {
                                new AdverseReaction() 
                                {
                                    Id = Guid.NewGuid(),
                                    AdverseReactionType = new codeable() { OriginalText="rash" },
                                    AgentDescription = new AdverseReaction.Agent()
                                    {
                                        AgentDescription = new codeable() { OriginalText="bee venom" },
                                    }
                                }
                            }
                        },
                        Alerts = new AlertsSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">alerts section</paragraph>",
                            Alerts = new List<AlertsSection.AlertDetails>()
                            {
                                new AlertsSection.AlertDetails()
                                {
                                    Id = Guid.NewGuid(),
                                    AlertType = new codeable() { OriginalText="access" },
                                    AlertDescription = new codeable() { OriginalText="vicious dog" },
                                }
                            }
                        },
                    },
                    Plan = new PlanSection()
                    {
                        StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">plan section</paragraph>",
                        ArrangedServices = new ArrangedServicesSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">arranged services section</paragraph>",
                            ArrangedServices = new List<ArrangedServicesSection.ArrangedService>()
                            {
                                new ArrangedServicesSection.ArrangedService()
                                {
                                    Id = Guid.NewGuid(),
                                    ArrangedServiceDescription = new codeable() { OriginalText="Dietitian Visit" },
                                    ServiceCommencementWindow = new time_period() { From=new DateTime(2011, 7, 1) },
                                    ServiceBookingStatus = ArrangedServicesSection.ArrangedService.ServceBookingStatusType.AppointmentMade,
                                },
                                new ArrangedServicesSection.ArrangedService()
                                {
                                    Id = Guid.NewGuid(),
                                    ArrangedServiceDescription = new codeable() { OriginalText="Dermatologist" },
                                    ServiceCommencementWindow = new time_period() { From=new DateTime(2011, 7, 1) },
                                    ServiceBookingStatus = ArrangedServicesSection.ArrangedService.ServceBookingStatusType.Intended,
                                },
                            },

                        },
                        RecommendationsInformation = new RecommendationsInformationSection()
                        {
                            StructuredNarrative = "<paragraph xmlns=\"urn:hl7-org:v3\">recommendations/information provided section</paragraph>",
                            Recommendations = new List<RecommendationDetails>()
                            {
                                new RecommendationDetails()
                                {
                                    Id = Guid.NewGuid(),
                                    RecommendationNote = "recommend long term follow-up",
                                    RecommendationRecipientPerson = new RecommendationRecipientPersonDetails()
                                    {
                                        Id = "666",
                                        HPII = "8003611234567890",
                                        Employer = new employer() { DepartmentName = "Admin", OrganizationName = "Main Street Clinic", HPIO = "8003621231167899" },
                                        Name = new name() { Family="KLEIN", Given1="MARY", Title="DR" },
                                        Role = new codeable { OriginalText = "GP" },
                                    }
                                },
                                 new RecommendationDetails()
                                {
                                    Id = Guid.NewGuid(),
                                    RecommendationNote = "recommend appointment",
                                    RecommendationRecipientOrganization = new RecommendationRecipientOrganizationDetails()
                                    {
                                        Id = Guid.NewGuid(),
                                        Role = new codeable() { OriginalText="GP" },
                                        Department = "Admin",
                                        ParentOrganization = new ParentOrganizationDetails()
                                        {
                                            Name = "Main Street Clinic",
                                            HPIO = "8003621234567890", 
                                        }
                                    }
                                        
                                },
                            },
                            InformationProvidedRecomendationNote = "recommend this follow-up is attended regularly, very important to satifactory outcome",
                        },
                    },
                },

            };

            ds.Author.Name = new name() { Family = "JONES", Given1 = "JANET", Title = "DR" };
            ds.Author.LocalProviderId = new identifier() { AuthorityName = "Hospital X", Root = "1.2.36.1.2001.1005.41.8003621566684455", Extension = "0819432" };
            return ds;
        }


        [TestMethod]
        public void Generate()
        {

            DischargeSummary ds = SampleDocument();

            Nehta.HL7.CDA.POCD_MT000040ClinicalDocument cda = CDAGenerator.Generate(ds, "W. Australia Standard Time");

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Nehta.HL7.CDA.POCD_MT000040ClinicalDocument));
            System.IO.StringWriter sw = new System.IO.StringWriter();
            ser.Serialize(sw, cda);
            string xml = sw.GetStringBuilder().ToString();
            System.IO.File.WriteAllText("output.xml", xml, System.Text.UnicodeEncoding.Unicode);
            System.Diagnostics.Process.Start("output.xml");

            System.Xml.Xsl.XslCompiledTransform tf = new System.Xml.Xsl.XslCompiledTransform();
            tf.Load(@"file://D:\Oridashi\Products\CDAAU\CDAAU.Test\NEHTA_Generic_CDA_Stylesheet-1.2.7.xsl");
            tf.Transform("output.xml", "rendered.html");

            System.Diagnostics.Process.Start("rendered.html");
        }

        [TestMethod]
        public void GenerateLevel2()
        {
            DischargeSummary ds = SampleDocument();

            Nehta.HL7.CDA.POCD_MT000040ClinicalDocument cda = CDAGenerator.Generate(ds, "AUS Eastern Standard Time", NeHTALevelAttribute.NeHTALevels.Level2);

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Nehta.HL7.CDA.POCD_MT000040ClinicalDocument));
            System.IO.StringWriter sw = new System.IO.StringWriter();
            ser.Serialize(sw, cda);
            string xml = sw.GetStringBuilder().ToString();
            System.IO.File.WriteAllText("output.xml", xml, System.Text.UnicodeEncoding.Unicode);
            System.Diagnostics.Process.Start("output.xml");

    
            System.Xml.Xsl.XslCompiledTransform tf = new System.Xml.Xsl.XslCompiledTransform();
            tf.Load(@"file://D:\Oridashi\Products\CDAAU\CDAAU.Test\NEHTA_Generic_CDA_Stylesheet-1.2.7.xsl");
            tf.Transform("output.xml", "rendered.html");

            System.Diagnostics.Process.Start("rendered.html");

        }

        [TestMethod]
        public void GenerateDocuments()
        {
            string output = CDAIGGenerator.Generate(typeof(DischargeSummary));
            System.IO.File.WriteAllText("IG.html", output);
            System.Diagnostics.Process.Start("IG.html");

        }
    }
}
