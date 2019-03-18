﻿using System;

namespace BindOpen.Framework.Runtime.System
{

    /// <summary>
    /// This enumeration lists the possible visibilities of an application module.
    /// </summary>
    [Flags]
    public enum InstanceIndexation
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any = InstanceIndexation.Default | InstanceIndexation.Central | InstanceIndexation.Cloud | InstanceIndexation.Box | InstanceIndexation.Standalone,
        
        /// <summary>
        /// The application module is not indexed. Standalone application instances are not indexed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Default saying that the indexation is the one of the application module. And if it is Default, then None.
        /// </summary>
        Default = 0x1 << 0,

        /// <summary>
        /// Global application module instance.
        /// </summary>
        Central = 0x1 << 1,

        /// <summary>
        /// Cloud application module instance.
        /// </summary>
        Cloud = 0x1 << 2,

        /// <summary>
        /// Box application module instance.
        /// </summary>
        Box = 0x1 << 3,
        
        /// <summary>
        /// Standalone.
        /// </summary>
        Standalone = 0x1 << 4,
    }

}
