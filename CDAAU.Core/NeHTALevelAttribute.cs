using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;

namespace TeamUnicorn.CDAAU.Core
{

    /// <summary>
    ///  assign RIM attributes to an Act
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class NeHTALevelAttribute : Attribute
    {
        public enum NeHTALevels { Level2, Level3a };

        /// <summary>
        /// </summary>
        public NeHTALevels Level { get; set; }
    }

   




}