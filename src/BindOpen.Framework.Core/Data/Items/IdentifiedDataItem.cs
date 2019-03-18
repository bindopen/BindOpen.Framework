﻿using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [Serializable()]
    [XmlType("IdentifiedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("identifiedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class IdentifiedDataItem : DataItem, IIdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [XmlAttribute("id")]
        public String Id { get; set; } = null;

        /// <summary>
        /// Specification of the ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean IdSpecified => !string.IsNullOrEmpty(this.Id);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedDataItem class.
        /// </summary>
        /// <param name="id">The ID to consider.</param>
        public IdentifiedDataItem(String id = null) : base()
        {
            this.Id = id?.Length == 0 ? IdentifiedDataItem.NewGuid() : id;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the identifier key.
        /// </summary>
        public virtual String Key()
        {
            return this.Id;
        }

        /// <summary>
        /// Returns a new Guid.
        /// </summary>
        public static String NewGuid()
        {
            return global::System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            IdentifiedDataItem item = base.Clone() as IdentifiedDataItem;
            if (this.Id != null)
                item.Id = IdentifiedDataItem.NewGuid();
            return item;
        }

        #endregion       
    }
}
