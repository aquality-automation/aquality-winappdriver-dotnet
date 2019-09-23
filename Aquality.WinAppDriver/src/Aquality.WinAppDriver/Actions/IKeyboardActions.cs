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
        void SendKeys(string keySequence);

        /// <summary>
        /// Sends a sequence of keystrokes to the application, holding a specified key.
        /// After the action, holded key is released.
        /// </summary>
        /// <param name="keySequence">A string representing the keystrokes to send.</param>
        /// <param name="keyToHold">The <see cref="ModifierKey"/> value representing the key to hold.</param>
        void SendKeysWithKeyHold(string keySequence, ModifierKey keyToHold);
    }
}
