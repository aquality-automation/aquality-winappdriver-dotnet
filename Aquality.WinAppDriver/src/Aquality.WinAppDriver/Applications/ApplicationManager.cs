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
using Aquality.WinAppDriver.Utilities;
using OpenQA.Selenium.Appium.Windows;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Controls application and Aquality services
    /// </summary>
    public class ApplicationManager : ApplicationManager<Application>
    {
        private static readonly ThreadLocal<ApplicationStartup> ApplicationStartupContainer = new ThreadLocal<ApplicationStartup>(() => new ApplicationStartup());
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
            get => GetApplication(StartApplicationFunction, ConfigureServices);
            set => SetApplication(value);
        }

        /// <summary>
        /// Provides access to Aquality services, registered in DI container.
        /// </summary>
        public static IServiceProvider ServiceProvider
        {
            get => GetServiceProvider(services => Application, ConfigureServices);
            set => SetServiceProvider(value);
        }

        /// <summary>
        /// Method which allow user to override or add custom services.
        /// </summary>
        /// <param name="startup"><see cref="ApplicationStartup"/>> object with custom or overriden services.</param>
        public static void SetStartup(ApplicationStartup startup)
        {
            if (startup != null)
            {
                ApplicationStartupContainer.Value = startup;
                SetServiceProvider(ConfigureServices().BuildServiceProvider());
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
            set => ApplicationFactoryContainer.Value = value;
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

        /// <summary>
        /// Sets window handle factory, which attaches to already running application by it's window handle
        /// </summary>
        /// <param name="getWindowHandleFunction">Function to get window handle via RootSession of Application</param>
        public static void SetWindowHandleApplicationFactory(Func<WindowsDriver<WindowsElement>, string> getWindowHandleFunction)
        {
            var appProfile = GetRequiredService<IApplicationProfile>();
            var serviceUri = appProfile.IsRemote ? appProfile.RemoteConnectionUrl : AppiumLocalServiceContainer.Value.ServiceUrl;
            ApplicationFactory = new WindowHandleApplicationFactory(serviceUri, ServiceProvider, getWindowHandleFunction);
        }

        private static Func<IServiceProvider, Application> StartApplicationFunction
        {
            get
            {
                return (services) => ApplicationFactory.Application;
            }
        }

        private static IServiceCollection ConfigureServices()
        {
            return ApplicationStartupContainer.Value.ConfigureServices(new ServiceCollection(), services => Application);
        }
    }
}
