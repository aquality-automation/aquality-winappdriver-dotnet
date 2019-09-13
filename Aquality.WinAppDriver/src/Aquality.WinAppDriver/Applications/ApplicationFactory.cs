using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public abstract class ApplicationFactory : IApplicationFactory
    {
        private readonly LocalizationLogger localizationLogger;

        protected ApplicationFactory(LocalizationLogger localizationLogger)
        {
            this.localizationLogger = localizationLogger;
        }

        public abstract Application Application { get; }

        protected WindowsDriver<WindowsElement> GetDriver(Uri driverServerUri, AppiumOptions options, TimeSpan commandTimeout)
        {
            options.ToDictionary().TryGetValue("app", out var appPath);
            localizationLogger.Info("loc.application.start", appPath);
            return new WindowsDriver<WindowsElement>(driverServerUri, options, commandTimeout);
        }
    }
}
