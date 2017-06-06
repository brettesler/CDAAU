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

using System.Collections.Generic;
using CDA.Generator.Common.SCSModel.MedicareOverview.Entities;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
  /// This interface encapsulates all the SCS specific context for an MedicareOverview
    /// </summary>
    public interface IMedicareOverviewContent
    {
        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

        /// <summary>
        /// Medicare View Exclusion Statement
        /// </summary>
        [CanBeNull]
        ExclusionStatement ExclusionStatement { get; set; }

        /// <summary>
        /// Medicare DVA Funded Services History 
        /// </summary>
        [CanBeNull]
        MedicareDVAFundedServicesHistory MedicareDvaFundedServicesHistory { get; set; }

        /// <summary>
        /// Pharmaceutical Benefit Items
        /// </summary>
        [CanBeNull]
        PharmaceuticalBenefitsHistory PharmaceuticalBenefitsHistory { get; set; }

        /// <summary>
        /// Australian Childhood Immunisation Register Component
        /// </summary>
        [CanBeNull]
        AustralianChildhoodImmunisationRegisterHistory AustralianChildhoodImmunisationRegisterHistory { get; set; }

        /// <summary>
        /// Australian Organ Donor Register Component
        /// </summary>
        [CanBeNull]
        AustralianOrganDonorRegisterDecisionInformation AustralianOrganDonorRegisterDecisionInformation { get; set; }

        /// <summary>
        /// Validate the SCS Content for Medicare View
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
