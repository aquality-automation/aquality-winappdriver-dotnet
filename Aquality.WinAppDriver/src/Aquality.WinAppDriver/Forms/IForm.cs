using System.Drawing;
using Aquality.WinAppDriver.Elements.Interfaces;

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
    }
}