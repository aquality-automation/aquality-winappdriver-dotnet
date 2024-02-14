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
    public interface IMouseActions : WinAppDriver.Actions.IMouseActions
    {
        /// <summary>
        /// Clicks the mouse at the element coordinates.
        /// </summary>
        /// <param name="button">Name of the mouse button to be clicked. An exception is thrown if an unknown button name is provided. 
        /// Supported button names are: left, middle, right, back, forward. The default value is left</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="times">How many times the click must be performed. One by default.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. Only makes sense if <paramref name="times"/> is greater than one. 100ms by default.</param>
        void Click(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Right-clicks the mouse at the element coordinates.
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
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
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing and releasing the mouse button. 
        /// By default no delay is applied, which simulates a regular click.</param>
        /// <param name="interClickDelay">Duration of the pause between each click gesture. 100ms by default.</param>
        /// </summary>
        void DoubleClick(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, TimeSpan? interClickDelay = null);

        /// <summary>
        /// Performs a drag-and-drop operation on the current element; drops it on target element.
        /// </summary>
        /// <param name="target">The element on which the drop is performed.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing the left mouse button and moving the cursor to the ending drag point. 
        /// 5000ms by default.</param>
        void DragAndDrop(IElement target, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Performs a drag-and-drop operation on the current element; drops it on target point.
        /// </summary>
        /// <param name="endX">The horizontal coordinate of the dropping point.</param>
        /// <param name="endY">The vertical coordinate of the dropping point.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing the left mouse button and moving the cursor to the ending drag point. 
        /// 5000ms by default.</param>
        void DragAndDrop(int endX, int endY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Performs a drag-and-drop operation on the current element to a specified offset.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing the left mouse button and moving the cursor to the ending drag point. 
        /// 5000ms by default.</param>
        void DragAndDropToOffset(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Moves the current element to a specified offset. Works the same as to <see cref="DragAndDropToOffset(int, int, IList{ModifierKey}, TimeSpan?)"/>
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the click is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while clicking, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">Time to wait between pressing the left mouse button and moving the cursor to the ending drag point. 
        /// 5000ms by default.</param>
        new void MoveByOffset(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);


        /// <summary>
        /// Hovers the cursor on the current element, then moves it to the end element.
        /// <param name="endElement">Element which coordinates to be used as the ending hover point.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the hover is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while hovering, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">The number of milliseconds between moving the cursor from the starting to the ending hover point. 500ms by default.</param>
        /// </summary>
        void Hover(IElement endElement, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Hovers the cursor on the current element, then moves it to the end element.
        /// <param name="endX">The horizontal coordinate of the ending hover point.</param>
        /// <param name="endY">The vertical coordinate of the ending hover point.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the hover is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while hovering, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">The number of milliseconds between moving the cursor from the starting to the ending hover point. 500ms by default.</param>
        /// </summary>
        void Hover(int endX, int endY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Moves the mouse from the current element.
        /// Works the same as to <see cref="Hover(int, int, IList{ModifierKey}, TimeSpan?)"/> when ending hover point is offset to current element in the direction up left, half of the element size.
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the hover is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while hovering, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">The number of milliseconds between moving the cursor from the starting to the ending hover point. 500ms by default.</param>
        /// </summary>
        void MoveFromElement(IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Moves the mouse to the current element. Works the same as to <see cref="Hover(IElement, IList{ModifierKey}, TimeSpan?)"/> when we don't move the mouse to other element afterwards.
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the hover is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while hovering, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">The number of milliseconds between moving the cursor from the starting to the ending hover point. 500ms by default.</param>
        /// </summary>
        void MoveToElement(IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Moves the mouse to the specified offset of the top-left corner of the current element.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the hover is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while hovering, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        /// <param name="duration">The number of milliseconds between moving the cursor from the starting to the ending hover point. 500ms by default.</param>
        void MoveToElement(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null);

        /// <summary>
        /// Scrolls the current screen by specified offset, starting from the current element.
        /// </summary>
        /// <param name="delta">The amount of horizontal wheel movement measured in wheel clicks (1 click ~ 120px). 
        /// In case of vertical scroll, a positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user.
        /// In case of horizontal scroll, a positive value indicates that the wheel was rotated to the right; a negative value indicates that the wheel was rotated to the left.</param>
        /// <param name="direction">Direction of scrolling.</param>
        /// <param name="modifierKeys">List of possible keys or a single key name to depress while the scroll is being performed. 
        /// Supported key names are: Shift, Ctrl, Alt, Win. 
        /// For example, in order to keep Ctrl+Alt depressed while scrolling, provide the value of [<see cref="ModifierKey.Ctrl"/>, <see cref="ModifierKey.Alt"/>]</param>
        void Scroll(int delta, ScrollDirection direction = ScrollDirection.Vertical, IList<ModifierKey> modifierKeys = null);
    }
}
