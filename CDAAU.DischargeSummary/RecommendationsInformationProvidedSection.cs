using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.DischargeSummary
{
    [RIMAct(ElementType = typeof(POCD_MT000040Section), ClassCode = "DOCSECT", MoodCode = "EVN")]
    [RIMActCode(Code = "101.20016", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Record of Recommendations and Information Provided")]
    [RIMActTitle(Title = "Record of Recommendations and Information Provided")]
    public class RecommendationsInformationSection
    {
        public RecommendationsInformationSection() { }

        [RIMAttribute(Name = RIMAttributeType.Text)]
        public string StructuredNarrative { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "act")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public List<RecommendationDetails> Recommendations { get; set; }

        [RIMRelationship(ElementName = "entry", ElementType = typeof(POCD_MT000040Entry), TypeCode = "DRIV", TargetElementName = "act")]
        [RIMContainerAct(ElementType = typeof(POCD_MT000040Act), ClassCode = "INFRM", MoodCode = "EVN", AutoId=true)]
        [RIMContainerActCode(Code = "102.20016.4.1.2", CodeSystem = "1.2.36.1.2001.1001.101", CodeSystemName = "NCTIS Data Components", DisplayName = "Information Provided")]
        [NeHTALevel(Level = NeHTALevelAttribute.NeHTALevels.Level3a)]
        public string InformationProvidedRecomendationNote { get; set; }

    }
}
