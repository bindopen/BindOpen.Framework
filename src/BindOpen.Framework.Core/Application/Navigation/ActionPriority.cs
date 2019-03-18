﻿using System;

namespace BindOpen.Framework.Core.Application.Navigation
{
    /// <summary>
    /// This enumeration lists the possible kinds of actions.
    /// </summary>
    [Serializable()]
    [Flags]
    public enum ActionPriority
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Low.
        /// </summary>
        Low = 0x01,

        /// <summary>
        /// Medium.
        /// </summary>
        Medium = 0x02,

        /// <summary>
        /// High.
        /// </summary>
        High = 0x04,

        /// <summary>
        /// Very high.
        /// </summary>
        VeryHigh = 0x08
    }

}
