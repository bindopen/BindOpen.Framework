﻿using System;
using BindOpen.Framework.Data.Queries;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_CurrentDate(object[] parameters)
        {
            return "getdate()";
        }
    }
}