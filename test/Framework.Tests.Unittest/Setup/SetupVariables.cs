﻿using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Connectors;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Setup
{
    public static class SetupVariables
    {
        static String _WorkingFolder = null;
        static RuntimeAppScope _AppScope = null;
        static DataSourceService _DataSourceService = null;

        public static String WorkingFolder
        {
            get
            {
                String workingFolder = SetupVariables._WorkingFolder;
                if (workingFolder == null)
                    SetupVariables._WorkingFolder = workingFolder = (AppDomain.CurrentDomain.BaseDirectory.GetEndedString(@"\") + @"temp\").ToPath();

                return workingFolder;
            }
        }

        public static RuntimeAppScope AppScope
        {
            get
            {
                RuntimeAppScope appScope = SetupVariables._AppScope;
                if (appScope == null)
                {
                    appScope = new RuntimeAppScope(AppDomain.CurrentDomain);
                    ILog log = appScope.AppExtension.AddLibraries(
                        new AppExtensionConfiguration(
                            new AppExtensionFilter("BindOpen.Framework.Standard"),
                            new AppExtensionFilter("BindOpen.Framework.Databases"),
                            new AppExtensionFilter("BindOpen.Framework.Databases.MSSqlServer")));
                    appScope.DataSourceService = SetupVariables.DataSourceService;
                    appScope.Update<RuntimeAppScope>();
                    SetupVariables._AppScope = appScope;

                    Assert.IsFalse(log.HasErrorsOrExceptions(), log.ToXml());
                }

                return appScope;
            }
        }

        public static DataSourceService DataSourceService
        {
            get
            {
                DataSourceService dataSourceService = SetupVariables._DataSourceService;
                if (dataSourceService == null)
                {
                    dataSourceService = new DataSourceService();
                    dataSourceService = new DataSourceService(
                        new DataSource("prd@ptf_central_db", DataSourceKind.Database,
                            new ConnectorConfiguration(
                                null,
                                DatabaseConnectorKind.MSSqlServer.GetUniqueName(),
                                @"<Enter.Connection.String.Here>")
                        )
                    );
                    SetupVariables._DataSourceService = dataSourceService;
                }

                return dataSourceService;
            }
        }

    }

}
