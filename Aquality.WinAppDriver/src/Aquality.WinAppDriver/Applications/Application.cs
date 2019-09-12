using System;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Provides functionality to work with Windows application via WinAppDriver.  
    /// </summary>
    public class Application : IApplication
    {
        private TimeSpan implicitWait;

        /// <summary>
        /// Instantiate application.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="driverServer"></param>
        /// <param name="timeoutConfiguration"></param>
        /// <param name="appiumOptions"></param>
        /// <param name="logger"></param>
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
        
        /// <summary>
        /// Sets WinAppDriver ImplicitWait timeout. 
        /// Default value: <see cref="ITimeoutConfiguration.Implicit"/>.
        /// </summary>
        /// <param name="timeout">Desired Implicit wait timeout.</param>
        public void SetImplicitWaitTimeout(TimeSpan timeout)
        {
            if (timeout != implicitWait)
            {
                Driver.Manage().Timeouts().ImplicitWait = timeout;
                implicitWait = timeout;
            }
        }

        /// <summary>
        /// Quit application.
        /// </summary>
        public void Quit()
        {
            Logger.Info("loc.application.driver.quit");
            Driver?.Quit();
        }
    }
}
