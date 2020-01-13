﻿using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFactory
    {
        // As parameter -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name to consider.</param>
        public static IDataElement UseParameter(
            this IDbQuery query,
            string name)
        {
            return query?.UseParameter(name, DataValueType.Any, null);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement UseParameter(
            this IDbQuery query,
            string name,
            object value)
        {
            return query?.UseParameter(name, DataValueType.Any, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement UseParameter(
            this IDbQuery query,
            string name,
            DataValueType valueType,
            object value)
        {
            if (query.ParameterSet == null)
            {
                query.ParameterSet = new DataElementSet();
            }

            DataElement parameter;
            if ((parameter = query.ParameterSet[name]) != null)
            {
                parameter.SetItem(value);
            }
            else
            {
                parameter = ElementFactory.CreateScalar(name, valueType, value);
                parameter.Index = query.ParameterSet.Count + 1;
                query.ParameterSet.Add(parameter);
            }

            return parameter;
        }

        /// <summary>
        /// Creates a parameter string from the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to consider.</param>
        /// <returns>Returns the string corresponding to the specified parameter.</returns>
        public static string CreateParameterString(this IDataElement parameter) => "@" + parameter?.Name ?? parameter.Index.ToString();

    }
}