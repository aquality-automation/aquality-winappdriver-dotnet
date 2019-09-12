using Aquality.Selenium.Core.Configurations;
using System;
using Microsoft.Extensions.DependencyInjection;
using Aquality.Selenium.Core.Localization;
using System.IO;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Elements;
using Aquality.Selenium.Core.Applications;

namespace Aquality.WinAppDriver.Applications
{
    public class ApplicationManager : ApplicationManager<ApplicationManager, Application>
    {
        public static Application Application => GetApplication(StartApplicationFunction, () => RegisterServices(services => Application));

        public static IServiceProvider ServiceProvider => GetServiceProvider(services => Application, () => RegisterServices(services => Application));

        public static T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        private static IServiceCollection RegisterServices(Func<IServiceProvider, Application> applicationSupplier)
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            var settings = startup.GetSettings();
            /*string settingsFile = null;  todo: add embedded resource Resources/settings.json and then startup.GetSettings(); */
            //startup.ConfigureServices(services, applicationSupplier); //, settingsFile);
            /*services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreFactory, ElementFactory>();*/
            startup.ConfigureServices(services, applicationSupplier);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreElementFactory, ElementFactory>();
            return services;
        }

        private static Func<IServiceProvider, Application> StartApplicationFunction
        {
            get
            {
                return (services) =>
                {
                    // Call the local/remote ApplicationFactory here
                    var timeoutConfig = services.GetRequiredService<ITimeoutConfiguration>();
                    var locLogger = services.GetRequiredService<LocalizationLogger>();
                    // return new Application("get me from config", new Uri("metoo"), timeoutConfig, null, locLogger);
                    var pathToApp = Path.GetFullPath("./Resources/Applications/Day Maxi Calc.exe");
                    return new Application(pathToApp, new Uri("http://127.0.0.1:4723/"),  timeoutConfig, null, locLogger);
                };
            }
        }
    }
}
