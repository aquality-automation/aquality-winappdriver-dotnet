namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Provides methods representing basic keyboard actions.
    /// </summary>
    public interface IKeyboardActions
    {
        /// <summary>
        /// Presses a key.
        /// </summary>
        /// <param name="keyToPress">The <see cref="ModifierKey"/> value representing the key to press.</param>
        void PressKey(ModifierKey keyToPress);

        /// <summary>
        /// Releases a key.
        /// </summary>
        /// <param name="keyToRelease">The <see cref="ModifierKey"/> value representing the key to release.</param>
        void ReleaseKey(ModifierKey keyToRelease);

        /// <summary>
        /// Sends a sequence of keystrokes to the target.
        /// </summary>
        /// <param name="keySequence">A string representing the keystrokes to send.</param>
        /// <param name="sendAfterSequence">An action key to send right after the <paramref name="keySequence"/></param>
        void SendKeys(string keySequence, ActionKey? sendAfterSequence = null);

        /// <summary>
        /// Sends a sequence of keystrokes to the target.
        /// </summary>
        /// <param name="key">The <see cref="ActionKey"/> value representing the keystroke to send.</param>
        /// <param name="times">Number of times for the keystroke to send.</param>
        void SendKeys(ActionKey key, int times = 1);

        /// <summary>
        /// Sends a sequence of keystrokes to the application, holding a specified key.
        /// After the action, holded key is released.
        /// </summary>
        /// <param name="keySequence">A string representing the keystrokes to send.</param>
        /// <param name="keyToHold">The <see cref="ModifierKey"/> value representing the key to hold.</param>
        /// <param name="mayDisappear">May the application or current window disappear after sending the <paramref name="keySequence"/>.</param>
        void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold, bool mayDisappear = false);
    }
}
