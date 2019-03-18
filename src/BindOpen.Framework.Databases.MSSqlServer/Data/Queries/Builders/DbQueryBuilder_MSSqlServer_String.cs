﻿using System;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Text(string value1)
        {
            return "'" + ScriptParsingHelper.GetValueFromText(value1).Replace("'", "''") + "'";
        }

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Like(string value1, string value2)
        {
            return "(" + value1 + " LIKE " + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Replace(string value1, string value2, string value3)
        {
            return "replace(" + value1 + ", " + value2 + ", " + value3 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLCONCATENATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Concatenate(object[] parameters)
        {
            string text = "";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += (text == String.Empty ? st : " + " + st);
                }
            }

            return text;
        }
    }
}