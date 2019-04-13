﻿using System;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Hosts.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the application host.
    /// </summary>
    public interface IBdoAppHost : IBdoAppService
    {
        // Execution ---------------------------------

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        IBdoAppHost Configure(Action<IAppHostOptions> setupOptions);

        /// <summary>
        /// The application settings.
        /// </summary>
        IBdoAppSettings Settings { get; }

        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param name="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(ApplicationPathKind pathKind);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the application host to consider.</returns>
        new IBdoAppHost Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the application host to consider.</returns>
        new IBdoAppHost End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

        // Settings ----------------------------------

        /// <summary>
        /// The set of user settings.
        /// </summary>
        IDataElementSet UserSettingsSet { get; set; }

        /// <summary>
        /// Gets the specified credential.
        /// </summary>
        /// <param name="name">The name of the credential to consider.</param>
        /// <returns>Returns the specified credential.</returns>
        IApplicationCredential GetCredential(string name);

        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="log">The log of the option.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider.</param>
        /// <returns>Returns the application settings.</returns>
        IBdoAppSettings LoadSettings(string filePath, ILog log, IAppScope appScope = null, XmlSchemaSet xmlSchemaSet = null);

        /// <summary>
        /// Saves settings.
        /// </summary>
        void SaveSettings();
    }
}