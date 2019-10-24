﻿namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This enumeration lists all the kinds of data query jointures.
    /// </summary>
    public enum DbDataQueryJointureKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Inner.
        /// </summary>
        Inner,

        /// <summary>
        /// Left.
        /// </summary>
        Left,

        /// <summary>
        /// Right.
        /// </summary>
        Right,

        /// <summary>
        /// Union.
        /// </summary>
        Union
    }

}