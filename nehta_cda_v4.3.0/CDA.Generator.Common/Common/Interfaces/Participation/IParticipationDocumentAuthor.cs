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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IParticipationDocumentAuthor interface defines the properties associated with a participation
    /// when the participation / participant is the document author
    /// </summary>
    public interface IParticipationDocumentAuthor
    {
        /// <summary>
        /// The involvement or role of the participant in the related action from a healthcare 
        /// perspective rather than the specific participation perspective.
        /// </summary>
        [CanBeNull]
        ICodableText Role { get; set; }

        /// <summary>
        /// The participation period
        /// </summary>
        [CanBeNull]
        ParticipationPeriod AuthorParticipationPeriodOrDateTimeAuthored { get; set; }

        /// <summary>
        /// The author
        /// </summary>
        [CanBeNull]
        IAuthor Participant { get; set; }

        /// <summary>
        /// Validates this Author
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this Author
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateV2(string path, List<ValidationMessage> messages);

        /// <summary>
        /// Validates this Author
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="nullableRole">A nullable role </param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ValidateOptional(string path, bool nullableRole, List<ValidationMessage> messages);
    }
}
