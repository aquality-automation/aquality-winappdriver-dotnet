using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Configurations;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public abstract class ApplicationFactory : IApplicationFactory
    {
        private readonly ITimeoutConfiguration timeoutConfiguration;

        protected ILocalizedLogger LocalizedLogger { get; }
        protected IDriverSettings DriverSettings { get; }

        protected ApplicationFactory()
        {
            LocalizedLogger = AqualityServices.LocalizedLogger;
            DriverSettings = AqualityServices.Get<IDriverSettings>();
            timeoutConfiguration = AqualityServices.Get<ITimeoutConfiguration>();
        }

        public abstract IWindowsApplication Application { get; }

        protected virtual IWindowsApplication GetApplication(Uri driverServerUri)
        {
            return new Application(() => GetApplicationSession(driverServerUri), () => GetRootSession(driverServerUri));
        }

        protected virtual WindowsDriver<WindowsElement> GetApplicationSession(Uri driverServerUri)
        {
            var options = DriverSettings.AppiumOptions;
            options.ToDictionary().TryGetValue("app", out var appPath);
            LocalizedLogger.Info("loc.application.start", appPath);
            return CreateSession(driverServerUri, options);
        }

        protected virtual WindowsDriver<WindowsElement> GetRootSession(Uri driverServerUri)
        {
            var options = DriverSettings.AppiumOptions; 
            options.AddAdditionalCapability("app", "Root");
            return CreateSession(driverServerUri, options);
        }

        protected virtual WindowsDriver<WindowsElement> CreateSession(Uri driverServerUri, AppiumOptions appliumOptions)
        {
            return new WindowsDriver<WindowsElement>(driverServerUri, appliumOptions, timeoutConfiguration.Command);
        }
    }
}
