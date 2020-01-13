﻿using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Connections;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.Extensions.Attributes;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using Npgsql;
using System.Data;

namespace BindOpen.Framework.Extensions.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [BdoConnector(Name = "databases.postgresql$client")]
    public class BdoDbConnector_PostgreSql : BdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_PostgreSql class.
        /// </summary>
        public BdoDbConnector_PostgreSql() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_PostgreSql class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        public BdoDbConnector_PostgreSql(
            string name, string connectionString = null) : base(name, connectionString)
        {
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public override IBdoDbConnector WithScope(IBdoScope scope)
        {
            QueryBuilder = new DbQueryBuilder_PostgreSql(scope);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public override IBdoDbConnection CreateConnection(IBdoLog log = null)
        {
            IBdoDbConnection connection = null;

            if (!Check<BdoDbConnector_PostgreSql>().AddEventsTo(log, p => p.HasErrorsOrExceptions()).HasErrorsOrExceptions())
            {
                var dbConnection = new NpgsqlConnection(ConnectionString);
                if (dbConnection != null)
                {
                    connection = new BdoDbConnection(this, dbConnection);
                }
            }

            return connection;
        }

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        public override IDbCommand CreateCommand(
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var command = new NpgsqlCommand(CreateCommandText(query, false, scriptVariableSet, log));

            if (query.ParameterSet != null)
            {
                foreach (var parameter in query.ParameterSet.Elements)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.GetObject());
                }
            }

            return command;
        }

        #endregion
    }
}