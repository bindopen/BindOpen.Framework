﻿using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This interface defines the connection service.
    /// </summary>
    public interface IConnectionService : IDataItem
    {
        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <param name="connector"></param>
        /// <returns>The log of the operation.</returns>
        ILog Close(IConnection connector);
        //Connection GetConnection(string name);
        //bool IsOpened(string name);

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(IConnectorConfiguration configuration, ILog log = null) where T : IConnection, new();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(IDataSource dataSource, string connectorDefinitionUniqueId, ILog log = null) where T : IConnection, new();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSourceService"></param>
        /// <param name="dataSourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(IDataSourceDepot dataSourceService, string dataSourceName, string connectorDefinitionUniqueId, ILog log = null) where T : IConnection, new();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(string dataSourceName, string connectorDefinitionUniqueId, ILog log = null) where T : IConnection, new();
    }
}