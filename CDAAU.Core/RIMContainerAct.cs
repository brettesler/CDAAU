using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.Core
{
    /// <summary>
    ///  define RIM Act templated to contain the property value
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class RIMContainerAct : Attribute
    {
        /// <summary>
        /// </summary>
        public string ClassCode { get; set; }

        /// <summary>
        /// </summary>
        public string MoodCode { get; set; }

        /// <summary>
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        ///  add automatic id value on act
        /// </summary>
        public bool AutoId { get; set; }

        /// <summary>
        /// xml ID setting
        /// </summary>
        public string ID { get; set; }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class RIMContainerActCode : Attribute
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

    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class RIMEntityAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        /// </summary>
        public string ElementName { get; set; }
    }
 
}