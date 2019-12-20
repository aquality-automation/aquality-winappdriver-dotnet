using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;

namespace Aquality.WinAppDriver.Elements.Interfaces
{
    /// <summary>
    /// Defines the interface used to create the windows application's elements.
    /// </summary>
    public interface IElementFactory : CoreElementFactory
    {
        /// <summary>
        /// Creates element that implements IButton interface.
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element that implements IButton interface</returns>
        IButton GetButton(By locator, string name, ElementState elementState = ElementState.Displayed);

        /// <summary>
        /// Creates element that implements ILabel interface.
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element that implements ILabel interface</returns>
        ILabel GetLabel(By locator, string name, ElementState elementState = ElementState.Displayed);

        /// <summary>
        /// Creates element that implements ITextBox interface.
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element that implements ITextBox interface</returns>
        ITextBox GetTextBox(By locator, string name, ElementState elementState = ElementState.Displayed);

        /// <summary>
        /// Finds element relative to parent window.
        /// </summary>
        /// <typeparam name="T">Type of the target element.</typeparam>
        /// <param name="parentWindow">Parent window for the element.</param>
        /// <param name="childLocator">Locator of the element relative to parent window.</param>
        /// <param name="childName">Name of the element.</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom element.</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element.</returns>
        T FindChildElement<T>(Window parentWindow, By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed) where T : IElement;
    }
}
