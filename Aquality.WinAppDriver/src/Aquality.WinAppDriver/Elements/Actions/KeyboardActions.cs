using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
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
        /// <param name="localizationLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        public KeyboardActions(IElement element, string elementType, Func<IApplication> applicationSupplier, LocalizationLogger localizationLogger, ElementActionRetrier elementActionsRetrier)
            : base(element, elementType, applicationSupplier, localizationLogger, elementActionsRetrier)
        {
        }

        public void PressKey(string keyToPress)
        {
            LogAction("loc.keyboard.presskey", keyToPress.GetLoggableValueForKeyboardKey());
            PerformAction((actions, element) => actions.KeyDown(element, keyToPress));
        }

        public void ReleaseKey(string keyToRelease)
        {
            LogAction("loc.keyboard.releasekey", keyToRelease.GetLoggableValueForKeyboardKey());
            PerformAction((actions, element) => actions.KeyUp(element, keyToRelease));
        }

        public void SendKeys(string keySequence)
        {
            LogAction("loc.keyboard.sendkeys", keySequence);
            PerformAction((actions, element) => actions.SendKeys(element, keySequence));
        }

        public void SendKeysWithKeyHold(string keySequence, string keyToHold)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold.GetLoggableValueForKeyboardKey());
            PerformAction((actions, element) => actions.KeyDown(element, keyToHold).SendKeys(element, keySequence).KeyUp(element, keyToHold));
        }
    }
}
