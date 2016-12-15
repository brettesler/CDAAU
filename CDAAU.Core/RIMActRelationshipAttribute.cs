using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.Core
{
      
    /// <summary>
    ///  assign RIM attributes for a Participation or ActRelationship
    ///  define class C# type for factory
    ///  define child element name for the object (Act/Role) to which this attribute applies 
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class RIMRelationshipAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// </summary>
        public string TargetElementName { get; set; }
    }
}