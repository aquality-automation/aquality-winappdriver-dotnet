namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Represents action keys which could be used in <see cref="IKeyboardActions"/>.
    /// Used to enhance logging of SendKeys actions.
    /// Int values correspond to Virtual-Key Codes: <seealso href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes"/>
    /// </summary>
    public enum ActionKey: short
    {
        /// <summary>
        /// Represents the number pad 0 key.
        /// </summary>
        NumberPad0 = 0x60,
        /// <summary>
        /// Represents the number pad 1 key.
        /// </summary>
        NumberPad1 = 0x61,
        /// <summary>
        /// Represents the number pad 2 key.
        /// </summary>
        NumberPad2 = 0x62,
        /// <summary>
        /// Represents the number pad 3 key.
        /// </summary>
        NumberPad3 = 0x63,
        /// <summary>
        /// Represents the number pad 4 key.
        /// </summary>
        NumberPad4 = 0x64,
        /// <summary>
        /// Represents the number pad 5 key.
        /// </summary>
        NumberPad5 = 0x65,
        /// <summary>
        /// Represents the number pad 6 key.
        /// </summary>
        NumberPad6 = 0x66,
        /// <summary>
        /// Represents the number pad 7 key.
        /// </summary>
        NumberPad7 = 0x67,
        /// <summary>
        /// Represents the number pad 8 key.
        /// </summary>
        NumberPad8 = 0x68,
        /// <summary>
        /// Represents the number pad 9 key.
        /// </summary>
        NumberPad9 = 0x69,
        /// <summary>
        /// Represents the number pad multiplication key.
        /// </summary>
        Multiply = 0x6A,
        /// <summary>
        /// Represents the number pad addition key.
        /// </summary>
        Add = 0x6B,
        /// <summary>
        /// Represents the number pad thousands separator key.
        /// </summary>
        Separator = 0x6C,
        /// <summary>
        /// Represents the number pad subtraction key.
        /// </summary>
        Subtract = 0x6D,
        /// <summary>
        /// Represents the number pad decimal separator key.
        /// </summary>
        Decimal = 0x6E,
        /// <summary>
        /// Represents the number pad division key.
        /// </summary>
        Divide = 0x6F,
        /// <summary>
        /// Represents the function key F1.
        /// </summary>
        F1 = 0x70,
        /// <summary>
        /// Represents the function key F2.
        /// </summary>
        F2 = 0x71,
        /// <summary>
        /// Represents the function key F3.
        /// </summary>
        F3 = 0x72,
        /// <summary>
        /// Represents the function key F4.
        /// </summary>
        F4 = 0x73,
        /// <summary>
        /// Represents the function key F5.
        /// </summary>
        F5 = 0x74,
        /// <summary>
        /// Represents the function key F6.
        /// </summary>
        F6 = 0x75,
        /// <summary>
        /// Represents the function key F7.
        /// </summary>
        F7 = 0x76,
        /// <summary>
        /// Represents the function key F8.
        /// </summary>
        F8 = 0x77,
        /// <summary>
        /// Represents the function key F9.
        /// </summary>
        F9 = 0x78,
        /// <summary>
        /// Represents the function key F10.
        /// </summary>
        F10 = 0x79,
        /// <summary>
        /// Represents the function key F11.
        /// </summary>
        F11 = 0x7A,
        /// <summary>
        /// Represents the function key F12.
        /// </summary>
        F12 = 0x7B,
        /// <summary>
        /// Represents the semi-colon key.
        /// </summary>
        Semicolon = 0xBA,
        /// <summary>
        /// Represents the Insert key.
        /// </summary>
        Insert = 0x2D,
        /// <summary>
        /// Represents the Cancel keystroke.
        /// </summary>
        Cancel = 0x03,
        /// <summary>
        /// Represents the Help keystroke.
        /// </summary>
        Help = 0x2F,
        /// <summary>
        /// Represents the Backspace key.
        /// </summary>
        Backspace = 0x08,
        /// <summary>
        /// Represents the Tab key.
        /// </summary>
        Tab = 0x09,
        /// <summary>
        /// Represents the Clear keystroke.
        /// </summary>
        Clear = 0x0C,
        /// <summary>
        /// Represents the Return key.
        /// </summary>
        Return = 0x0D,
        /// <summary>
        /// Represents the Enter key.
        /// </summary>
        Enter = 0x0D,
        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        Shift = 0x10,
        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        LeftShift = 0xA0,
        /// <summary>
        /// Represents the Control key.
        /// </summary>
        Control = 0x11,
        /// <summary>
        /// Represents the Control key.
        /// </summary>
        LeftControl = 0xA2,
        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        Alt = 0x12,
        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        LeftAlt = 0xA4,
        /// <summary>
        /// Represents the Delete key.
        /// </summary>
        Delete = 0x2E,
        /// <summary>
        /// Represents the Pause key.
        /// </summary>
        Pause = 0x13,
        /// <summary>
        /// Represents the Spacebar key.
        /// </summary>
        Space = 0x20,
        /// <summary>
        /// Represents the Page Up key.
        /// </summary>
        PageUp = 0x21,
        /// <summary>
        /// Represents the Page Down key.
        /// </summary>
        PageDown = 0x22,
        /// <summary>
        /// Represents the End key.
        /// </summary>
        End = 0x23,
        /// <summary>
        /// Represents the Home key.
        /// </summary>
        Home = 0x24,
        /// <summary>
        /// Represents the left arrow key.
        /// </summary>
        Left = 0x25,
        /// <summary>
        /// Represents the left arrow key.
        /// </summary>
        ArrowLeft = 0x25,
        /// <summary>
        /// Represents the up arrow key.
        /// </summary>
        Up = 0x26,
        /// <summary>
        /// Represents the up arrow key.
        /// </summary>
        ArrowUp = 0x26,
        /// <summary>
        /// Represents the right arrow key.
        /// </summary>
        Right = 0x27,
        /// <summary>
        /// Represents the right arrow key.
        /// </summary>
        ArrowRight = 0x27,
        /// <summary>
        /// Represents the Left arrow key.
        /// </summary>
        Down = 0x28,
        /// <summary>
        /// Represents the Left arrow key.
        /// </summary>
        ArrowDown = 0x28,
        /// <summary>
        /// Represents the Escape key.
        /// </summary>
        Escape = 0x1B
    }
}
