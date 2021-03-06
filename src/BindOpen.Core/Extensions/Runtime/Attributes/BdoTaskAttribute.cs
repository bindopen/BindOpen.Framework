﻿using BindOpen.Data.Items;
using System;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents an attribute of tasks.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoTaskAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskAttribute class.
        /// </summary>
        public BdoTaskAttribute() : base()
        {
        }

        #endregion
    }
}
