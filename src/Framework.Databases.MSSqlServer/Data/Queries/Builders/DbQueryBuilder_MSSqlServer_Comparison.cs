﻿using System;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Equal(string value1, string value2)
        {
            if (string.Equals(value1, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value1 = "NULL";
            if (string.Equals(value2, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value2 = "NULL";

            return "(" + value1 + "=" + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_NotEqual(string value1, string value2)
        {
            if (string.Equals(value1, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value1 = "NULL";
            if (string.Equals(value2, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value2 = "NULL";

            return "(" + value1 + "<>" + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Greater(string value1, string value2)
        {
            if (string.Equals(value1, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value1 = "NULL";
            if (string.Equals(value2, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value2 = "NULL";

            return "(" + value1 + ">" + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_GreaterOrEqual(string value1, string value2)
        {
            if (string.Equals(value1, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value1 = "NULL";
            if (string.Equals(value2, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value2 = "NULL";

            return "(" + value1 + ">=" + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Less(string value1, string value2)
        {
            if (string.Equals(value1, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value1 = "NULL";
            if (string.Equals(value2, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value2 = "NULL";

            return "(" + value1 + "<" + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_LessOrEqual(string value1, string value2)
        {
            if (string.Equals(value1, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value1 = "NULL";
            if (string.Equals(value2, "%NULL()", StringComparison.OrdinalIgnoreCase))
                value2 = "NULL";

            return "(" + value1 + "<=" + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_IsNull(string value1)
        {
            return "(" + value1 + " IS NULL)";
        }
    }
}