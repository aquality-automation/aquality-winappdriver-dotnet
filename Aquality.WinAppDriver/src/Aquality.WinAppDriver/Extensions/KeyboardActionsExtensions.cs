using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using OpenQA.Selenium;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;

namespace Aquality.WinAppDriver.Extensions
{
    public static class KeyboardActionsExtensions
    {
        public static void SendKeyWithWindowsKeyHold(this IKeyboardActions keyboardActions, char keyToSend)
        {
            LogSendKeyWithWindowsKeyHold(keyToSend);
            SendKeyInRootSession(keyboardActions, keyToSend.ToString());
        }

        public static void SendKeyWithWindowsKeyHold(this IKeyboardActions keyboardActions, ActionKey keyToSend)
        {
            LogSendKeyWithWindowsKeyHold(keyToSend);
            SendKeyInRootSession(keyboardActions, keyToSend.GetKeysString());
        }

        private static void LogSendKeyWithWindowsKeyHold(object keyToSend)
        {
            AqualityServices.LocalizedLogger.Info("loc.keyboard.sendkeys.withkeyhold", keyToSend, "Windows");
        }

        private static void SendKeyInRootSession(this IKeyboardActions keyboardActions, string key)
        {
            SeleniumActions action(SeleniumActions actions) => actions.SendKeys(Keys.Command + key + Keys.Command);
            if (keyboardActions is KeyboardActions appActions)
            {
                appActions.PerformInRootSession(action);
            } 
            else if (keyboardActions is Elements.Actions.KeyboardActions elementActions)
            {
                elementActions.PerformInRootSession(action);
            }
            else
            {
                action(new SeleniumActions(AqualityServices.Application.RootSession)).Build().Perform();
            }
        }
    }
}
