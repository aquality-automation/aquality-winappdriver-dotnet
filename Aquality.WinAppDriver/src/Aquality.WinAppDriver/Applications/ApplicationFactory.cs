using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Configurations;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public abstract class ApplicationFactory : IApplicationFactory
    {
        private readonly IDriverSettings driverSettings;
        private readonly ITimeoutConfiguration timeoutConfiguration;

        protected ILocalizedLogger LocalizedLogger { get; }
        protected IServiceProvider ServiceProvider { get; }

        protected ApplicationFactory(IServiceProvider serviceProvider)
        {
            LocalizedLogger = serviceProvider.GetRequiredService<ILocalizedLogger>();
            driverSettings = serviceProvider.GetRequiredService<IDriverSettings>();
            timeoutConfiguration = serviceProvider.GetRequiredService<ITimeoutConfiguration>();
            ServiceProvider = serviceProvider;
        }

        public abstract Application Application { get; }

        protected WindowsDriver<WindowsElement> GetDriver(Uri driverServerUri)
        {
            var options = driverSettings.AppiumOptions;
            options.ToDictionary().TryGetValue("app", out var appPath);
            LocalizedLogger.Info("loc.application.start", appPath);
            return new WindowsDriver<WindowsElement>(driverServerUri, options, timeoutConfiguration.Command);
        }
    }
}
