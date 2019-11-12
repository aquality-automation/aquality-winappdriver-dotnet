using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Keyboard actions for the whole application.
    /// </summary>
    public class KeyboardActions : ApplicationActions, IKeyboardActions
    {
        public KeyboardActions(ILocalizedLogger localizationLogger, Func<WindowsDriver<WindowsElement>> windowsDriverSupplier)
            : base(localizationLogger, windowsDriverSupplier)
        {
        }

        public void PressKey(ModifierKey keyToPress)
        {
            LogAction("loc.keyboard.presskey", keyToPress);
            PerformAction(actions => actions.KeyDown(keyToPress.GetKeysString()));
        }

        public void ReleaseKey(ModifierKey keyToRelease)
        {
            LogAction("loc.keyboard.releasekey", keyToRelease);
            PerformAction(actions => actions.KeyUp(keyToRelease.GetKeysString()));
        }

        public void SendKeys(string keySequence)
        {
            LogAction("loc.keyboard.sendkeys", keySequence);
            PerformAction(actions => actions.SendKeys(keySequence));
        }

        public void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold)
        {
            var keyToHoldString = keyToHold.GetKeysString();
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold);
            PerformAction(actions => actions.KeyDown(keyToHoldString).SendKeys(keySequence).KeyUp(keyToHoldString));
        }
    }
}
