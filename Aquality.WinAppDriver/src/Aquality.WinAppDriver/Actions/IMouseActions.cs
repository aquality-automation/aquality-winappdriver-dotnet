namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Provides methods representing basic mouse actions.
    /// </summary>
    public interface IMouseActions
    {
        /// <summary>
        /// Clicks the mouse at the last known mouse coordinates.
        /// </summary>
        void Click();

        /// <summary>
        /// Clicks and holds the mouse button at the last known mouse coordinates.
        /// </summary>
        void ClickAndHold();

        /// <summary>
        /// Releases the mouse button at the last known mouse coordinates.
        /// </summary>
        void Release();

        /// <summary>
        /// Right-clicks the mouse at the last known mouse coordinates.
        /// </summary>
        void ContextClick();

        /// <summary>
        /// Double-clicks the mouse at the last known mouse coordinates.
        /// </summary>
        void DoubleClick();

        /// <summary>
        /// Moves the mouse to the specified offset of the last known mouse coordinates.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        void MoveByOffset(int offsetX, int offsetY);
    }
}
