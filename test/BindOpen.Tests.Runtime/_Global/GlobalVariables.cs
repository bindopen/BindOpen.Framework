﻿using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Files;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Tests.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace BindOpen.Tests.Core
{
    public static class GlobalVariables
    {
        static string _workingFolder = null;
        static IBdoHost _appHost = null;
        static IConfiguration _netCoreConfiguration;

        public static string WorkingFolder
        {
            get
            {
                string workingFolder = GlobalVariables._workingFolder;
                if (workingFolder == null)
                    GlobalVariables._workingFolder = workingFolder =
                        ((_appHost?.GetKnownPath(BdoHostPathKind.RuntimeFolder) ?? AppDomain.CurrentDomain.BaseDirectory).EndingWith(@"\") + @"bdo\temp\").ToPath();

                return workingFolder;
            }
        }

        public static IBdoHost AppHost
        {
            get
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                        .AddConsole();
                });

                return _appHost ??= BdoHostFactory.CreateBindOpenHost<TestAppSettings>(
                    options => options
                        .SetModule("app.test")
                        //.SetRootFolder(q => q.HostSettings.Environment != "Development", @"..\..\..")
                        //.SetRootFolder(q => q.HostSettings.Environment == "Development", @"..\..")
                        .SetLogger(p => p.AddTrace(), true)//.AddFile(options))
                        .ThrowExceptionOnStartFailure()
                        .SetLogger(loggerFactory));
            }
        }

        public static IConfiguration NetCoreConfiguration
        {
            get
            {
                if (_netCoreConfiguration != null)
                {
                    return _netCoreConfiguration;
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppHost?.GetKnownPath(BdoHostPathKind.RootFolder))
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                builder.AddEnvironmentVariables();
                return _netCoreConfiguration = builder.Build();
            }
        }
    }

}
