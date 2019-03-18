﻿using System;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // System

        /// <summary>
        /// Evaluates the script word $SQLNEWGUID.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_NewGuid()
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLRANDOM.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Random()
        {
            return "";
       }
    }
}