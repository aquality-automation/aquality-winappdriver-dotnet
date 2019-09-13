﻿using Aquality.Selenium.Core.Configurations;
using System;
using Microsoft.Extensions.DependencyInjection;
using Aquality.Selenium.Core.Localization;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Elements;
using Aquality.Selenium.Core.Applications;
using System.Threading;
using Aquality.WinAppDriver.Configurations;
using OpenQA.Selenium.Appium.Service;
using Aquality.Selenium.Core.Logging;
using System.Reflection;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Controls application and Aquality services
    /// </summary>
    public class ApplicationManager : ApplicationManager<ApplicationManager, Application>
    {
        private static readonly ThreadLocal<IApplicationFactory> ApplicationFactoryContainer = new ThreadLocal<IApplicationFactory>();
        private static readonly ThreadLocal<AppiumLocalService> AppiumLocalServiceContainer = new ThreadLocal<AppiumLocalService>(AppiumLocalService.BuildDefaultService);

        /// <summary>
        /// Stops appium local service.
        /// </summary>
        /// <returns>True if service was running, false otherwise</returns>
        public static bool TryToStopAppiumLocalService()
        {
            if(AppiumLocalServiceContainer.IsValueCreated && AppiumLocalServiceContainer.Value.IsRunning)
            {
                GetRequiredService<LocalizationLogger>().Info("loc.application.driver.service.local.stop");
                AppiumLocalServiceContainer.Value.Dispose();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provides current instance of application
        /// </summary>
        public static Application Application => GetApplication(StartApplicationFunction, () => RegisterServices(services => Application));

        /// <summary>
        /// Provides access to Aquality services, registered in DI container.
        /// </summary>
        public static IServiceProvider ServiceProvider => GetServiceProvider(services => Application, () => RegisterServices(services => Application));

        /// <summary>
        /// Resolves required service from <see cref="ServiceProvider"/>
        /// </summary>
        /// <typeparam name="T">type of required service</typeparam>
        /// <exception cref="InvalidOperationException" Thrown if there is no service of type <see cref="T"/>.
        /// <returns></returns>
        public static T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Sets default factory responsible for application creation.
        /// RemoteApplicationFactory if value set in configuration and LocalApplicationFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            var appProfile = GetRequiredService<IApplicationProfile>();
            var driverSettings = GetRequiredService<IDriverSettings>();
            var localizationLogger = GetRequiredService<LocalizationLogger>();
            var timeoutConfiguration = GetRequiredService<ITimeoutConfiguration>();
            
            IApplicationFactory applicationFactory;
            if (appProfile.IsRemote)
            {
                applicationFactory = new RemoteApplicationFactory(appProfile.RemoteConnectionUrl, driverSettings, timeoutConfiguration, localizationLogger);
            }
            else
            {
                applicationFactory = new LocalApplicationFactory(AppiumLocalServiceContainer.Value, driverSettings, timeoutConfiguration, localizationLogger);
            }

            SetFactory(applicationFactory);
        }

        /// <summary>
        /// Sets custom application factory.
        /// </summary>
        /// <param name="browserFactory">Custom implementation of <see cref="IApplicationFactory"/></param>
        public static void SetFactory(IApplicationFactory applicationFactory)
        {
            ApplicationFactoryContainer.Value = applicationFactory;
        }

        private static IServiceCollection RegisterServices(Func<IServiceProvider, Application> applicationSupplier)
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            var settingsFile = startup.GetSettings();
            startup.ConfigureServices(services, applicationSupplier, settingsFile);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreElementFactory, ElementFactory>();
            var driverSettings = new DriverSettings(settingsFile);
            services.AddSingleton<IDriverSettings>(driverSettings);
            services.AddSingleton<IApplicationProfile>(new ApplicationProfile(settingsFile, driverSettings));
            services.AddSingleton(new LocalizationManager(new LoggerConfiguration(settingsFile), Logger.Instance, Assembly.GetExecutingAssembly()));
            return services;
        }

        private static Func<IServiceProvider, Application> StartApplicationFunction
        {
            get
            {
                if (!ApplicationFactoryContainer.IsValueCreated)
                {
                    SetDefaultFactory();
                }
                return (services) => ApplicationFactoryContainer.Value.Application;
            }
        }
    }
}
