using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using System;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Implements Keyboard actions for a specific element.
    /// </summary>
    public class KeyboardActions : ElementActions, IKeyboardActions
    {
        /// <summary>
        /// Instantiates Keyboard actions for a specific element.
        /// </summary>
        /// <param name="element">Target element.</param>
        /// <param name="elementType">Target element's type.</param>
        /// <param name="applicationSupplier">Method to get current application session.</param>
        /// <param name="localizedLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        public KeyboardActions(IElement element, string elementType, Func<IApplication> applicationSupplier, ILocalizedLogger localizedLogger, ElementActionRetrier elementActionsRetrier)
            : base(element, elementType, applicationSupplier, localizedLogger, elementActionsRetrier)
        {
        }

        public void PressKey(ModifierKey keyToPress)
        {
            LogAction("loc.keyboard.presskey", keyToPress);
            PerformAction((actions, element) => actions.KeyDown(element, keyToPress.GetKeysString()));
        }

        public void ReleaseKey(ModifierKey keyToRelease)
        {
            LogAction("loc.keyboard.releasekey", keyToRelease);
            PerformAction((actions, element) => actions.KeyUp(element, keyToRelease.GetKeysString()));
        }

        public void SendKeys(string keySequence, ActionKey? sendAfterSequence = null)
        {
            var valueToLog = $"{keySequence}{(sendAfterSequence == null ? string.Empty : $" + {sendAfterSequence}")}";
            LogAction("loc.keyboard.sendkeys", valueToLog);
            var valueToSend = $"{keySequence}{sendAfterSequence.GetKeysOrEmptyString()}";
            PerformAction((actions, element) => actions.SendKeys(element, valueToSend));
        }

        public void SendKeys(ActionKey key, int times = 1)
        {
            if (times == 1)
            {
                LogAction("loc.keyboard.sendkey", key);
            }
            else
            {
                LogAction("loc.keyboard.sendkey.times", key, times);
            }
            PerformAction((actions, element) => actions.SendKeys(key.GetKeysString(times)));
        }

        public void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold)
        {
            var keyToHoldString = keyToHold.GetKeysString();
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold);
            PerformAction((actions, element) => actions.KeyDown(element, keyToHoldString).SendKeys(element, keySequence).KeyUp(element, keyToHoldString));
        }
    }
}
