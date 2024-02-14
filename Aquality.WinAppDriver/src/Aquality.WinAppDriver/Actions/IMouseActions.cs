using System.Collections.Generic;
using System;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Provides methods representing basic mouse actions.
    /// </summary>
    public interface IMouseActions
    {
        /// <summary>
        /// Returns current mouse coordinates.
        /// </summary>
        Point Coordinates { get; }

        /// <summary>
        /// Clicks the mouse at the last known or specified mouse coordinates.
        /// </summary>
        /// <param name="x">The horizontal coordinate of the clicking point. If not specified, last known coordinate will be used.</param>
        /// <param name="y">The vertical coordinate of the clicking point. If not specified, last known coordinate will be used.</param>
        /// <param name="button">Name of the mouse button to be clicked. An exception is thrown if an unknown button name is provided. 
        /// Supported button names are: left, middle, right, back, forward. The default value is left</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="times">How many times the click must be performed. One by default.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        void Click(int? x = null, int? y = null, MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Right-clicks the mouse at the last known or specified mouse coordinates.
        /// </summary>
        /// <param name="x">The horizontal coordinate of the clicking point. If not specified, last known coordinate will be used.</param>
        /// <param name="y">The vertical coordinate of the clicking point. If not specified, last known coordinate will be used.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="times">How many times the click must be performed. One by default.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        void ContextClick(int? x = null, int? y = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Double-clicks the mouse at the last known or specified mouse coordinates.
        /// </summary>
        /// <param name="x">The horizontal coordinate of the clicking point. If not specified, last known coordinate will be used.</param>
        /// <param name="y">The vertical coordinate of the clicking point. If not specified, last known coordinate will be used.</param>
        /// <param name="button">Name of the mouse button to be clicked. An exception is thrown if an unknown button name is provided. 
        /// Supported button names are: left, middle, right, back, forward. The default value is left</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="times">How many times the click must be performed. One by default.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        void DoubleClick(int? x = null, int? y = null, MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Moves the mouse to the specified offset of the last known mouse coordinates.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        void MoveByOffset(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Scrolls the current screen by specified offset.
        /// </summary>
        /// <param name="x">The horizontal coordinate of the scroll point.</param>
        /// <param name="y">The vertical coordinate of the scroll point.</param>
        /// <param name="delta">The amount of horizontal wheel movement measured in wheel clicks (1 click ~ 120px). 
        /// In case of vertical scroll, a positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user.
        /// In case of horizontal scroll, a positive value indicates that the wheel was rotated to the right; a negative value indicates that the wheel was rotated to the left.</param>
        /// <param name="direction">Direction of scrolling.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the scroll is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while scrolling, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        void Scroll(int delta, int? x = null, int? y = null, ScrollDirection direction = ScrollDirection.Vertical, IList<ModifierKey> modifierKeys = null);


        /// <summary>
        /// Hovers the cursor on the current element, then moves it to the end element.
        /// <param name="startX">The horizontal coordinate of the starting hover point.</param>
        /// <param name="startY">The vertical coordinate of the starting hover point.</param>
        /// <param name="endX">The horizontal coordinate of the ending hover point. Will be the same as the starting point by default.</param>
        /// <param name="endY">The vertical coordinate of the ending hover point. Will be the same as the starting point by default.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the hover is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while hovering, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">The number of milliseconds between moving the cursor from the starting to the ending hover point. 500ms by default.</param>
        /// </summary>
        void Hover(int startX, int startY, int? endX = null, int? endY = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);
    }
}
