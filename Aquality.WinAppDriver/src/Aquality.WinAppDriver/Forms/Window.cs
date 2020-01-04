using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines base class for a separate window of any application.
    /// </summary>
    public abstract class Window : Form
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the window.</param>
        /// <param name="name">Name of the window.</param>
        protected Window(By locator, string name) : base(locator, name, isRootSession: true)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.window");
    }
}
