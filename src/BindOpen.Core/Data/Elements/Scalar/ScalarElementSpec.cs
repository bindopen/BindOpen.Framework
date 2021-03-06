﻿using BindOpen.Data.Common;
using BindOpen.System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    [XmlType("ScalarElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ScalarElementSpec : DataElementSpec, IScalarElementSpec
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Any)]
        public new DataValueTypes ValueType
        {
            get { return base.ValueType; }
            set { base.ValueType = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        public ScalarElementSpec() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpec(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpec(
            string name,
            DataValueTypes dataValueType = DataValueTypes.Text,
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
            Name = name;
            ValueType = dataValueType;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckItem(
            object item,
            IDataElement dataElement = null)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null)
        {
            return new BdoLog();
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            ScalarElementSpec aScalarElementSpec = base.Clone(areas) as ScalarElementSpec;
            return aScalarElementSpec;
        }

        #endregion
    }

}
