using System;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Actions;
using OpenQA.Selenium.Remote;
using WindowsDriver = OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Provides functionality to work with Windows application via WinAppDriver.  
    /// </summary>
    public class Application : IWindowsApplication
    {
        private TimeSpan implicitWait;
        private WindowsDriver applicationSession;
        private WindowsDriver rootSession;
        private readonly Func<WindowsDriver> createApplicationSession;
        private readonly Func<WindowsDriver> createDesktopSession;

        /// <summary>
        /// Instantiate application.
        /// </summary>
        /// <param name="createApplicationSession">Function to create an instance of WinAppDriver for current application</param>
        /// <param name="createRootSession">Function to create an instance of WinAppDriver for desktop session</param>
        public Application(Func<WindowsDriver> createApplicationSession, Func<WindowsDriver> createRootSession)
        {
            this.createApplicationSession = createApplicationSession;
            this.createDesktopSession = createRootSession;
            Logger = AqualityServices.LocalizedLogger;
            KeyboardActions = AqualityServices.KeyboardActions;
            MouseActions = AqualityServices.MouseActions;
            var timeoutConfiguration = AqualityServices.Get<ITimeoutConfiguration>();
            implicitWait = timeoutConfiguration.Implicit;
        }

        private ILocalizedLogger Logger { get; }

        RemoteWebDriver IApplication.Driver => Driver;

        public virtual WindowsDriver Driver
        {
            get
            {
                if (!IsSessionStarted(applicationSession))
                {
                    applicationSession = createApplicationSession();
                    Logger.Info("loc.application.ready");
                    applicationSession.Manage().Timeouts().ImplicitWait = implicitWait;
                }
                return applicationSession;
            }
        }

        public virtual IKeyboardActions KeyboardActions { get; }

        public virtual IMouseActions MouseActions { get; }

        public virtual WindowsDriver RootSession
        {
            get
            {
                if (!IsSessionStarted(rootSession))
                {
                    rootSession = createDesktopSession();
                    rootSession.Manage().Timeouts().ImplicitWait = implicitWait;
                }
                return rootSession;
            }
        }

        public virtual bool IsStarted => IsSessionStarted(applicationSession) || IsSessionStarted(rootSession);

        private bool IsSessionStarted(WindowsDriver session) => session != null && session.SessionId != null;

        /// <summary>
        /// Sets WinAppDriver ImplicitWait timeout. 
        /// Default value: <see cref="ITimeoutConfiguration.Implicit"/>.
        /// </summary>
        /// <param name="timeout">Desired Implicit wait timeout.</param>
        public virtual void SetImplicitWaitTimeout(TimeSpan timeout)
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
        public virtual void Quit()
        {
            Logger.Info("loc.application.quit");
            applicationSession?.Quit();
            rootSession?.Quit();
        }

        public virtual IWindowsApplication Launch()
        {
            var launchedAppTitle = Driver.Title;
            AqualityServices.Logger.Debug(launchedAppTitle);
            return this;
        }
    }
}
