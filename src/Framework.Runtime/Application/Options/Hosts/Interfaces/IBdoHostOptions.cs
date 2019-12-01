﻿using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Core.Extensions.Runtime.Stores;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Application.Options.Hosts
{
    /// <summary>
    /// The interface defines the base BDO host options.
    /// </summary>
    public interface IBdoHostOptions : IDataItem
    {
        // Modules ----------------------

        /// <summary>
        /// The application module.
        /// </summary>
        IAppModule ApplicationModule { get; }

        // Paths ----------------------

        /// <summary>
        /// The application folder path.
        /// </summary>
        string AppFolderPath { get; }

        /// <summary>
        /// The settings file path.
        /// </summary>
        string AppSettingsFilePath { get; }

        /// <summary>
        /// The runtime application folder path.
        /// </summary>
        string RuntimeFolderPath { get; }

        // Settings ----------------------

        /// <summary>
        /// The application settings.
        /// </summary>
        IBdoHostAppSettings AppSettings { get; set; }

        /// <summary>
        /// The host settings.
        /// </summary>
        IBdoHostSettings HostSettings { get; set; }

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        IDataElementSpecSet SettingsSpecificationSet { get; set; }

        // Extensions ----------------------

        /// <summary>
        /// The extension to load.
        /// </summary>
        List<IBdoExtensionReference> ExtensionReferences { get; set; }

        /// <summary>
        /// The extension load options.
        /// </summary>
        IExtensionLoadOptions ExtensionLoadOptions { get; }

        // Loggers ----------------------

        /// <summary>
        /// Indicates whether this instance uses a default logger.
        /// </summary>
        bool IsDefaultFileLoggerUsed { get; }

        /// <summary>
        /// The loggers.
        /// </summary>
        IList<IBdoLogger> Loggers { get; }
    }
}