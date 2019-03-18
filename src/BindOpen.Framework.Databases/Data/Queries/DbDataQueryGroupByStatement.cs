﻿using BindOpen.Framework.Databases.Extensions.Carriers;
using System.Collections.Generic;
using System.ComponentModel;

namespace BindOpen.Framework.Databases.Data.Queries
{

    /// <summary>
    /// This class represents the GroupBy statement of a database data query.
    /// </summary>
    public class DbDataQueryGroupByStatement
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<DbField> _Fields;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<DbField> Fields
        {
            get { return this._Fields; }
            set { this._Fields = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryGroupByStatement class.
        /// </summary>
        public DbDataQueryGroupByStatement()
        {
        }

        #endregion

    }
}