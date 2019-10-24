﻿using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents a stored data item.
    /// </summary>
    [Serializable()]
    [XmlType("StoredDataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("storedDataItem", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class StoredDataItem : IdentifiedDataItem, IStoredDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name preffix of this instance.
        /// </summary>
        [XmlIgnore()]
        protected string NamePreffix { get; set; } = "Object_";

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlElement("creationDate")]
        public string CreationDate { get; set; } = null;

        /// <summary>
        /// Specification of the CreationDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool CreationDateSpecified => !string.IsNullOrEmpty(CreationDate);

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [XmlElement("lastModificationDate")]
        public string LastModificationDate { get; set; } = null;

        /// <summary>
        /// Specification of the LastModificationDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LastModificationDateSpecified => !string.IsNullOrEmpty(LastModificationDate);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StoredDataItem class.
        /// </summary>
        public StoredDataItem() : this("")
        {
        }

        /// <summary>
        /// Instantiates a new instance of the StoredDataItem class considering the specified name and preffix name.
        /// </summary>
        /// <param name="id">The ID to consider.</param>
        /// <param name="creationDate">The creation date of this instance.</param>
        public StoredDataItem(
            string id = null,
            DateTime? creationDate = null) : base(id)
        {
            CreationDate = creationDate?.ToString();
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Declares this instance has changed.
        /// </summary>
        public virtual void DeclareUpdate()
        {
            // we update the modification date
            LastModificationDate = ObjectHelper.ToString(DateTime.Now);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            StoredDataItem item = base.Clone() as StoredDataItem;
            if (CreationDate != null)
                item.CreationDate = ObjectHelper.ToString(DateTime.Now);
            item.LastModificationDate = null;
            return item;
        }

        #endregion       
    }
}