using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Applications;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Keyboard actions for the whole application.
    /// </summary>
    public class KeyboardActions : ApplicationActions, IKeyboardActions
    {
        private readonly Func<WindowsDriver> windowsDriverSupplier;

        public KeyboardActions(ILocalizedLogger localizationLogger, Func<WindowsDriver> windowsDriverSupplier)
            : base(localizationLogger, windowsDriverSupplier)
        {
            this.windowsDriverSupplier = windowsDriverSupplier;
        }


        protected virtual void PerformKeyActions(IList<KeyAction> keyActions, bool rootSession = false)
        {
            (rootSession ? AqualityServices.Application.RootSession : windowsDriverSupplier())
                .ExecuteScript("windows: keys", new Dictionary<string, object>(){{ "actions", keyActions.Select(act => act.ToDictionary()).ToArray() }});
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

        public virtual void SendKeys(string keySequence, ActionKey? sendAfterSequence = null)
        {
            var valueToLog = $"{keySequence}{(sendAfterSequence == null ? string.Empty : $" + {sendAfterSequence}")}";
            LogAction("loc.keyboard.sendkeys", valueToLog);
            var actions = new List<KeyAction> { new KeyAction { Text = keySequence }};
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

        public virtual void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold, bool mayDisappear = false)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold);

            if (mayDisappear)
            {
                PerformKeyActions(new List<KeyAction> { 
                    new KeyAction { VirtualKeyCode = (short)keyToHold, Down = true },
                    new KeyAction { Text = keySequence }
                });
                PerformKeyActions(new List<KeyAction> { new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false } }, rootSession: true);
            }
            else
            {
                PerformKeyActions(new List<KeyAction> {
                    new KeyAction { VirtualKeyCode = (short)keyToHold, Down = true },
                    new KeyAction { Text = keySequence },
                    new KeyAction { VirtualKeyCode = (short)keyToHold, Down = false }
                });
            }
        }
    }
}
