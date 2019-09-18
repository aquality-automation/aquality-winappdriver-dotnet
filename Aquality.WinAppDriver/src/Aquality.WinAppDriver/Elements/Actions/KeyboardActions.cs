using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
using OpenQA.Selenium;
using System;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Implements Keyboard actions for a specific element.
    /// </summary>
    public class KeyboardActions : IKeyboardActions
    {
        private readonly IElement element;
        private readonly string elementType;
        private readonly Func<IApplication> applicationSupplier;
        private readonly LocalizationLogger localizationLogger;
        private readonly ElementActionRetrier elementActionsRetrier;

        /// <summary>
        /// Instantiates Keyboard actions for a specific element.
        /// </summary>
        /// <param name="element">Target element.</param>
        /// <param name="elementType">Target element's type.</param>
        /// <param name="applicationSupplier">Method to get current application session.</param>
        /// <param name="localizationLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        public KeyboardActions(IElement element, string elementType, Func<IApplication> applicationSupplier, LocalizationLogger localizationLogger, ElementActionRetrier elementActionsRetrier)
        {
            this.element = element;
            this.elementType = elementType;
            this.applicationSupplier = applicationSupplier;
            this.localizationLogger = localizationLogger;
            this.elementActionsRetrier = elementActionsRetrier;
        }

        public void PressKey(string keyToPress)
        {
            LogElementAction("loc.keyboard.presskey", keyToPress.GetLoggableValueForKeyboardKey());
            PerformAction((actions, element) => actions.KeyDown(element, keyToPress));
        }

        public void ReleaseKey(string keyToRelease)
        {
            LogElementAction("loc.keyboard.releasekey", keyToRelease.GetLoggableValueForKeyboardKey());
            PerformAction((actions, element) => actions.KeyUp(element, keyToRelease));
        }

        public void SendKeys(string keySequence)
        {
            LogElementAction("loc.keyboard.sendkeys", keySequence);
            PerformAction((actions, element) => actions.SendKeys(element, keySequence));
        }

        public void SendKeysWithKeyHold(string keySequence, string keyToHold)
        {
            LogElementAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold.GetLoggableValueForKeyboardKey());
            PerformAction((actions, element) => actions.KeyDown(element, keyToHold).SendKeys(element, keySequence).KeyUp(element, keyToHold));
        }

        /// <summary>
        /// Performs submitted action against new <see cref="SeleniumActions"/> object.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        protected virtual void PerformAction(Func<SeleniumActions, IWebElement, SeleniumActions> action)
        {
            elementActionsRetrier.DoWithRetry(() =>
            {
                action(new SeleniumActions(applicationSupplier().Driver), element.GetElement()).Build().Perform();
            });            
        }

        /// <summary>
        /// Logs element action in specific format.
        /// </summary>
        /// <param name="messageKey">Key of the localized message.</param>
        /// <param name="args">Arguments for the localized message.</param>
        protected virtual void LogElementAction(string messageKey, params object[] args)
        {
            localizationLogger.InfoElementAction(elementType, element.Name, messageKey, args);
        }
    }
}
