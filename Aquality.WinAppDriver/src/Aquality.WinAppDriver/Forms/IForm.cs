using System.Drawing;
using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines an interface for any form on any application's window.
    /// </summary>
    public interface IForm : IElement
    {
        /// <summary>
        /// Return window state for form locator
        /// </summary>
        /// <value>True - form is opened,
        /// False - form is not opened.</value>
        bool IsDisplayed { get; }
        
        /// <summary>
        /// Gets size of the form element defined by its locator.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Finds element relative to current form.
        /// </summary>
        /// <typeparam name="T">Type of the target element.</typeparam>
        /// <param name="childLocator">Locator of the element relative to current form.</param>
        /// <param name="childName">Name of the element.</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom element.</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element.</returns>
        T FindChildElement<T>(By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed) where T : IElement;
    }
}