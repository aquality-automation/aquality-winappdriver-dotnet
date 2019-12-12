using Aquality.Selenium.Core.Configurations;
using System;
using Microsoft.Extensions.DependencyInjection;
using Aquality.Selenium.Core.Localization;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Elements;
using Aquality.Selenium.Core.Applications;
using Aquality.WinAppDriver.Configurations;
using Aquality.Selenium.Core.Logging;
using System.Reflection;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Utilities;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Provides functionality to  register services
    /// </summary>
    public class ApplicationStartup : Startup
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider,
            ISettingsFile settings = null)
        {
            settings = settings ?? GetSettings();
            base.ConfigureServices(services, applicationProvider, settings);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreElementFactory, ElementFactory>();
            services.AddSingleton<IDriverSettings>(serviceProvider => new DriverSettings(settings));
            services.AddSingleton<IApplicationProfile>(serviceProvider => new ApplicationProfile(settings, serviceProvider.GetRequiredService<IDriverSettings>()));
            services.AddSingleton<ILocalizationManager>(serviceProvider => new LocalizationManager(serviceProvider.GetRequiredService<ILoggerConfiguration>(), serviceProvider.GetRequiredService<Logger>(), Assembly.GetExecutingAssembly()));
            services.AddSingleton<IKeyboardActions>(serviceProvider => new KeyboardActions(serviceProvider.GetRequiredService<ILocalizedLogger>(), () => ApplicationManager.Application.Driver));
            services.AddSingleton<IMouseActions>(serviceProvider => new MouseActions(serviceProvider.GetRequiredService<ILocalizedLogger>(), () => ApplicationManager.Application.Driver));
            services.AddTransient(serviceProvider => ApplicationManager.ApplicationFactory);
            services.AddTransient<IProcessManager, ProcessManager>();
            services.AddTransient<IWinAppDriverLauncher, WinAppDriverLauncher>();
            return services;
        }
    }
}
