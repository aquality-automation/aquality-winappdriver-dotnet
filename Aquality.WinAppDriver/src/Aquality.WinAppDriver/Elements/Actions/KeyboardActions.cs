using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Implements Keyboard actions for a specific element.
    /// </summary>
    public class KeyboardActions : WinAppDriver.Actions.KeyboardActions, IKeyboardActions
    {
        private readonly IElement element;
        private readonly string elementType;
        private readonly ILocalizedLogger localizedLogger;
        private readonly IElementActionRetrier elementActionsRetrier;

        /// <summary>
        /// Instantiates Keyboard actions for a specific element.
        /// </summary>
        /// <param name="element">Target element.</param>
        /// <param name="elementType">Target element's type.</param>
        /// <param name="windowsDriverSupplier">Method to get current application session.</param>
        /// <param name="localizedLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        public KeyboardActions(IElement element, string elementType, Func<WindowsDriver> windowsDriverSupplier, ILocalizedLogger localizedLogger, IElementActionRetrier elementActionsRetrier)
            : base(localizedLogger, windowsDriverSupplier)
        {
            this.element = element;
            this.elementType = elementType;
            this.localizedLogger = localizedLogger;
            this.elementActionsRetrier = elementActionsRetrier;
        }

        public override void PerformKeyActions(IList<KeyAction> keyActions, bool rootSession = false)
        {
            elementActionsRetrier.DoWithRetry(() => base.PerformKeyActions(keyActions, rootSession));
        }

        protected override void LogAction(string messageKey, params object[] args)
        {
            localizedLogger.InfoElementAction(elementType, element.Name, messageKey, args);
        }
    }
}
