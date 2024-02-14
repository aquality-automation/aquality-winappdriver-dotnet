using System;
using Microsoft.Extensions.DependencyInjection;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Applications;
using System.Threading;
using Aquality.WinAppDriver.Configurations;
using OpenQA.Selenium.Appium.Service;
using Aquality.Selenium.Core.Logging;
using OpenQA.Selenium.Appium.Windows;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Utilities;
using Aquality.WinAppDriver.Actions;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Controls application and Aquality services
    /// </summary>
    public class AqualityServices : AqualityServices<IWindowsApplication>
    {
        private static readonly ThreadLocal<ApplicationStartup> ApplicationStartupContainer = new ThreadLocal<ApplicationStartup>(() => new ApplicationStartup());
        private static readonly ThreadLocal<IApplicationFactory> ApplicationFactoryContainer = new ThreadLocal<IApplicationFactory>();
        private static readonly ThreadLocal<AppiumLocalService> AppiumLocalServiceContainer = new ThreadLocal<AppiumLocalService>(AppiumLocalService.BuildDefaultService);

        /// <summary>
        /// Check if application already started.
        /// </summary>
        /// <value>true if application started and false otherwise.</value>
        public new static bool IsApplicationStarted => IsApplicationStarted();

        /// <summary>
        /// Gets registered instance of localized logger
        /// </summary>
        public static ILocalizedLogger LocalizedLogger => Get<ILocalizedLogger>();

        /// <summary>
        /// Gets registered instance of Logger
        /// </summary>
        public static Logger Logger => Get<Logger>();

        /// <summary>
        /// Gets ConditionalWait object
        /// </summary>
        public static IConditionalWait ConditionalWait => Get<IConditionalWait>();

        /// <summary>
        /// Gets KeyboardActions object
        /// </summary>
        public static IKeyboardActions KeyboardActions => Get<IKeyboardActions>();

        /// <summary>
        /// Gets MouseActions object
        /// </summary>
        public static IMouseActions MouseActions => Get<IMouseActions>();

        /// <summary>
        /// Gets ProcessManager object
        /// </summary>
        public static IProcessManager ProcessManager => Get<IProcessManager>();

        /// <summary>
        /// Stops appium local service.
        /// </summary>
        /// <returns>True if service was running, false otherwise</returns>
        public static bool TryToStopAppiumLocalService()
        {
            if(AppiumLocalServiceContainer.IsValueCreated && AppiumLocalServiceContainer.Value.IsRunning)
            {
                Get<ILocalizedLogger>().Info("loc.application.driver.service.local.stop");
                AppiumLocalServiceContainer.Value.Dispose();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provides current instance of application
        /// </summary>
        public static IWindowsApplication Application
        {
            get => GetApplication(StartApplicationFunction, ConfigureServices);
            set => SetApplication(value);
        }        

        /// <summary>
        /// Method which allow user to override or add custom services.
        /// </summary>
        /// <param name="startup"><see cref="ApplicationStartup"/> object with custom or overriden services.</param>
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
        public static T Get<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Sets default factory responsible for application creation.
        /// RemoteApplicationFactory if value set in configuration and LocalApplicationFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            var appProfile = Get<IApplicationProfile>();
            IApplicationFactory applicationFactory;
            if (appProfile.IsRemote)
            {
                applicationFactory = new RemoteApplicationFactory(appProfile.RemoteConnectionUrl);
            }
            else
            {
                applicationFactory = new LocalApplicationFactory(AppiumLocalServiceContainer.Value);
            }

            ApplicationFactory = applicationFactory;
        }

        /// <summary>
        /// Sets window handle factory, which attaches to already running application by it's window handle
        /// </summary>
        /// <param name="getWindowHandleFunction">Function to get top window handle via RootSession of Application, see <see cref="Forms.Window.NativeWindowHandle"/>.
        /// window handle could be also achieved from process.MainWindowHandle;</param>
        public static void SetWindowHandleApplicationFactory(Func<WindowsDriver, string> getWindowHandleFunction)
        {
            var appProfile = Get<IApplicationProfile>();
            var serviceUri = appProfile.IsRemote ? appProfile.RemoteConnectionUrl : AppiumLocalServiceContainer.Value.ServiceUrl;
            ApplicationFactory = new WindowHandleApplicationFactory(serviceUri, getWindowHandleFunction);
        }

        private static IServiceProvider ServiceProvider => GetServiceProvider(services => Application, ConfigureServices);

        private static Func<IServiceProvider, IWindowsApplication> StartApplicationFunction
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
