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
        public KeyboardActions(ILocalizedLogger localizationLogger, Func<WindowsDriver> windowsDriverSupplier)
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

        public void SendKeys(string keySequence, ActionKey? sendAfterSequence = null)
        {
            var valueToLog = $"{keySequence}{(sendAfterSequence == null ? string.Empty : $" + {sendAfterSequence}")}";
            LogAction("loc.keyboard.sendkeys", valueToLog);
            var valueToSend = $"{keySequence}{sendAfterSequence.GetKeysOrEmptyString()}";
            PerformAction(actions => actions.SendKeys(valueToSend));
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
            PerformAction(actions => actions.SendKeys(key.GetKeysString(times)));
        }

        public void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold, bool mayDisappear = false)
        {
            var keyToHoldString = keyToHold.GetKeysString();
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold);
            if (mayDisappear)
            {
                PerformAction(actions => actions.KeyDown(keyToHoldString).SendKeys(keySequence));
                PerformInRootSession(actions => actions.KeyUp(keyToHoldString));
            }
            else
            {
                PerformAction(actions => actions.KeyDown(keyToHoldString).SendKeys(keySequence).KeyUp(keyToHoldString));
            }
        }
    }
}
