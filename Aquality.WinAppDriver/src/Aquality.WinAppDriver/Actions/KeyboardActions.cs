using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Keyboard actions for the whole application.
    /// </summary>
    public class KeyboardActions : ApplicationActions, IKeyboardActions
    {
        [DllImport("User32")]
        private static extern short VkKeyScanA(char ch);

        public KeyboardActions(ILocalizedLogger localizationLogger, Func<WindowsDriver> windowsDriverSupplier)
            : base(localizationLogger, windowsDriverSupplier)
        {
        }

        protected virtual List<KeyAction> ConvertToSendKeysActions(string keySequence)
        {
            var actions = new List<KeyAction>();
            foreach (var key in keySequence)
            {
                var vkCode = VkKeyScanA(key);
                actions.Add(new KeyAction { VirtualKeyCode = vkCode, Down = true });
                actions.Add(new KeyAction { VirtualKeyCode = vkCode, Down = false });
            }
            return actions;
        }

        public virtual void PerformKeyActions(IList<KeyAction> keyActions, bool rootSession = false)
        {
            PerformAction("windows: keys", new Dictionary<string, object>(){{ "actions", keyActions.Select(act => act.ToDictionary()).ToArray() }}, rootSession);
        }

        public void PressKey(ModifierKey keyToPress)
        {
            LogAction("loc.keyboard.presskey", keyToPress);
            PerformKeyActions(new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToPress, Down = true }});
        }

        public void ReleaseKey(ModifierKey keyToRelease)
        {
            LogAction("loc.keyboard.releasekey", keyToRelease);
            PerformKeyActions(new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToRelease, Down = false } });
        }

        public void SendKeys(string keySequence, ActionKey? sendAfterSequence = null)
        {
            var valueToLog = $"{keySequence}{(sendAfterSequence == null ? string.Empty : $" + {sendAfterSequence}")}";
            LogAction("loc.keyboard.sendkeys", valueToLog);
            var actions = ConvertToSendKeysActions(keySequence);
            if (sendAfterSequence != null)
            {
                actions.Add(new KeyAction { VirtualKeyCode = (short)sendAfterSequence, Down = true });
                actions.Add(new KeyAction { VirtualKeyCode = (short)sendAfterSequence, Down = false });
            }
            PerformKeyActions(actions);
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
            var actions = new List<KeyAction>();
            for (int i = 0; i < times; i++)
            {
                actions.Add(new KeyAction { VirtualKeyCode = (short)key, Down = true });
                actions.Add(new KeyAction { VirtualKeyCode = (short)key, Down = false });
            }
            PerformKeyActions(actions);
        }

        public void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold, bool mayDisappear = false)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold);
            var actions = new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToHold, Down = true } };
            actions.AddRange(ConvertToSendKeysActions(keySequence));
            if (mayDisappear)
            {
                PerformKeyActions(actions);
                PerformKeyActions(new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false } }, rootSession: true);
            }
            else
            {
                actions.Add(new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false });
                PerformKeyActions(actions);
            }
        }

        public void SendKeysWithKeyHold(ActionKey key, ModifierKey keyToHold, bool mayDisappear = false)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", key, keyToHold);
            var actions = new List<KeyAction> {
                new KeyAction { VirtualKeyCode = (short)keyToHold, Down = true },
                new KeyAction { VirtualKeyCode = (short)key, Down = true },
                new KeyAction { VirtualKeyCode = (short)key, Down = false },
            };
            if (mayDisappear)
            {
                PerformKeyActions(actions);
                PerformKeyActions(new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false } }, rootSession: true);
            }
            else
            {
                actions.Add(new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false });
                PerformKeyActions(actions);
            }
        }
    }
}
