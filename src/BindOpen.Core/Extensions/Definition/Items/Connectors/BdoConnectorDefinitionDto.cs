﻿using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    [XmlType("BdoConnectorDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "connector.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoConnectorDefinitionDto : BdoExtensionItemDefinitionDto, IBdoConnectorDefinitionDto
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
        /// Data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// The data source detail specification of this instance.
        /// </summary>
        [XmlElement("dataSource.specification")]
        public DataElementSpecSet DatasourceDetailSpec { get; set; } = new DataElementSpecSet();

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinitionDto class.
        /// </summary>
        public BdoConnectorDefinitionDto()
        {
        }

        #endregion
    }

}
