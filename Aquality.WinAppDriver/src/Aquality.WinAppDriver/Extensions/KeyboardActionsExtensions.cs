using Aquality.WinAppDriver.Actions;

namespace Aquality.WinAppDriver.Extensions
{
    public static class KeyboardActionsExtensions
    {
        public static void SendKeyWithWindowsKeyHold(this IKeyboardActions keyboardActions, char keyToSend)
        {
            keyboardActions.SendKeysWithKeyHold(keyToSend.ToString(), ModifierKey.Win, mayDisappear: true);
        }

        public static void SendKeyWithWindowsKeyHold(this IKeyboardActions keyboardActions, ActionKey keyToSend)
        {
            keyboardActions.SendKeysWithKeyHold(keyToSend, ModifierKey.Win, mayDisappear: true);
        }
    }
}
