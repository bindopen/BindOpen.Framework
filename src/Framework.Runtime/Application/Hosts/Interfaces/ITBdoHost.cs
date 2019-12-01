﻿using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Options.Hosts;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using System;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the bot.
    /// </summary>
    public interface ITBdoHost<S> : ITBdoService<S>, IBdoHost
        where S : class, IBdoHostSettings, new()
    {
        // Execution ---------------------------------

        /// <summary>
        /// The options.
        /// </summary>
        ITBdoHostOptions<S> Options { get; set; }

        /// <summary>
        /// Configures the bot.
        /// </summary>
        /// <param name="setupOptions">The action to setup the bot.</param>
        /// <returns>Returns the bot.</returns>
        ITBdoHost<S> Configure(Action<ITBdoHostOptions<S>> setupOptions);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <returns>Returns the bot to consider.</returns>
        new ITBdoHost<S> Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the bot to consider.</returns>
        new ITBdoHost<S> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}