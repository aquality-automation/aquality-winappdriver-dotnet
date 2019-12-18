using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using System.Drawing;
using Element = Aquality.WinAppDriver.Elements.Element;
using IElementFactory = Aquality.WinAppDriver.Elements.Interfaces.IElementFactory;

namespace Aquality.WinAppDriver.Windows
{
    /// <summary>
    /// Defines base class for any application's window.
    /// </summary>
    public abstract class Window : Element
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
        /// Element factory <see cref="IElementFactory"/>
        /// </summary>
        /// <value>Element factory.</value>
        protected IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();

        /// <summary>
        /// Finds element relative to current window.
        /// </summary>
        /// <typeparam name="T">Type of the target element.</typeparam>
        /// <param name="childLocator">Locator of the element relative to current window.</param>
        /// <param name="childName">Name of the element.</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom element.</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element.</returns>
        public T FindChildElement<T>(By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed) where T : IElement
        {
            return ElementFactory.FindChildElement(this, childLocator, childName, supplier, elementState);
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
