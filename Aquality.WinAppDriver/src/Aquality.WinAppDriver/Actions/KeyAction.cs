using System;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Represents key action structure which could be used in chains in <see cref="IKeyboardActions"/>.
    /// </summary>
    public class KeyAction
    {
        /// <summary>
        /// Allows to set a delay between key input series. Either this property or text or virtualKeyCode must be provided.
        /// </summary>
        public TimeSpan? Pause { get; set; }

        /// <summary>
        /// Non-empty string of Unicode text to type (surrogate characters like smiles are not supported). Either this property or pause or virtualKeyCode must be provided.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Valid virtual key code. The list of supported key codes is available at Virtual-Key Codes page. Either this property or pause or text must be provided.
        /// You can use short values of <see cref="ModifierKey"/> and <see cref="ActionKey"/> for convenience here.
        /// </summary>
        public int? VirtualKeyCode { get; set; }

        /// <summary>
        /// This property only makes sense in combination with virtualKeyCode. 
        /// If set to true then the corresponding key will be depressed, false - released. By default the key is just pressed once. 
        /// ! Do not forget to release depressed keys in your automated tests.
        /// </summary>
        public bool? Down {  get; set; }

        /// <summary>
        /// Converts this structure to KeyActions dictionary.
        /// <seealso href="https://github.com/appium/appium-windows-driver?tab=readme-ov-file#keyaction"/>
        /// </summary>
        /// <returns>KeyAction dictionary.</returns>
        public Dictionary<string, object> ToDictionary()
        {
            var result = new Dictionary<string, object>();
            if (Pause != null)
            {
                result.Add("pause", Pause?.TotalMilliseconds);
            }
            if (Text != null)
            {
                result.Add("text", Text);
            }
            if (VirtualKeyCode != null)
            {
                result.Add("virtualKeyCode", VirtualKeyCode);
            }
            if (Down != null)
            {
                result.Add("down", Down);
            }
            return result;
        }
    }
}
