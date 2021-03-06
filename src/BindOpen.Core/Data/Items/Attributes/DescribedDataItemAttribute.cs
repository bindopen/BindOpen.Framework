﻿using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents an indexed data item attribute.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public abstract class DescribedDataItemAttribute : Attribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// The title of this instance.
        /// </summary>
        public string Title { get; set; } = null;

        /// <summary>
        /// The description of this instance.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        public string CreationDate { get; set; } = null;

        /// <summary>
        /// The last modification date of this instance.
        /// </summary>
        public string LastModificationDate { get; set; } = null;

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int Index { get; set; } = -1;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemAttribute class.
        /// </summary>
        protected DescribedDataItemAttribute() : base()
        {
        }

        #endregion
    }
}
