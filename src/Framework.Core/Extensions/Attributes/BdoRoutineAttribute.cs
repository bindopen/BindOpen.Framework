﻿using BindOpen.Framework.Data.Items;
using System;

namespace BindOpen.Framework.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of routines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoRoutineAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineAttribute class.
        /// </summary>
        public BdoRoutineAttribute() : base()
        {
        }

        #endregion
    }
}
