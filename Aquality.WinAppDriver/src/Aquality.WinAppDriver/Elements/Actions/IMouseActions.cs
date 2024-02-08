using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Provides methods representing basic mouse actions against the current element.
    /// </summary>
    public interface IMouseActions
    {
        /// <summary>
        /// Clicks the mouse at the element coordinates.
        /// </summary>
        /// <param name="button">Name of the mouse button to be clicked. An exception is thrown if an unknown button name is provided. 
        /// Supported button names are: left, middle, right, back, forward. The default value is left</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of ['ctrl', 'alt']</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="times">How many times the click must be performed. One by default.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        void Click(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Right-clicks the mouse at the element coordinates.
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of ['ctrl', 'alt']</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="times">How many times the click must be performed. One by default.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        /// </summary>
        void ContextClick(IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Double-clicks the mouse at the element coordinates.
        /// <param name="button">Name of the mouse button to be clicked. An exception is thrown if an unknown button name is provided. 
        /// Supported button names are: left, middle, right, back, forward. The default value is left</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of ['ctrl', 'alt']</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        /// </summary>
        void DoubleClick(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Performs a drag-and-drop operation on the current element; drops it on target element.
        /// </summary>
        /// <param name="target">The element on which the drop is performed.</param>
        void DragAndDrop(IElement target);

        /// <summary>
        /// Performs a drag-and-drop operation on the current element to a specified offset.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        void DragAndDropToOffset(int offsetX, int offsetY);

        /// <summary>
        /// Moves the current element to a specified offset. Works the same as to <see cref="DragAndDropToOffset(int, int)"/>
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        void MoveByOffset(int offsetX, int offsetY);

        /// <summary>
        /// Moves the mouse from the current element.
        /// </summary>
        void MoveFromElement();

        /// <summary>
        /// Moves the mouse to the current element.
        /// </summary>
        void MoveToElement();

        /// <summary>
        /// Moves the mouse to the specified offset of the top-left corner of the current element.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        void MoveToElement(int offsetX, int offsetY);

        /// <summary>
        /// Scrolls the current screen by specified offset, starting from the current element.
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        void Scroll(int offsetX, int offsetY);
    }
}
