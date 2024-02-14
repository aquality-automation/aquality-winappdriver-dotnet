using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Applications;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Abstract class for any actions against the whole application.
    /// </summary>
    public abstract class ApplicationActions
    {
        private readonly ILocalizedLogger localizedLogger;
        private readonly Func<WindowsDriver> windowsDriverSupplier;

        /// <summary>
        /// Instantiates Application actions.
        /// </summary>
        /// <param name="localizedLogger">Logger for localized values.</param>
        /// <param name="windowsDriverSupplier">Method to get current application session.</param>
        protected ApplicationActions(ILocalizedLogger localizedLogger, Func<WindowsDriver> windowsDriverSupplier)
        {
            this.localizedLogger = localizedLogger;
            this.windowsDriverSupplier = windowsDriverSupplier;
        }

        /// <summary>
        /// Performs submitted action script with specified parameters.
        /// </summary>
        /// <param name="script">Script to be executed.</param>
        /// <param name="parameters">Script parameters.</param>
        /// <param name="rootSession">Whether to execute the script against the root session or the application session <see cref="AqualityServices.Application"/>.</param>
        protected virtual object PerformAction(string script, Dictionary<string, object> parameters, bool rootSession = false)
        {
            return (rootSession ? AqualityServices.Application.RootSession : windowsDriverSupplier()).ExecuteScript(script, parameters);
        }

        /// <summary>
        /// Logs keyboard action in specific format.
        /// </summary>
        /// <param name="messageKey">Key of the localized message.</param>
        /// <param name="args">Arguments for the localized message.</param>
        protected virtual void LogAction(string messageKey, params object[] args)
        {
            localizedLogger.Info(messageKey, args);
        }
    }
}
