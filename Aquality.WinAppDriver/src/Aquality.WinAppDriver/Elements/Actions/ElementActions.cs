using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Abstract class for any actions agains element.
    /// </summary>
    public abstract class ElementActions
    {
        private readonly IElement element;
        private readonly string elementType;
        private readonly Func<WindowsDriver> windowsDriverSupplier;
        private readonly ILocalizedLogger localizedLogger;
        private readonly IElementActionRetrier elementActionsRetrier;

        /// <summary>
        /// Instantiates Element actions for a specific element.
        /// </summary>
        /// <param name="element">Target element.</param>
        /// <param name="elementType">Target element's type.</param>
        /// <param name="windowsDriverSupplier">Method to get current application session.</param>
        /// <param name="localizedLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        protected ElementActions(IElement element, string elementType, Func<WindowsDriver> windowsDriverSupplier, ILocalizedLogger localizedLogger, IElementActionRetrier elementActionsRetrier)
        {
            this.element = element;
            this.elementType = elementType;
            this.windowsDriverSupplier = windowsDriverSupplier;
            this.localizedLogger = localizedLogger;
            this.elementActionsRetrier = elementActionsRetrier;
        }

        /// <summary>
        /// Performs submitted action against new <see cref="SeleniumActions"/> object on current element.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        protected virtual void PerformAction(Func<SeleniumActions, IWebElement, SeleniumActions> action)
        {
            elementActionsRetrier.DoWithRetry(() =>
            {
                action(new SeleniumActions(windowsDriverSupplier()), element.GetElement()).Build().Perform();
            });
        }

        /// <summary>
        /// Performs submitted action against the <see cref="IWindowsApplication.RootSession"/>.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        protected internal virtual void PerformInRootSession(Func<SeleniumActions, SeleniumActions> action)
        {
            elementActionsRetrier.DoWithRetry(() =>
            {
                action(new SeleniumActions(AqualityServices.Application.RootSession)).Build().Perform();
            });
        }

        /// <summary>
        /// Logs element action in specific format.
        /// </summary>
        /// <param name="messageKey">Key of the localized message.</param>
        /// <param name="args">Arguments for the localized message.</param>
        protected virtual void LogAction(string messageKey, params object[] args)
        {
            localizedLogger.InfoElementAction(elementType, element.Name, messageKey, args);
        }
    }
}
