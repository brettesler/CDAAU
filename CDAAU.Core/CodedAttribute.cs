using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.Core
{

    /// <summary>
    ///  assign codes to enums
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field)]
    public class CodedAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// </summary>
        public string CodeSystem { get; set; }

        /// <summary>
        /// </summary>
        public string CodeSystemName { get; set; }

        /// <summary>
        /// </summary>
        public string DisplayName { get; set; }
    }
}