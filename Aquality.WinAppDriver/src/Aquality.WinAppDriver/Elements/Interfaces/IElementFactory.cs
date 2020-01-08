using Aquality.Selenium.Core.Elements;
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
    }
}
