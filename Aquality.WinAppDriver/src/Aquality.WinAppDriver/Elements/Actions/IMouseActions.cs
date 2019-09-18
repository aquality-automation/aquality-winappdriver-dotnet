using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium.Interactions;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Provides methods representing basic mouse actions against the current element.
    /// Current element's coordinates are user as last known mouse coordinates.
    /// </summary>
    public interface IMouseActions : WinAppDriver.Actions.IMouseActions
    {
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
        new void MoveByOffset(int offsetX, int offsetY);

        /// <summary>
        /// Moves the mouse to the current element.
        /// </summary>
        void MoveToElement();

        /// <summary>
        /// Moves the mouse from the current element.
        /// </summary>
        void MoveFromElement();

        /// <summary>
        /// Moves the mouse to the specified offset of the top-left corner of the current element.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        void MoveToElement(int offsetX, int offsetY);

        /// <summary>
        /// Moves the mouse to the specified offset of specified offset origin of the current element.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <param name="offsetOrigin">The <see cref="MoveToElementOffsetOrigin"/> value from which to calculate the offset.</param>
        void MoveToElement(int offsetX, int offsetY, MoveToElementOffsetOrigin offsetOrigin);
    }
}
