using OpenQA.Selenium;
using System.Drawing;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines base class for any application's window.
    /// </summary>
    public abstract class Window : Form
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the window.</param>
        /// <param name="name">Name of the window.</param>
        protected Window(By locator, string name) : base(locator, name)
        {
        }

        /// <summary>
        /// Return window state for window locator
        /// </summary>
        /// <value>True - window is opened,
        /// False - window is not opened.</value>
        public bool IsDisplayed => State.WaitForDisplayed();

        /// <summary>
        /// Gets size of window element defined by its locator.
        /// </summary>
        public Size Size => GetElement().Size;

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.window");
    }
}
