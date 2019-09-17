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
        /// <param name="keyToPress">The key value representing the key to press.</param>
        /// <remarks>The key value must be one of the values from the <see cref="OpenQA.Selenium.Keys"/> class.</remarks>
        /// <exception cref="System.ArgumentException">If the key sent is not is not one of:
        /// <see cref="OpenQA.Selenium.Keys.Shift"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Control"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Alt"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Meta"/>,
        /// <see cref="OpenQA.Selenium.Keys.Command"/>,
        /// <see cref="OpenQA.Selenium.Keys.LeftAlt"/>,
        /// <see cref="OpenQA.Selenium.Keys.LeftShift"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Shift"/>
        /// </exception>
        void PressKey(string keyToPress);

        /// <summary>
        /// Releases a key.
        /// </summary>
        /// <param name="keyToRelease">The key value representing the key to release.</param>
        /// <remarks>The key value must be one of the values from the <see cref="OpenQA.Selenium.Keys"/> class.</remarks>
        /// <exception cref="System.ArgumentException">If the key sent is not is not one of:
        /// <see cref="OpenQA.Selenium.Keys.Shift"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Control"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Alt"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Meta"/>,
        /// <see cref="OpenQA.Selenium.Keys.Command"/>,
        /// <see cref="OpenQA.Selenium.Keys.LeftAlt"/>,
        /// <see cref="OpenQA.Selenium.Keys.LeftShift"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Shift"/>
        /// </exception>
        void ReleaseKey(string keyToRelease);

        /// <summary>
        /// Sends a sequence of keystrokes to the target.
        /// </summary>
        /// <param name="keySequence">A string representing the keystrokes to send.</param>
        void SendKeys(string keySequence);

        /// <summary>
        /// Sends a sequence of keystrokes to the target, holding a specified key.
        /// After the action, holded key is released.
        /// </summary>
        /// <param name="keySequence">A string representing the keystrokes to send.</param>
        /// <param name="keyToHold">The key value representing the key to hold.
        /// <remarks>The key value must be one of the values from the <see cref="OpenQA.Selenium.Keys"/> class.</remarks></param>
        /// <exception cref="System.ArgumentException">If the key sent is not is not one of:
        /// <see cref="OpenQA.Selenium.Keys.Shift"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Control"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Alt"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Meta"/>,
        /// <see cref="OpenQA.Selenium.Keys.Command"/>,
        /// <see cref="OpenQA.Selenium.Keys.LeftAlt"/>,
        /// <see cref="OpenQA.Selenium.Keys.LeftShift"/>, 
        /// <see cref="OpenQA.Selenium.Keys.Shift"/>
        /// </exception>
        void SendKeysWithKeyHold(string keySequence, string keyToHold);
    }
}
