﻿using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries.ApiScript
{
    /// <summary>
    /// This class represents the database data query extension.
    /// </summary>
    public static partial class DbDataQueryExtension
    {
        private static string GetScriptFunction(DataOperator aOperator)
        {
            switch (aOperator)
            {
                case DataOperator.ListDeclaration:
                    return "$sqlList";
                case DataOperator.Contains:
                    return "$sqlLike";
                case DataOperator.Different:
                    return "$sqlDiff";
                case DataOperator.Equal:
                    return "$sqlEq";
                case DataOperator.Greater:
                    return "$sqlGt";
                case DataOperator.GreaterOrEqual:
                    return "$sqlGte";
                case DataOperator.Has:
                    return "";
                case DataOperator.In:
                    return "$sqlIn";
                case DataOperator.Lesser:
                    return "$sqlLt";
                case DataOperator.LesserOrEqual:
                    return "$sqlLte";
            }

            return null;
        }

        /// <summary>
        /// Converts the specifed search query into an extension script.
        /// </summary>
        /// <param name="searchQuery">The search query to consider.</param>
        /// <param name="log">The </param>
        /// <param name="definition">The clause statement to consider.</param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ConvertToXtensionScript(
            this string searchQuery,
            ILog log = null,
            ApiScriptFilteringDefinition definition = null,
            int i = 0)
        {
            string script = searchQuery;
            if (!string.IsNullOrEmpty(script))
            {
                // boolean instructions

                foreach (string instruction in new string[] { "Or", "And", "Not" })
                {
                    int j = i;
                    List<string> clauses = new List<string>();
                    script.GetIndexOfNextString(" " + instruction + " ", ref j);
                    while (j < script.Length - 1)
                    {
                        string clause = script.Substring(i, j - i + 1);
                        clause = clause.ConvertToXtensionScript(log, definition, 0);
                        clauses.Add(clause);
                        j = i = j + (" " + instruction + " ").Length;
                        script.GetIndexOfNextString(" " + instruction + " ", ref j);
                        if (j == script.Length)
                        {
                            clause = script.Substring(i);
                            clause = clause.ConvertToXtensionScript(log, definition, 0);
                            clauses.Add(clause);
                        }
                    }
                    if (clauses.Count > 0)
                    {
                        script = "$sql" + instruction + "(" + clauses.Aggregate((p, q) => p + "," + q) + ")";
                    }
                }

                if (i == 0)
                {
                    DataOperator aOperator = DataOperator.None;

                    int k = script.Length;
                    string scriptOperator = null;
                    foreach (DataOperator currentOperator in new DataOperator[] {
                        DataOperator.ListDeclaration,
                        DataOperator.Contains, DataOperator.Different, DataOperator.Equal,
                        DataOperator.GreaterOrEqual, DataOperator.Greater,
                        DataOperator.LesserOrEqual, DataOperator.Lesser,
                        DataOperator.Has, DataOperator.In })
                    {
                        int k1 = 0;
                        string currentScriptOperator = DbDataQueryExtension.GetInstruction(currentOperator);
                        script.GetIndexOfNextString(currentScriptOperator, ref k1);
                        if (k1 < k)
                        {
                            scriptOperator = currentScriptOperator;
                            aOperator = currentOperator;
                            k = k1;
                        }
                    }
                    if (k == script.Length)
                    {
                        log.AddError("No operator found in clause '" + searchQuery + "'", resultCode: "user");
                    }
                    else
                    {
                        string scriptFunction = DbDataQueryExtension.GetScriptFunction(aOperator)?.Trim();
                        string fieldName = script.Substring(0, k)?.Trim();
                        string value = script.Substring(k + scriptOperator.Length)?.Trim();

                        if (value.Length > 2 && value.StartsWith("'") && value.EndsWith("'"))
                            value = "$sqlText('" + value.Substring(1, value.Length - 2) + "')";

                        // check that the field is in the dictionary
                        if (!definition.ContainsKey(fieldName))
                        {
                            log.AddError("Undefined field '" + fieldName + "' in clause '" + searchQuery + "''", resultCode: "user");
                        }
                        else
                        {
                            ApiScriptClause clause = definition?[fieldName];

                            // check the instruction found corresponds to the definition in dictionary
                            if (!clause.Operators.Any(p => p == aOperator))
                            {
                                log.AddError("Undefined operator '" + aOperator.ToString() + "' for field '" + fieldName + "'", resultCode: "user");
                            }
                            else
                            {
                                if (clause.Field == null) clause.Field = new DbField(fieldName);

                                if (aOperator == DataOperator.Has)
                                {
                                    if (value.Length > 2 && value.StartsWith("{") && value.EndsWith("}"))
                                        value = value.Substring(1, value.Length - 2);
                                    value = value.ConvertToXtensionScript(log, clause.ValueDefinition, 0);
                                    script = "(" + value + ")";
                                }
                                else
                                {
                                    script = scriptFunction + "($"
                                        + (!string.IsNullOrEmpty(clause.Field.DataModule) ? "sqlDataModule('" + clause.Field.DataModule + "')." : "")
                                        + (!string.IsNullOrEmpty(clause.Field.Schema) ? "sqlSchema('" + clause.Field.Schema + "')." : "")
                                        + (!string.IsNullOrEmpty(clause.Field.DataTableAlias) ? "sqlTable('" + clause.Field.DataTableAlias + "')." :
                                            !string.IsNullOrEmpty(clause.Field.DataTable) ? "sqlTable('" + clause.Field.DataTable + "')." : "")
                                        + (!string.IsNullOrEmpty(clause.Field.Alias) ? "sqlField('" + clause.Field.Alias + "')" :
                                            !string.IsNullOrEmpty(clause.Field.Name) ? "sqlField('" + clause.Field.Name + "')" : "")
                                        + "," + value + ")";
                                }
                            }
                        }
                    }
                }
            }
            return script;
        }

        private static string GetInstruction(DataOperator aOperator)
        {
            switch (aOperator)
            {
                case DataOperator.ListDeclaration:
                    return "[";
                case DataOperator.Contains:
                    return "constains";
                case DataOperator.Different:
                    return "!=";
                case DataOperator.Equal:
                    return "=";
                case DataOperator.Greater:
                    return ">";
                case DataOperator.GreaterOrEqual:
                    return ">=";
                case DataOperator.Has:
                    return "has";
                case DataOperator.In:
                    return "in";
                case DataOperator.Lesser:
                    return "<";
                case DataOperator.LesserOrEqual:
                    return "<=";
            }

            return null;
        }

        /// <summary>
        /// Adds filters to the specified database query considering the specified filter query string.
        /// </summary>
        /// <param name="dbQuery">The database query to consider.</param>
        /// <param name="filterQuery">The filter query string to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="definition">The clause statement to consider.</param>
        /// <returns>The built query.</returns>
        public static IAdvancedDbDataQuery Filter(
            this IAdvancedDbDataQuery dbQuery,
            string filterQuery,
            ILog log = null,
            ApiScriptFilteringDefinition definition = null)
        {
            log = log ?? new Log();

            if (dbQuery != null && !string.IsNullOrEmpty(filterQuery))
            {
                string scriptText = filterQuery.ConvertToXtensionScript(log, definition);

                if (scriptText?.Length > 0)
                {
                    if (!string.IsNullOrEmpty(dbQuery.WhereClause?.Text))
                    {
                        dbQuery.WhereClause.Text = "$sqlAnd(" + dbQuery.WhereClause.Text + "," + scriptText + ")";
                    }
                    else
                    {
                        dbQuery.WhereClause = scriptText.CreateScript();
                    }
                }
            }

            return dbQuery;
        }

        /// <summary>
        /// Sorts the specified query considering the specified query script.
        /// </summary>
        /// <param name="query">The database query to consider.</param>
        /// <param name="sortQuery">The sort query text to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <returns>The built query.</returns>
        public static IAdvancedDbDataQuery Sort(
            this IAdvancedDbDataQuery query,
            string sortQuery,
            ILog log = null,
            ApiScriptSortingDefinition definition = null)
        {
            log = log ?? new Log();

            if (query != null && !string.IsNullOrEmpty(sortQuery))
            {
                query.OrderByStatements = new List<IDbDataQueryOrderByStatement>();
                foreach (string fieldItem in sortQuery.Split(','))
                {
                    IDbDataQueryOrderByStatement statement = new DbDataQueryOrderByStatement();
                    var fieldItemParams = fieldItem?.Trim().Split(' ');
                    if (fieldItemParams.Length > 0)
                    {
                        string fieldName = fieldItemParams[0]?.Trim();
                        if (!definition.ContainsKey(fieldName))
                        {
                            log.AddError("Undefined field '" + fieldName + "' in order statement", resultCode: "user");
                        }
                        else
                        {
                            statement.Field = definition?[fieldName]?.Field ?? new DbField(fieldName);
                            statement.Sorting = DataSortingMode.Ascending;

                            if (fieldItemParams.Length > 1)
                            {
                                string direction = fieldItemParams[1]?.Trim();
                                if (string.Equals(direction, "desc"))
                                {
                                    statement.Sorting = DataSortingMode.Descending;
                                    query.OrderByStatements.Add(statement);
                                }
                                else if (!string.Equals(direction, "asc"))
                                {
                                    log.AddError("Invalid order direction '" + direction + "'", resultCode: "user");
                                }
                                else
                                {
                                    query.OrderByStatements.Add(statement);
                                }
                            }
                        }
                    }
                }
            }

            return query;
        }

        /// <summary>
        /// Builds the following query: Get the server instances.
        /// </summary>
        /// <param name="query">The database query to consider.</param>
        /// <param name="pageSize">The page size to consider.</param>
        /// <param name="pageToken">The page token text to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="clauseStatement">The clause statement to consider.</param>
        /// <returns>The built query.</returns>
        public static IAdvancedDbDataQuery Paginate(
            this IAdvancedDbDataQuery query,
            int? pageSize,
            string pageToken,
            ILog log = null,
            ApiScriptSortingDefinition clauseStatement = null)
        {
            log = log ?? new Log();
            return query;
        }
    }
}
