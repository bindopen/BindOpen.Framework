﻿using BindOpen.Framework.Databases.Extensions;

namespace BindOpen.Framework.Extensions.Carriers
{
    /// <summary>
    /// This enumeration lists all the possible kinds of database carriers.
    /// </summary>
    public enum DatabaseCarrierKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Database field.
        /// </summary>
        DbField,

        /// <summary>
        /// Database table.
        /// </summary>
        DbTable
    }

    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the DatabaseCarrierKind enumeration.
    /// </summary>
    public static class DatabaseCarrierKindExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified carrier kind.
        /// </summary>
        /// <param name="aDatabaseCarrierKind">The carrier kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this DatabaseCarrierKind aDatabaseCarrierKind)
        {
            return aDatabaseCarrierKind.ToString().ToLower().GetUniqueName_database();
        }
    }

    #endregion

}
