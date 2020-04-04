﻿using BindOpen.Application.Scopes;
using BindOpen.Application.Services;
using BindOpen.Application.Settings;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static class BdoAppServiceCollectionExtensions
    {
        // BindOpen host --------------------------

        /// <summary>
        /// Adds a BindOpen default service.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen(
            this IServiceCollection services,
            Action<ITBdoHostOptions<BdoDefaultAppSettings>> setupAction = null)
        {
            services.AddSingleton<IBdoHost>(_ => BdoHostFactory.CreateBindOpenDefaultHost(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="provider">The service provider to consider.</param>
        /// <returns></returns>
        public static IBdoHost GetBindOpenHost(this IServiceProvider provider)
        {
            return provider?.GetService<IBdoHost>();
        }

        /// <summary>
        /// Adds a BindOpen service.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen<S>(
            this IServiceCollection services,
            Action<ITBdoHostOptions<S>> setupAction = null)
            where S : class, IBdoAppSettings, new()
        {
            services.AddSingleton<IBdoHost>(_ => BdoHostFactory.CreateBindOpenHost<S>(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen service.
        /// </summary>
        /// <typeparam name="SHost">The class of bot to consider.</typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen<SHost, S>(
            this IServiceCollection services,
            Action<ITBdoHostOptions<S>> setupAction = null)
            where SHost : TBdoHost<S>, new()
            where S : class, IBdoAppSettings, new()
        {
            services.AddSingleton<IBdoHost, SHost>(_ => BdoHostFactory.CreateBindOpenHost<SHost, S>(setupAction));

            return services;
        }

        // BindOpen hosted services --------------------------

        /// <summary>
        /// Adds a BidnOpen hosted service as singleton.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen hosted service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddBdoSingletonService<TService, TImplementation>(
            this IServiceCollection services,
            Func<IBdoHost, TImplementation> setupAction)
            where TService : class, IBdoScoped
            where TImplementation : class, TService
            => services.AddBdoScopedService<TService, TImplementation>(ServiceLifetime.Singleton, setupAction);

        /// <summary>
        /// Adds a BidnOpen scoped service as scoped.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen hosted service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddBdoScopedService<TService, TImplementation>(
            this IServiceCollection services,
            Func<IBdoHost, TImplementation> setupAction)
            where TService : class, IBdoScoped
            where TImplementation : class, TService
            => services.AddBdoScopedService<TService, TImplementation>(ServiceLifetime.Scoped, setupAction);

        /// <summary>
        /// Adds a BidnOpen scoped service as transient.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen hosted service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddBdoTransientService<TService, TImplementation>(
            this IServiceCollection services,
            Func<IBdoHost, TImplementation> setupAction)
            where TService : class, IBdoScoped
            where TImplementation : class, TService
            => services.AddBdoScopedService<TService, TImplementation>(ServiceLifetime.Transient, setupAction);

        /// <summary>
        /// Adds a BidnOpen hosted service.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen hosted service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <param name="serviceLifetime">The service life time to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        private static IServiceCollection AddBdoScopedService<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            Func<IBdoHost, TImplementation> setupAction)
            where TService : class, IBdoScoped
            where TImplementation : class, TService
        {
            TImplementation initializer(IServiceProvider p)
            {
                var host = p.GetService<IBdoHost>();
                var repo = setupAction?.Invoke(host);

                return repo;
            }

            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TService, TImplementation>(initializer);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService, TImplementation>(initializer);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TService, TImplementation>(initializer);
                    break;
            }

            return services;
        }
    }
}