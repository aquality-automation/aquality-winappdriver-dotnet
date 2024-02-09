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

        protected override void PerformKeyActions(IList<KeyAction> keyActions, bool rootSession = false)
        {
            element.Click();
            elementActionsRetrier.DoWithRetry(() => base.PerformKeyActions(keyActions, rootSession));
        }

        protected override void LogAction(string messageKey, params object[] args)
        {
            localizedLogger.InfoElementAction(elementType, element.Name, messageKey, args);
        }

        public override void SendKeys(string keySequence, ActionKey? sendAfterSequence = null)
        {
            var valueToLog = $"{keySequence}{(sendAfterSequence == null ? string.Empty : $" + {sendAfterSequence}")}";
            LogAction("loc.keyboard.sendkeys", valueToLog);
            element.SendKeys(keySequence);
            if (sendAfterSequence != null)
            {
                var actions = new List<KeyAction>
                {
                    new KeyAction { VirtualKeyCode = (short)sendAfterSequence, Down = true },
                    new KeyAction { VirtualKeyCode = (short)sendAfterSequence, Down = false }
                };
                base.PerformKeyActions(actions);
            }
        }

        public override void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold, bool mayDisappear = false)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold);

            PerformKeyActions(new List<KeyAction> {
                    new KeyAction { VirtualKeyCode = (short)keyToHold, Down = true }
                });
            element.SendKeys(keySequence);
            base.PerformKeyActions(new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false } }, rootSession: mayDisappear);
        }
    }
}
