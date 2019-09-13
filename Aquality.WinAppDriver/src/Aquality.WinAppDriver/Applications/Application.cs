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
        /// <param name="windowsDriver">Instance of WinAppDriver</param>
        /// <param name="timeoutConfiguration">Instance of <see cref="ITimeoutConfiguration"/></param>
        /// <param name="logger">Instance of <see cref="LocalizationLogger"/></param>
        public Application(WindowsDriver<WindowsElement> windowsDriver, ITimeoutConfiguration timeoutConfiguration, LocalizationLogger logger)
        {
            Logger = logger;
            WindowsDriver = windowsDriver;
            WindowsDriver.Manage().Timeouts().ImplicitWait = timeoutConfiguration.Implicit;
            logger.Info("loc.application.ready");
        }

        private LocalizationLogger Logger { get; }

        public RemoteWebDriver Driver => WindowsDriver;

        /// <summary>
        /// Provides instance of Windows Driver
        /// </summary>
        public WindowsDriver<WindowsElement> WindowsDriver { get; }

        /// <summary>
        /// Sets WinAppDriver ImplicitWait timeout. 
        /// Default value: <see cref="ITimeoutConfiguration.Implicit"/>.
        /// </summary>
        /// <param name="timeout">Desired Implicit wait timeout.</param>
        public void SetImplicitWaitTimeout(TimeSpan timeout)
        {
            if (timeout != implicitWait)
            {
                WindowsDriver.Manage().Timeouts().ImplicitWait = timeout;
                implicitWait = timeout;
            }
        }

        /// <summary>
        /// Quit application.
        /// </summary>
        public void Quit()
        {
            Logger.Info("loc.application.quit");
            WindowsDriver?.Quit();
        }
    }
}
