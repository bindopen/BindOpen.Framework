﻿using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using Microsoft.Extensions.Configuration;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDatasourceExtensions
    {
        /// <summary>
        /// Adds sources from host.
        /// </summary>
        /// <param name="depot">The datasource depot to consider.</param>
        /// <param name="config">The configuration to consider.</param>
        /// <param name="keyName">The key name to consider.</param>
        public static IBdoDatasourceDepot AddFromNetCoreConfiguration(this IBdoDatasourceDepot depot, IConfiguration config, string keyName = "connectionStrings")
        {
            if (depot != null && config != null)
            {
                var sections = config.GetSection(keyName).GetChildren();
                foreach (var section in sections)
                {
                    depot.Add(
                        ItemFactory.CreateDatasource(section.Key, DatasourceKind.Database)
                            .WithConfiguration(
                                new BdoConnectorConfiguration().WithConnectionString(section.Value) as BdoConnectorConfiguration)
                    );
                }
            };

            return depot;
        }
    }

}