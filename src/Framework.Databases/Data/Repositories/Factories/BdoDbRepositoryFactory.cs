﻿using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using System;

namespace BindOpen.Framework.Application.Repositories
{
    /// <summary>
    /// This class represents a repository factory.
    /// </summary>
    public static class BdoDbRepositoryFactory
    {
        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <param name="initializer"></param>
        /// <param name="log"></param>
        /// <returns>Returns the log of the operation.</returns>
        public static T Create<T>(
            this IBdoScope scope,
            Func<IBdoScope, IBdoLog, IBdoConnector> initializer,
            IBdoLog log = null)
            where T : IBdoDbRepository, new()
        {
            var repo = new T();

            var subLog = new BdoLog();
            repo.SetConnector(initializer?.Invoke(scope, subLog));
            subLog.AddEventsTo(log);


            if (subLog.HasErrorsOrExceptions())
            {
                return default;
            }

            return repo;
        }
    }
}
