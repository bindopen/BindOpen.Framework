﻿namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This interface represents a named DTO.
    /// </summary>
    public interface INamed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        #endregion
    }
}
