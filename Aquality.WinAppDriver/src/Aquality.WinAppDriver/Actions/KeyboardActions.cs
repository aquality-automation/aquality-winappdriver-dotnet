using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Extensions;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Keyboard actions for the whole application.
    /// </summary>
    public class KeyboardActions : ApplicationActions, IKeyboardActions
    {
        public KeyboardActions(LocalizationLogger localizationLogger, Func<WindowsDriver<WindowsElement>> windowsDriverSupplier)
            : base(localizationLogger, windowsDriverSupplier)
        {
        }

        public void PressKey(string keyToPress)
        {
            LogAction("loc.keyboard.presskey", keyToPress.GetLoggableValueForKeyboardKey());
            PerformAction(actions => actions.KeyDown(keyToPress));
        }

        public void ReleaseKey(string keyToRelease)
        {
            LogAction("loc.keyboard.releasekey", keyToRelease.GetLoggableValueForKeyboardKey());
            PerformAction(actions => actions.KeyUp(keyToRelease));
        }

        public void SendKeys(string keySequence)
        {
            LogAction("loc.keyboard.sendkeys", keySequence);
            PerformAction(actions => actions.SendKeys(keySequence));
        }

        public void SendKeysWithKeyHold(string keySequence, string keyToHold)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold.GetLoggableValueForKeyboardKey());
            PerformAction(actions => actions.KeyDown(keyToHold).SendKeys(keySequence).KeyUp(keyToHold));
        }
    }
}
