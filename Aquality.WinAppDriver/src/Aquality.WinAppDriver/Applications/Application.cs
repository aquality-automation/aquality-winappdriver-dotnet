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
    public class Application : IWindowsApplication
    {
        private TimeSpan implicitWait;
        private WindowsDriver<WindowsElement> applicationSession;
        private WindowsDriver<WindowsElement> rootSession;
        private readonly Func<WindowsDriver<WindowsElement>> createApplicationSession;
        private readonly Func<WindowsDriver<WindowsElement>> createDesktopSession;

        /// <summary>
        /// Instantiate application.
        /// </summary>
        /// <param name="createApplicationSession">Function to create an instance of WinAppDriver for current application</param>
        /// <param name="createRootSession">Function to create an instance of WinAppDriver for desktop session</param>
        /// <param name="serviceProvider">Service provider to resolve all dependencies from DI container</param>
        public Application(Func<WindowsDriver<WindowsElement>> createApplicationSession, Func<WindowsDriver<WindowsElement>> createRootSession, IServiceProvider serviceProvider)
        {
            this.createApplicationSession = createApplicationSession;
            this.createDesktopSession = createRootSession;
            Logger = serviceProvider.GetRequiredService<ILocalizedLogger>();
            KeyboardActions = serviceProvider.GetRequiredService<IKeyboardActions>();
            MouseActions = serviceProvider.GetRequiredService<IMouseActions>();
            var timeoutConfiguration = serviceProvider.GetRequiredService<ITimeoutConfiguration>();
            implicitWait = timeoutConfiguration.Implicit;
        }

        private ILocalizedLogger Logger { get; }

        RemoteWebDriver IApplication.Driver => Driver;

        public WindowsDriver<WindowsElement> Driver
        {
            get
            {
                if (applicationSession == null)
                {
                    applicationSession = createApplicationSession();
                    Logger.Info("loc.application.ready");
                    applicationSession.Manage().Timeouts().ImplicitWait = implicitWait;
                }
                return applicationSession;
            }
        }

        public IKeyboardActions KeyboardActions { get; }

        public IMouseActions MouseActions { get; }

        public WindowsDriver<WindowsElement> RootSession
        {
            get
            {
                if (rootSession == null)
                {
                    rootSession = createDesktopSession();
                    rootSession.Manage().Timeouts().ImplicitWait = implicitWait;
                }
                return rootSession;
            }
        }

        /// <summary>
        /// Sets WinAppDriver ImplicitWait timeout. 
        /// Default value: <see cref="ITimeoutConfiguration.Implicit"/>.
        /// </summary>
        /// <param name="timeout">Desired Implicit wait timeout.</param>
        public void SetImplicitWaitTimeout(TimeSpan timeout)
        {
            if (timeout != implicitWait)
            {
                if (rootSession != null)
                {
                    RootSession.Manage().Timeouts().ImplicitWait = timeout;
                }
                if (applicationSession != null)
                {
                    Driver.Manage().Timeouts().ImplicitWait = timeout;
                }
                implicitWait = timeout;
            }
        }

        /// <summary>
        /// Quit application.
        /// </summary>
        public void Quit()
        {
            Logger.Info("loc.application.quit");
            Driver?.Quit();
        }
    }
}
