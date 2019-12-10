using Aquality.Selenium.Core.Configurations;
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
using Aquality.WinAppDriver.Actions;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Controls application and Aquality services
    /// </summary>
    public class ApplicationManager : ApplicationManager<Application>
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
                GetRequiredService<ILocalizedLogger>().Info("loc.application.driver.service.local.stop");
                AppiumLocalServiceContainer.Value.Dispose();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provides current instance of application
        /// </summary>
        public static Application Application
        {
            get
            {
                return GetApplication(StartApplicationFunction, () => RegisterServices(services => Application));
            }
            set
            {
                SetApplication(value);
            }
        }

        /// <summary>
        /// Provides access to Aquality services, registered in DI container.
        /// </summary>
        public static IServiceProvider ServiceProvider
        {
            get
            {
                return GetServiceProvider(services => Application, () => RegisterServices(services => Application));
            }
            set
            {
                SetServiceProvider(value);
            }
        }

        /// <summary>
        /// Factory for application creation.
        /// </summary>
        public static IApplicationFactory ApplicationFactory
        {
            get
            {
                if (!ApplicationFactoryContainer.IsValueCreated)
                {
                    SetDefaultFactory();
                }
                return ApplicationFactoryContainer.Value;
            }
            set
            {
                ApplicationFactoryContainer.Value = value;
            }
        }

        /// <summary>
        /// Resolves required service from <see cref="ServiceProvider"/>
        /// </summary>
        /// <typeparam name="T">type of required service</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if there is no service of the required type.</exception> 
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
            IApplicationFactory applicationFactory;
            if (appProfile.IsRemote)
            {
                applicationFactory = new RemoteApplicationFactory(appProfile.RemoteConnectionUrl, ServiceProvider);
            }
            else
            {
                applicationFactory = new LocalApplicationFactory(AppiumLocalServiceContainer.Value, ServiceProvider);
            }

            ApplicationFactory = applicationFactory;
        }

        private static IServiceCollection RegisterServices(Func<IServiceProvider, Application> applicationSupplier)
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            var settingsFile = startup.GetSettings();
            startup.ConfigureServices(services, applicationSupplier, settingsFile);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreElementFactory, ElementFactory>();
            services.AddSingleton<IDriverSettings>(serviceProvider => new DriverSettings(settingsFile));
            services.AddSingleton<IApplicationProfile>(serviceProvider => new ApplicationProfile(settingsFile, serviceProvider.GetRequiredService<IDriverSettings>()));
            services.AddSingleton<ILocalizationManager>(serviceProvider => new LocalizationManager(serviceProvider.GetRequiredService<ILoggerConfiguration>(), serviceProvider.GetRequiredService<Logger>(), Assembly.GetExecutingAssembly()));
            services.AddSingleton<IKeyboardActions>(serviceProvider => new KeyboardActions(serviceProvider.GetRequiredService<ILocalizedLogger>(), () => Application.Driver));
            services.AddSingleton<IMouseActions>(serviceProvider => new MouseActions(serviceProvider.GetRequiredService<ILocalizedLogger>(), () => Application.Driver));
            services.AddTransient(serviceProvider => ApplicationFactory);
            return services;
        }

        private static Func<IServiceProvider, Application> StartApplicationFunction
        {
            get
            {
                return (services) => ApplicationFactory.Application;
            }
        }
    }
}
