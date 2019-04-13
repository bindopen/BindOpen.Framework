﻿using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Formats
{
    /// <summary>
    /// This class represents the format definition.
    /// </summary>
    [Serializable()]
    [XmlType("FormatDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "format.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class FormatDefinitionDto : AppExtensionItemDefinitionDto, IFormatDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public string ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        [XmlElement("viewerClass")]
        public string ViewerClass
        {
            get;
            set;
        }

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind DataSourceKind { get; set; } = DataSourceKind.Memory;

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        public FormatDefinitionDto()
        {
        }

        #endregion
    }
}
