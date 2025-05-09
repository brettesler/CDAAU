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
    [AttributeUsage(AttributeTargets.Class)]
    public class RIMActAttribute : Attribute
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
    }

    /// <summary>
    ///  assign RIM attributes to a Role
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class RIMRoleAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public string ClassCode { get; set; }

        /// <summary>
        /// </summary>
        public Type ElementType { get; set; }
    }

    /// <summary>
    ///  assign code attribute fixed value to an Act
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class RIMActCodeAttribute : Attribute
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

    /// <summary>
    ///  assign title attribute fixed value to an Act
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class RIMActTitleAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    ///  assign templateId attribute fixed value to an Act
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class RIMTemplateIdAttribute: Attribute
    {
        /// <summary>
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// </summary>
        public string Extension { get; set; }
    }


    /// <summary>
    ///  assign typeId attribute fixed value to an Act
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class RIMTypeIdAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// </summary>
        public string Extension { get; set; }
    }




}