﻿using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_MSSqlServer class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder_MSSqlServer(
            IBdoScope scope = null)
            : base(scope)
        {
            Id = Id = "databases.mssqlserver$client";
        }

        #endregion

        // ------------------------------------------
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

        private string GetSqlText_Field(
            DbField field,
            IDataElementSet parameterSet,
            IBdoLog log,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            IBdoScriptVariableSet scriptVariableSet = null,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null)
        {
            string queryString = "";

            if (field != null)
            {
                switch (viewMode)
                {
                    case DbDataFieldViewMode.CompleteName:
                    case DbDataFieldViewMode.CompleteNameOrValue:
                    case DbDataFieldViewMode.CompleteNameAsAlias:
                        if (field.IsAll)
                        {
                            string tableName = GetSqlText_Table(
                                field.DataModule,
                                field.Schema,
                                field.DataTable,
                                field.DataTableAlias,
                                viewMode,
                                defaultDataModule,
                                defaultSchema);

                            queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                            queryString += "*";
                        }
                        else
                        {
                            if ((viewMode != DbDataFieldViewMode.CompleteNameOrValue) || (field.Value == null))
                            {
                                string tableName = GetSqlText_Table(
                                    field.DataModule,
                                    field.Schema,
                                    field.DataTable ?? defaultDataTable,
                                    field.DataTableAlias,
                                    viewMode,
                                    defaultDataModule,
                                    defaultSchema);

                                queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                                queryString += GetSqlText_Field(
                                    field,
                                    parameterSet,
                                    log,
                                    viewMode == DbDataFieldViewMode.CompleteNameAsAlias ?
                                        DbDataFieldViewMode.OnlyNameAsAlias :
                                        DbDataFieldViewMode.OnlyName,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);
                            }
                            else
                            {
                                queryString += GetSqlText_Field(
                                    field,
                                    parameterSet,
                                    log,
                                    DbDataFieldViewMode.OnlyValue,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);

                                if (viewMode == DbDataFieldViewMode.CompleteNameAsAlias)
                                {
                                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));
                                }
                            }
                        }
                        break;
                    case DbDataFieldViewMode.OnlyName:
                        if (!string.IsNullOrEmpty(field.Alias))
                        {
                            queryString += GetSqlText_Field(field.Alias);
                        }
                        else if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        break;
                    case DbDataFieldViewMode.OnlyNameAsAlias:
                        if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));

                        break;
                    case DbDataFieldViewMode.OnlyValue:
                        string value = _scope?.Interpreter.Interprete(field.Value, scriptVariableSet, log) ?? "";

                        if (field.Query != null)
                        {
                            string subQueryText = "";
                            if (field.Query is BasicDbQuery)
                                subQueryText = GetSqlText(field.Query as BasicDbQuery, parameterSet, scriptVariableSet, log);
                            else if (field.Query is AdvancedDbQuery)
                                subQueryText = GetSqlText(field.Query as AdvancedDbQuery, parameterSet, scriptVariableSet, log);

                            queryString += "(" + subQueryText + ")";
                        }
                        else
                        {
                            queryString += GetSqlText_Value(value, field.ValueType);
                        }
                        break;
                    case DbDataFieldViewMode.NameEqualsValue:
                        queryString += GetSqlText_Field(
                            field,
                            parameterSet,
                            log,
                            DbDataFieldViewMode.CompleteName,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        queryString += "=";

                        queryString += GetSqlText_Field(
                            field,
                            parameterSet,
                            log,
                            DbDataFieldViewMode.OnlyValue,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        break;
                    default:
                        break;
                }
            }

            return queryString;
        }

        private string GetSqlText_Table(
            DbTable table,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            if (table != null)
            {
                return GetSqlText_Table(
                   table.DataModule,
                   table.Schema,
                   table.Name,
                   table.Alias,
                   viewMode,
                   defaultDataModule,
                   defaultSchema);
            }

            return "";
        }

        private string GetSqlText_Table(
            string tableDataModule,
            string tableSchema,
            string tableName,
            string tableAlias,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            string queryString = "";

            if ((viewMode == DbDataFieldViewMode.CompleteName) && (!string.IsNullOrEmpty(tableAlias)))
            {
                queryString += GetSqlText_Table(tableAlias);
            }
            else if (!string.IsNullOrEmpty(tableName))
            {
                if ((viewMode == DbDataFieldViewMode.CompleteName) || (viewMode == DbDataFieldViewMode.CompleteNameAsAlias))
                {

                    if (string.IsNullOrEmpty(tableDataModule))
                        tableDataModule = defaultDataModule;
                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        tableDataModule = GetDatabaseName(tableDataModule);
                    }

                    if (string.IsNullOrEmpty(tableSchema))
                    {
                        tableSchema = defaultSchema;
                    }
                    queryString += DbFluent.Table(tableName, tableSchema, tableDataModule);
                }
                else
                {
                    queryString += GetSqlText_Table(tableName);
                }

                if (viewMode == DbDataFieldViewMode.CompleteNameAsAlias)
                {
                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableAlias), " as " + GetSqlText_Table(tableAlias));
                }
            }

            return queryString;
        }

        private string GetSqlText_From(
            IDbQueryFromStatement queryFrom,
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoLog log)
        {
            string queryString = "";
            foreach (DbQueryJoinStatement queryJoin in queryFrom.JoinStatements)
            {
                switch (queryJoin.Kind)
                {
                    case DbQueryJoinKind.Inner:
                        {
                            queryString += " inner join ";
                            break;
                        }
                    case DbQueryJoinKind.Left:
                        {
                            queryString += " left join ";
                            break;
                        }
                    case DbQueryJoinKind.Right:
                        {
                            queryString += " right join ";
                            break;
                        }
                    case DbQueryJoinKind.Union:
                        {
                            queryString += " inner join ";
                            break;
                        }
                }

                if (queryJoin.Query != null)
                {
                    string subQuery = BuildQuery(queryJoin.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                    queryString += "(" + subQuery + ")";
                    if (!string.IsNullOrEmpty(queryJoin.Query.Alias))
                    {
                        queryString += " as " + queryJoin.Query.Alias;
                    }
                }
                else
                {
                    if (queryJoin.Table == null || (string.IsNullOrEmpty(queryJoin.Table.Name) && string.IsNullOrEmpty(queryJoin.Table.Alias)))
                    {
                        queryJoin.Table = DbFluent.Table(query?.DataTable).WithAlias(query?.DataTableAlias);
                    }
                    queryString += GetSqlText_Table(queryJoin.Table, DbDataFieldViewMode.CompleteNameAsAlias);
                }

                if (queryJoin.Kind != DbQueryJoinKind.None)
                {
                    queryString += " on ";
                    string expression = _scope?.Interpreter.Interprete(queryJoin.Condition, scriptVariableSet, log) ?? String.Empty;
                    queryString += expression;
                }
            }
            if (queryFrom.UnionStatement?.Query != null)
            {
                switch (queryFrom.UnionStatement.Type)
                {
                    case DbQueryUnionKind.Union:
                        {
                            queryString += " union ";
                            break;
                        }
                }
                string subQuery = BuildQuery(queryFrom.UnionStatement.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                queryString += "(" + subQuery + ")";
                queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(queryFrom.UnionStatement.Query.Alias), " as " + queryFrom.UnionStatement.Query.Alias);
            }

            return queryString;
        }

        #endregion
    }
}