using Aquality.Selenium.Core.Logging;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using System.Drawing;
using IElementFactory = Aquality.WinAppDriver.Elements.Interfaces.IElementFactory;

namespace Aquality.WinAppDriver.Windows
{
    /// <summary>
    /// Defines base class for any application's window.
    /// </summary>
    public abstract class Window
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the window.</param>
        /// <param name="name">Name of the window.</param>
        protected Window(By locator, string name)
        {
            Locator = locator;
            Name = name;
        }

        /// <summary>
        /// Locator of specified window.
        /// </summary>
        public By Locator { get; }

        /// <summary>
        /// Name of specified window.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Instance of logger <see cref="Selenium.Core.Logging.Logger"/>
        /// </summary>
        /// <value>Logger instance.</value>
        protected Logger Logger => ApplicationManager.GetRequiredService<Logger>();

        /// <summary>
        /// Element factory <see cref="IElementFactory"/>
        /// </summary>
        /// <value>Element factory.</value>
        protected IElementFactory ElementFactory => ApplicationManager.GetRequiredService<IElementFactory>();

        /// <summary>
        /// Return window state for window locator
        /// </summary>
        /// <value>True - window is opened,
        /// False - window is not opened.</value>
        public bool IsDisplayed => WindowLabel.State.WaitForDisplayed();

        /// <summary>
        /// Gets size of window element defined by its locator.
        /// </summary>
        public Size Size => WindowLabel.GetElement().Size;

        private ILabel WindowLabel => ElementFactory.GetLabel(Locator, Name);
    }
}
