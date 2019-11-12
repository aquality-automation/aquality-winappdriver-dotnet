using System;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Actions;
using Microsoft.Extensions.DependencyInjection;
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
        /// <param name="serviceProvider">Service provider to resolve all dependencies from DI container</param>
        public Application(WindowsDriver<WindowsElement> windowsDriver, IServiceProvider serviceProvider)
        {
            WindowsDriver = windowsDriver;
            Logger = serviceProvider.GetRequiredService<ILocalizedLogger>();
            KeyboardActions = serviceProvider.GetRequiredService<IKeyboardActions>();
            MouseActions = serviceProvider.GetRequiredService<IMouseActions>();
            var timeoutConfiguration = serviceProvider.GetRequiredService<ITimeoutConfiguration>();
            WindowsDriver.Manage().Timeouts().ImplicitWait = timeoutConfiguration.Implicit;
            Logger.Info("loc.application.ready");
        }

        private ILocalizedLogger Logger { get; }

        public RemoteWebDriver Driver => WindowsDriver;

        /// <summary>
        /// Provides instance of Windows Driver
        /// </summary>
        public WindowsDriver<WindowsElement> WindowsDriver { get; }

        public IKeyboardActions KeyboardActions { get; }

        public IMouseActions MouseActions { get; }

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
