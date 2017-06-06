﻿/*
 * Copyright 2013 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System.Configuration;
using System.IO;
using Nehta.VendorLibrary.CDA.Generator;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = null;
            if (args != null && args.Length > 0)
                folderPath = args[0];
            else
                folderPath = ConfigurationManager.AppSettings["OutputFolder"];

            if (folderPath.IsNullOrEmptyWhitespace()) folderPath = ".";

            SpecialistLetterSample.OutputFolderPath = folderPath;
            SharedHealthSummarySample.OutputFolderPath = folderPath;
            EventSummarySample.OutputFolderPath = folderPath;
            EDischargeSummarySample.OutputFolderPath = folderPath;
            EReferralSample.OutputFolderPath = folderPath;
            
            PrepareOutputFolder(folderPath);

            var eReferralSampleCode = new EReferralSample();
            var minEReferralCda = eReferralSampleCode.MinPopulatedEReferralSample("EReferral_3A_Min.xml");
            var maxEReferralCda = eReferralSampleCode.MaxPopulatedEReferralSample("EReferral_3A_Max.xml");
            var eReferralCda1A = eReferralSampleCode.PopulateEReferralSample_1A("EReferral_1A.xml");
            var eReferralCda1B = eReferralSampleCode.PopulateEReferralSample_1B("EReferral_1B.xml");
            LevelsGenerator.Generate2("EReferral_3A_Max.xml", "EReferral_2.xml");

            var sharedHealthSummarySampleCode = new SharedHealthSummarySample();
            var minSharedHealthSummaryCda = sharedHealthSummarySampleCode.MinPopulatedSharedHealthSummarySample("SharedHealthSummary_3A_Min.xml");
            var maxSharedHealthSummaryCda = sharedHealthSummarySampleCode.MaxPopulatedSharedHealthSummarySample("SharedHealthSummary_3A_Max.xml");
            var sharedHealthSummaryCda_1A = sharedHealthSummarySampleCode.PopulateSharedHealthSummarySample_1A("SharedHealthSummary_1A.xml");
            LevelsGenerator.Generate2("SharedHealthSummary_3A_Max.xml", "SharedHealthSummary_2.xml");

            var specialistLetterSampleCode = new SpecialistLetterSample();
            var minSpecialistLetterCda = specialistLetterSampleCode.MinPopulatedSpecialistLetterSample("SpecialistLetter_3A_Min.xml");
            var maxSpecialistLetterCda = specialistLetterSampleCode.MaxPopulatedSpecialistLetterSample("SpecialistLetter_3A_Max.xml");
            var specialistLetterCda1A = specialistLetterSampleCode.PopulateSpecialistLetterSample_1A("SpecialistLetter_1A.xml");
            var specialistLetterCda1B = specialistLetterSampleCode.PopulateSpecialistLetterSample_1B("SpecialistLetter_1B.xml");
            LevelsGenerator.Generate2("SpecialistLetter_3A_Max.xml", "SpecialistLetter_2.xml");

            var dischargeSummarySampleCode = new EDischargeSummarySample();
            var minDischargeSummaryCDA = dischargeSummarySampleCode.MinPopulatedEDischargeSummary("DischargeSummary_3A_Min.xml");
            var maxDischargeSummaryCDA = dischargeSummarySampleCode.MaxPopulatedEDischargeSummary("DischargeSummary_3A_Max.xml");
            var dischargeSummaryCDA1A = dischargeSummarySampleCode.PopulateEDischargeSummarySample_1A("DischargeSummary_1A.xml");
            var dischargeSummaryCDA1B = dischargeSummarySampleCode.PopulateEDischargeSummarySample_1B("DischargeSummary_1B.xml");
            LevelsGenerator.Generate2("DischargeSummary_3A_Max.xml", "DischargeSummary_2.xml");

            var eEventSummarySampleCode = new EventSummarySample();
            var minEventSummaryCda = eEventSummarySampleCode.MinPopulatedEventSummary("EventSummary_3A_Min.xml");
            var maxEventSummaryCda = eEventSummarySampleCode.MaxPopulatedEventSummary("EventSummary_3A_Max.xml");
            var eventSummaryCda1A = eEventSummarySampleCode.PopulateEventSummarySample_1A("EventSummary_1A.xml");
            LevelsGenerator.Generate2("EventSummary_3A_Max.xml", "EventSummary_2.xml");

            var genericObjectReuseSampleCode = new GenericObjectReuseSample();
            var sampleSubjectOfCare = genericObjectReuseSampleCode.PopulateSubjectOfCare();
            var sampleAutrhor = genericObjectReuseSampleCode.PopulateAuthor();
            var sampleCustodian = genericObjectReuseSampleCode.PopulateCustodian();
            var sampleAuthenticator = genericObjectReuseSampleCode.PopulateAuthenticator();
            var sampleRecipient = genericObjectReuseSampleCode.PopulateRecipient();
        }

        static void PrepareOutputFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                if (File.Exists(folderPath + @"\logo.png"))
                    File.Delete(folderPath + @"\logo.png");

                if (File.Exists(folderPath + @"\path1234.pdf"))
                    File.Delete(folderPath + @"\path1234.pdf");

                if (File.Exists(folderPath + @"\x-ray.jpg"))
                    File.Delete(folderPath + @"\x-ray.jpg");

                if (File.Exists(folderPath + @"\pit.txt"))
                    File.Delete(folderPath + @"\pit.txt");

                if (File.Exists(folderPath + @"\attachment.pdf"))
                    File.Delete(folderPath + @"\attachment.pdf");
            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }

            File.Copy(@"Attachments\logo.png", folderPath + @"\logo.png");
            File.Copy(@"Attachments\attachment.pdf", folderPath + @"\attachment.pdf");
            File.Copy(@"Attachments\path1234.pdf", folderPath + @"\path1234.pdf");
            File.Copy(@"Attachments\x-ray.jpg", folderPath + @"\x-ray.jpg");
            File.Copy(@"Attachments\pit.txt", folderPath + @"\pit.txt");
        }
    }
}
