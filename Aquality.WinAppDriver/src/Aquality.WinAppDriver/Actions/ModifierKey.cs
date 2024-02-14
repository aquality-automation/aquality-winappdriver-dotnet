namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Represents modifier keys which could be used in <see cref="IKeyboardActions"/>.
    /// Int values correspond to Virtual-Key Codes: <seealso href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes"/>
    /// </summary>
    public enum ModifierKey: short
    {
        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        Alt = 0x12,
        /// <summary>
        /// Represents the Control key.
        /// </summary>
        Control = 0x11,
        /// <summary>
        /// Represents the Left Alt key.
        /// </summary>
        LeftAlt = 0xA4,
        /// <summary>
        /// Represents the Left Control key.
        /// </summary>
        LeftControl = 0xA2,
        /// <summary>
        /// Represents the Left Shift key.
        /// </summary>
        LeftShift = 0xA0,
        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        Shift = 0x10,
        /// <summary>
        /// Represents the Ctrl key.
        /// </summary>
        Ctrl = 0xA3,
        /// <summary>
        /// Represents the Win key.
        /// </summary>
        Win = 0x5B
    }
}
