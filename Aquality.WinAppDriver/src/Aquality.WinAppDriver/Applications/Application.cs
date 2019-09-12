using System;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Logging;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Aquality.WinAppDriver.Applications
{
    public class Application : IApplication
    {
        private TimeSpan implicitWait;

        public Application(string application, Uri driverServer, ITimeoutConfiguration timeoutConfiguration, AppiumOptions appiumOptions, LocalizationLogger logger)
        {
            Logger = logger;
            logger.Info("loc.application.start");
            var options = appiumOptions ?? new AppiumOptions();
            options.AddAdditionalCapability("app", application);
            Driver = new WindowsDriver<WindowsElement>(driverServer, options, timeoutConfiguration.Command);
            Driver.Manage().Timeouts().ImplicitWait = timeoutConfiguration.Implicit;
        }

        private LocalizationLogger Logger { get; }

        public RemoteWebDriver Driver { get; }

        public void SetImplicitWaitTimeout(TimeSpan timeout)
        {
            if (timeout != implicitWait)
            {
                Driver.Manage().Timeouts().ImplicitWait = timeout;
                implicitWait = timeout;
            }
        }
    }
}
